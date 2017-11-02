using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    bool keyboard_activated = false;
    public int controlNumber = 1;

    public GameObject target;

    private Rigidbody rigidBody;
    public float accel = 2000;
    public float maxVelocity = 10000;
    private Quaternion yeet;

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


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        float deadzone = 0.25f;

        GameObject settings = GameObject.Find("Global Settings");
        keyboard_activated = settings.GetComponent<GlobalSettings>().keyboard_activated;
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // floats for stick input
        lstickx = Input.GetAxis(inputHorizLeft) + 0.0001f;
        lsticky = Input.GetAxis(inputVertLeft) + 0.0001f;

        // fixes a bug that makes no sense and only applies to this axis on this stick for no good reason????
        //if (Mathf.Abs(lsticky) == 1) { lsticky = -lsticky; }

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
            //var lookPos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            /*lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 20.0f);
            transform.Rotate(0, -90, 0);*/

            //Vector3 targetPostition = new Vector3(lookPos.x, this.transform.position.y, lookPos.z);
            this.transform.LookAt(target.transform.position);

            /*Ray lookRay = cam.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit = lookRay.GetPoint(10);
            transform.LookAt(lookRay.GetPoint(10));
            transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);*/
        

    }
        else if (rstickInput.magnitude > deadzone)
        { transform.rotation = rrotation; }
        else if (lstickInput.magnitude > deadzone)
        { transform.rotation = lrotation; }

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
}
