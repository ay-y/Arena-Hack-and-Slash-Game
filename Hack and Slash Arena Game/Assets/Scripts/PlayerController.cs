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

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        lstickx = Input.GetAxis("Horizontal");
        lsticky = Input.GetAxis("Vertical");

        rstickx = Input.GetAxis("HorizontalR");
        rsticky = Input.GetAxis("VerticalR");      

        lRotate = Mathf.Atan2(-lsticky, lstickx) * (Mathf.Rad2Deg);
        rRotate = Mathf.Atan2(rsticky, rstickx) * (Mathf.Rad2Deg);

        yeet = new Quaternion(0, rRotate, 0, 0);
        Debug.Log(yeet);
        Debug.Log(transform.rotation);

        if ((rstickx > 0.3 || rstickx < -0.3) || (rsticky > 0.3 || rsticky < -0.3))
        {
            transform.eulerAngles = new Vector3(0, rRotate, 0);
        }
        else if ((lstickx > 0.3 || lstickx < -0.3) || (lsticky > 0.3 || lsticky < -0.3))
        {
            transform.eulerAngles = new Vector3(0, lRotate, 0);
        }

        if (Input.GetKeyDown("space"))
        {
            Debug.Log(rstickx);
            Debug.Log(rsticky);
            Debug.Log(rRotate);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(new Vector3(lstickx * Time.deltaTime * accel, 0, lsticky * Time.deltaTime * accel));
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
      
    }
}
