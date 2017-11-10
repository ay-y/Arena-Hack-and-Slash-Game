using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    bool keyboard_activated = false;
    public int controlNumber = 1;
    public Collider targetPlane;
    private float lookSpeed = 0.5f;

    private Rigidbody rigidBody;
    private float accel = 2000;
    private float maxVelocity = 7;
    private Quaternion yeet;
    private Animator anim;

    private float lstickx = 0;
    private float lsticky = 0;

    private float rstickx = 0;
    private float rsticky = 0;

    // Left stick inputs
    public string inputHorizLeft = "HorizontalL_P1";
    public string inputVertLeft = "VerticalL_P1";

    // right stick inputs
    public string inputHorizRight = "HorizontalR_P1";
    public string inputVertRight = "VerticalR_P1";


	public GameObject Projectile;
    public float primaryCooldown = 0;


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        anim.SetFloat("velocity", rigidBody.velocity.magnitude);
        //Debug.Log(rigidBody.velocity.magnitude);

        float deadzone = 0.25f;

        GameObject settings = GameObject.Find("Global Settings");
        keyboard_activated = settings.GetComponent<GlobalSettings>().keyboard_activated;
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // floats for stick input
        lstickx = Input.GetAxis(inputHorizLeft) + 0.0001f;
        lsticky = Input.GetAxis(inputVertLeft) + 0.0001f;

        // create a direction vector based on input floats
        var ldirection = new Vector3(-lsticky, 0, lstickx);
        // set vector to quaternion (angle vector)
        var lrotation = Quaternion.LookRotation(ldirection, Vector3.up);
   
        // set to vector2 for checking deadzone
        Vector2 lstickInput = new Vector2(lstickx, lsticky);


        // same as above but for the right stick
        rstickx = Input.GetAxis(inputHorizRight) + 0.0001f;
        rsticky = Input.GetAxis(inputVertRight) + 0.0001f;

        var rdirection = new Vector3(rsticky, 0, rstickx);
        var rrotation = Quaternion.LookRotation(rdirection, Vector3.up);        
        Vector2 rstickInput = new Vector2(rstickx, rsticky);


        // check deadzone and set player rotation to preferred stick
        if (keyboard_activated)
        {
            Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            if (targetPlane.Raycast(camRay, out floorHit, 200.0f))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                //rigidBody.MoveRotation(newRotation);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, lookSpeed);
            }
        }
        else if (rstickInput.magnitude > deadzone)
        {
            transform.Rotate(new Vector3(0.0f, -90, 0.0f));
            transform.rotation = Quaternion.Slerp(transform.rotation, rrotation, lookSpeed);
            transform.Rotate(new Vector3(0.0f, 90, 0.0f));
        }
        else if (lstickInput.magnitude > deadzone)

        {
            transform.Rotate(new Vector3(0.0f, -90, 0.0f));
            transform.rotation = Quaternion.Slerp(transform.rotation, lrotation, lookSpeed);
            transform.Rotate(new Vector3(0.0f, 90, 0.0f));
        }

        //{ transform.rotation = lrotation; }

		//Check to see if primary attack should happen
		if ((Input.GetKey("space") || Input.GetAxis("Fire_P1")  > 0.9f) & primaryCooldown.CompareTo(0) <= 0)
        {
			Fire();
		}

        //Cooldown for primary attack
        if(primaryCooldown.CompareTo(0) > 0)
        {
            primaryCooldown -= Time.deltaTime;
        }

        // comparing look and velocity direction


        float directionOfVelocity = Mathf.Atan2(rigidBody.velocity.x, rigidBody.velocity.z) * Mathf.Rad2Deg;
        float directionOfLook = transform.eulerAngles.y;
        if (directionOfLook > 180)
        {
            directionOfLook = directionOfLook - 360;
        }

        float lookCompare = directionOfLook - directionOfVelocity;
        if (lookCompare > 180) {
            lookCompare -= 360;
        }
        if (lookCompare < -180)
        {
            lookCompare += 360;
        }
        anim.SetFloat("direction", -lookCompare);

        //Debug.Log(directionOfVelocity);
        //Debug.Log(directionOfLook);
        Debug.Log(lookCompare);


    }

    private void FixedUpdate()
    {
        // add force in direction of left stick
        if (keyboard_activated)
        {
            rigidBody.AddForce(new Vector3(Input.GetAxis("Horizontal_KB") * Time.deltaTime * accel, 0, Input.GetAxis("Vertical_KB") * Time.deltaTime * accel));
        }
        else
        {
            rigidBody.AddForce(new Vector3(lstickx * Time.deltaTime * accel, 0, lsticky * Time.deltaTime * accel));
        }

        // clamp max velocity
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
    }

	private void Fire()
	{
        var fireHeight = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        
		// Create the projectile 
		var bullet = (GameObject)Instantiate (
			Projectile,
			fireHeight,
			transform.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 60;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);

        //Primary cooldown 
        primaryCooldown = 0.2f;
	}
}
