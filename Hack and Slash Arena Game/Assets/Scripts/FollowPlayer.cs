using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Rigidbody rigidBody;
    public Rigidbody playerRigidBody;
    public Transform target;
    public float angleBetween;
    public int speed;
	// Use this for initialization
	void Start () {
        //transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        float knockSpeed = 4f;
        Vector3 dir = other.transform.position - transform.position;
        dir.Normalize();
        if (other.gameObject.tag == "Player")
        {
            //rigidBody.AddForce(dir * 100f);
            rigidBody.velocity = new Vector3(-dir.x * knockSpeed, 0, -dir.y * knockSpeed);
        }
    }
    // Update is called once per frame
    void Update () {

        
    }

    void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        transform.LookAt(target);
        rigidBody.AddForce(direction * speed * Time.deltaTime);
    }
}
