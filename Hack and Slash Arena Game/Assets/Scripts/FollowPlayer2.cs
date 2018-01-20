using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class FollowPlayer2 : MonoBehaviour {


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
        agent.speed = 3.5f;
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

        int virusCount = 0;

        foreach (GameObject virus in viruses)
        {
            if (Vector3.Distance(virus.transform.position, transform.position) < 6.0f){
                virusCount++;
            }
        }
        agent.speed = 4.5f + (virusCount * 0.75f);

        if (agent.speed > 6.0f)
        {
            agent.speed = 6.0f;
        }
        anim.SetFloat("speed", agent.velocity.magnitude);

    }
    
    private void FixedUpdate()
    {

    }
}