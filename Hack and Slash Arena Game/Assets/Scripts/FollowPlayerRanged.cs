using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class FollowPlayerRanged : MonoBehaviour {


    public Transform target;

    private GameObject[] players;
    private GameObject[] viruses;
    NavMeshAgent agent;

    Rigidbody rigidBody;
    private float maxVelocity = 3;
    private float lookSpeed = 0.15f;
    private Animator anim;

    private float timer = 0.0f;
    

    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        target = GameObject.FindWithTag("Player").transform;
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5.5f;
    }
	
	// Update is called once per frame
	void Update () {
        viruses = GameObject.FindGameObjectsWithTag("Virus");
        players = GameObject.FindGameObjectsWithTag("Player");
        float dist = 100000;

        for (int x = 0; x < players.Length; x++) {
            float checkDist = Vector3.Distance(transform.position, players[x].transform.position);
            if (checkDist < dist)
            {
                    dist = checkDist;
                    target = players[x].transform;
                    agent.SetDestination(target.position);
            }
        }

        float shootDist = 13.5f;
        float checkShootDist = Vector3.Distance(transform.position, target.transform.position);
        if (checkShootDist < shootDist)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(target.transform.position);

        }


        anim.SetFloat("speed", agent.velocity.magnitude);

    }
    
    private void FixedUpdate()
    {

    }
}