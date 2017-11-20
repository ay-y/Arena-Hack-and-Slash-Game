﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float lookTime = 0.4f;
    private Vector3 velocity = Vector3.zero;
    private GameObject[] players;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 target = new Vector3(0, 0, 0);
        for (int x = 0; x < players.Length; x++)
        {
            target = target + players[x].transform.position;
        }
        target = target / players.Length;
        target = new Vector3(target.x, 25.0f, target.z - 10);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, lookTime);
	}
}
