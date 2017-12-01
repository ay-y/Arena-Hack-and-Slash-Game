using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowPlayer2 : MonoBehaviour {


    public Transform target;

    private GameObject[] players;
    private GameObject[] viruses;

    Rigidbody rigidBody;
    private float maxVelocity = 3;
    private float lookSpeed = 0.15f;
    private Animator anim;

    private float timer;

    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        target = GameObject.FindWithTag("Player").transform;
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (target == null || timer > 1.0f)
        { 
            newTarget();
        }

        /*float dist = 100000;
        for (int x = 0; x < players.Length; x++) {
            float checkDist = Vector3.Distance(transform.position, players[x].transform.position);
            if (checkDist < dist)
            {
                dist = checkDist;
                target = players[x].transform;
            }

        }*/


        Vector3 difference = target.position - transform.position;
        difference.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(difference);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, lookSpeed);
        timer += Time.deltaTime;
   


        

	}

    private void newTarget()
    {
        float distance = Mathf.Infinity;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Vector3 diff = player.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                distance = curDistance;
                target = player.transform;
            }
        }
    }

    private void FixedUpdate()
    {
        int viruscount = 0;
        viruses = GameObject.FindGameObjectsWithTag("Virus");
        for (int x = 0; x < viruses.Length; x++)
        {
            float vdist = Vector3.Distance(transform.position, viruses[x].transform.position);
            if (vdist < 7.0f)
            {
                viruscount++;
            }
            if (vdist < 10.0f && vdist > 5.0f)
            {
                rigidBody.AddForce((viruses[x].transform.position - transform.position).normalized * 500.0f * Time.smoothDeltaTime);
            }
            else if (vdist < 5.0f)
            {
                rigidBody.AddForce((viruses[x].transform.position - transform.position).normalized * -200.0f * Time.smoothDeltaTime);
            }
        }
        
        rigidBody.AddRelativeForce(Vector3.forward * 3000 * Time.deltaTime);
        // clamp max velocity
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity + viruscount * 1.0f);
        anim.SetFloat("speed", rigidBody.velocity.magnitude);
    }
}