using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private Rigidbody rigidBody;
    public float accel = 1000;
    public float maxVelocity = 1000;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        rigidBody.AddForce(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * accel, 0, Input.GetAxis("Vertical") * Time.deltaTime * accel));

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
    }
}
