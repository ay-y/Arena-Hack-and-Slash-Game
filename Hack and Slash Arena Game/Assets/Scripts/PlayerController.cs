using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private Rigidbody rigidBody;
    public float accel = 2000;
    public float maxVelocity = 10000;
    private Quaternion yeet;

    private float lRotate = 0;
    private float lstickx = 0;
    private float lsticky = 0;

    private float rRotate = 0;
    private float rstickx = 0;
    private float rsticky = 0;

	public GameObject Projectile;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        float deadzone = 0.25f;

        // floats for stick input
        lstickx = Input.GetAxis("Horizontal");
        lsticky = Input.GetAxis("Vertical");

        // fixes a bug that makes no sense and only applies to this axis on this stick for no good reason????
        //if (Mathf.Abs(lsticky) == 1) { lsticky = -lsticky; }

        // create a direction vector based on input floats
        var ldirection = new Vector3(-lsticky, 0, lstickx);
        // set vector to quaternion (angle vector)
        var lrotation = Quaternion.LookRotation(ldirection, Vector3.up);
        // set to vector2 for checking deadzone
        Vector2 lstickInput = new Vector2(lstickx, lsticky);


        // same as above but for the right stick
        rstickx = Input.GetAxis("HorizontalR");
        rsticky = Input.GetAxis("VerticalR");

        var rdirection = new Vector3(rsticky, 0, rstickx);
        var rrotation = Quaternion.LookRotation(rdirection, Vector3.up);
        Vector2 rstickInput = new Vector2(rstickx, rsticky);


        // check deadzone and set player rotation to preferred stick
        if (rstickInput.magnitude > deadzone)
        { transform.rotation = rrotation; }
        else if (lstickInput.magnitude > deadzone)
        { transform.rotation = lrotation; }

		//Check to see if primary attack should happen
		if (Input.GetKey("space")) 
		{
			Fire ();
		}
    }

    private void FixedUpdate()
    {
        // add force in direction of left stick
        rigidBody.AddForce(new Vector3(lstickx * Time.deltaTime * accel, 0, lsticky * Time.deltaTime * accel));

        // clamp max velocity
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
      
    }

	private void Fire()
	{
		// Create the projectile 
		var bullet = (GameObject)Instantiate (
			Projectile,
			transform.position,
			transform.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}
}
