using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    bool keyboard_activated = false;
    public char controlNumber;
    public char controlNumberK;
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
    private string inputHorizLeft = "HorizontalL_P1";
    private string inputVertLeft = "VerticalL_P1";

    // right stick inputs
    private string inputHorizRight = "HorizontalR_P1";
    private string inputVertRight = "VerticalR_P1";

    // attack inputs
    private string atkPrimary = "Primary_P1";
    private string atkSecondary = "Secondary_P1";
    private string changeAtk = "Change_P1";

	public GameObject W1Primary;
    public float primaryCooldown = 0;

    public GameObject W1Secondary;
    public float secondaryCooldown = 0;

    public GameObject W2Primary;
    public GameObject W2Secondary;

    AttackScript Primary;
    AttackScript Secondary;

    private bool equipped = true;
    private bool alreadySwitched = false;

    public string weaponName = "stick";
    private GameObject weapon;


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        changeControls(controlNumber);
        // W1Primary = GameObject.Find("Sphere");
        Primary = W1Primary.GetComponent<AttackScript>();
        weapon = transform.Find("Weapon").gameObject;
    }

    // Update is called once per frame
    void Update() {
        // change this when keyboard switch changes to menu
        //if (Input.GetKey("k"))
        {
            if (!keyboard_activated) {
                changeControls(controlNumber);
            }
            else
            {
                changeControls(controlNumberK);
            }
        }

        anim.SetFloat("velocity", rigidBody.velocity.magnitude);
        //Debug.Log(rigidBody.velocity.magnitude);

        float deadzone = 0.25f;
        GameObject settings = GameObject.Find("Global Settings");
        keyboard_activated = settings.GetComponent<GlobalSettings>().keyboard_activated;
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // floats for stick input
        if (!(keyboard_activated && controlNumberK == '0'))
        {
            lstickx = Input.GetAxis(inputHorizLeft) + 0.0001f;
            lsticky = Input.GetAxis(inputVertLeft) + 0.0001f;
        }

        // create a direction vector based on input floats
        var ldirection = new Vector3(-lsticky, 0, lstickx);
        // set vector to quaternion (angle vector)
        var lrotation = Quaternion.LookRotation(ldirection, Vector3.up);
   
        // set to vector2 for checking deadzone
        Vector2 lstickInput = new Vector2(lstickx, lsticky);


        // same as above but for the right stick
        if (!(keyboard_activated && controlNumberK == '0'))
        {
            rstickx = Input.GetAxis(inputHorizRight) + 0.0001f;
            rsticky = Input.GetAxis(inputVertRight) + 0.0001f;
        }

        var rdirection = new Vector3(rsticky, 0, rstickx);
        var rrotation = Quaternion.LookRotation(rdirection, Vector3.up);
        Vector2 rstickInput = new Vector2(rstickx, rsticky);



        // check deadzone and set player rotation to preferred stick
        if (keyboard_activated && controlNumberK == '0')
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

        // the stick look is 90 degrees off for some reason so subtract 90 then slerp then add 90 again
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

        float lookX = rigidBody.velocity.magnitude * Mathf.Cos(-lookCompare * Mathf.Deg2Rad);
        float lookY = rigidBody.velocity.magnitude * Mathf.Sin(-lookCompare * Mathf.Deg2Rad);

        anim.SetFloat("x", lookX);
        anim.SetFloat("y", lookY);


        //Debug.Log(directionOfVelocity);
        //Debug.Log(directionOfLook);
        //Debug.Log(lookCompare);

        //Check to see if primary attack should happen
        if ((Input.GetAxis(atkPrimary) > 0.9f) & primaryCooldown.CompareTo(0) <= 0)
        {
            PrimaryAttack();
        }

        //Cooldown for primary attack
        if (primaryCooldown.CompareTo(0) > 0)
        {
            primaryCooldown -= Time.deltaTime;
        }

        //Check to see if Secondary attack should happen
        if ((Input.GetAxis(atkSecondary) > 0.9f) & primaryCooldown.CompareTo(0) <= 0)
        {
            SecondaryAttack();
        }

        //Cooldown for secondary attack
        if (secondaryCooldown.CompareTo(0) > 0)
        {
            secondaryCooldown -= Time.deltaTime;
        }

        //Check to see if equipment should be switched
        if (Input.GetButton(changeAtk) && !alreadySwitched)
        {
            equipped = !equipped;
            alreadySwitched = true;
        } else if (alreadySwitched)
        {
            alreadySwitched = false;
        }
        

        //update attack reference
       /* if(equipped)
        {
            Primary = W1Primary.GetComponent<AttackScript>();
            Secondary = W1Secondary.GetComponent<AttackScript>();

        } else 
        {
            Primary = W2Primary.GetComponent<AttackScript>();
            Secondary = W2Secondary.GetComponent<AttackScript>();
        }
        */

    }

    private void FixedUpdate()
    {
        // add force in direction of left stick
        if (keyboard_activated && controlNumberK == '0')
        {
            rigidBody.AddForce(new Vector3(Input.GetAxis("Horizontal_P0") * Time.deltaTime * accel, 0, Input.GetAxis("Vertical_P0") * Time.deltaTime * accel));
        }
        else
        {
            rigidBody.AddForce(new Vector3(lstickx * Time.deltaTime * accel, 0, lsticky * Time.deltaTime * accel));
        }

        // clamp max velocity
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
    }

    private void changeControls(char num)
    {
        inputHorizLeft = ChangeNum(inputHorizLeft, num);
        inputVertLeft = ChangeNum(inputVertLeft, num);
        inputHorizRight = ChangeNum(inputHorizRight, num);
        inputVertRight = ChangeNum(inputVertRight, num);
        atkPrimary = ChangeNum(atkPrimary, num);
        atkSecondary = ChangeNum(atkSecondary, num);
        changeAtk = ChangeNum(changeAtk, num);
    }

    private string ChangeNum(string str, char num)
    {
        int l = str.Length - 1;
        StringBuilder sb = new StringBuilder(str);
        sb[l] = num;
        str = sb.ToString();
        return str;
    }

	private void PrimaryAttack()
	{
        var fireHeight = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        // Create the projectile 
       // GameObject attack = null;
        //if (equipped)
        //{
            GameObject attack = (GameObject)Instantiate(
            W1Primary,
            fireHeight,
            transform.rotation);

            // Add velocity to the bullet
             attack.GetComponent<Rigidbody>().velocity = attack.transform.forward * W1Primary.GetComponent<AttackScript>().getSpeed();

            // Destroy the bullet after specified amount of time
           Destroy(attack, W1Primary.GetComponent<AttackScript>().getDestroyTime());

            //Primary cooldown 
            primaryCooldown = W1Primary.GetComponent<AttackScript>().getCooldown();
        //} else
        //{
          //  var attack = (GameObject)Instantiate(
            //W2Primary,
            //fireHeight,
            //transform.rotation);

            // Add velocity to the bullet
            //attack.GetComponent<Rigidbody>().velocity = attack.transform.forward * Primary.getSpeed();

            // Destroy the bullet after specified amount of time
//            Destroy(attack, Primary.getDestroyTime());

            //Primary cooldown 
  //          primaryCooldown = Primary.getCooldown();
    //    }

		// Add velocity to the bullet

		//attack.GetComponent<Rigidbody>().velocity = attack.transform.forward * Primary.getSpeed();

		// Destroy the bullet after specified amount of time
		//Destroy(attack, Primary.getDestroyTime());

        //Primary cooldown 
        //primaryCooldown = Primary.getCooldown();

		attack.GetComponent<Rigidbody>().velocity = attack.transform.forward * 60.0f;

		// Destroy the bullet after specified amount of time
		Destroy(attack, 2.0f);

        //Primary cooldown 
        primaryCooldown = 0.2f;

	}

    private void SecondaryAttack()
    {
        var fireHeight = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        // Create the projectile 
        GameObject attack = null;
        if (equipped)
        {
            attack = (GameObject)Instantiate(
            W1Secondary,
            fireHeight,
            transform.rotation);
        }
        else
        {
            attack = (GameObject)Instantiate(
            W2Secondary,
            fireHeight,
            transform.rotation);
        }

        // Add velocity to the bullet
        attack.GetComponent<Rigidbody>().velocity = attack.transform.forward * 80.0f;

        // Destroy the bullet after specified amount of time
        Destroy(attack, 1.5f);

        //secondary cooldown 
        secondaryCooldown = 1.0f;
    }

    public void ChangeWeapon(string wep)
    {
        weaponName = wep;
        SwapWepModel(wep);
    }

    private void SwapWepModel(string wep)
    {
        
    }

}
