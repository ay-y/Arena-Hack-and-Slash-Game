﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowPlayer2 : MonoBehaviour {

    private GameObject[] players;
    private Transform target;
    Rigidbody rigidBody;
    private float maxVelocity = 5;
    private float lookSpeed = 0.15f;
    private Animator anim;

    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        target = GameObject.FindWithTag("Player").transform;
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float dist = 100000;
        for (int x = 0; x < players.Length; x++) {
            float checkDist = Vector3.Distance(transform.position, players[x].transform.position);
            if (checkDist < dist)
            {
                dist = checkDist;
                target = players[x].transform;
            }

        }

        Vector3 difference = target.position - transform.position;
        difference.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(difference);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, lookSpeed);

        
	}
    private void FixedUpdate()
    {
        rigidBody.AddRelativeForce(Vector3.forward * 2000 * Time.deltaTime);
        // clamp max velocity
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
        anim.SetFloat("speed", rigidBody.velocity.magnitude);
    }
}
