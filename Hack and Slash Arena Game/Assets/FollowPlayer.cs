using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Rigidbody rigidBody;
    public Rigidbody playerRigidBody;
    public Transform target;
    public float angleBetween;
	// Use this for initialization
	void Start () {
        //transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
	}

    private void OnTriggerEnter(Collider other)
    {
        float speed = 5f;
        Vector3 dir = -other.transform.position + transform.position;
        dir.Normalize();
        if (other.gameObject.tag == "Player")
        {
            //rigidBody.AddForce(dir * 100f);
            rigidBody.velocity = new Vector3(dir.x * speed, 0, dir.y * speed);
        }
    }
    // Update is called once per frame
    void Update () {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        transform.LookAt(target);
        rigidBody.AddForce(direction * 100f * Time.deltaTime);

	}
}
