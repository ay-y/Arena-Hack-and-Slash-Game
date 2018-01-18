using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float lookTime = 1.5f;
    private Vector3 velocity = Vector3.zero;
    private GameObject[] players;
    private int playerCount;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        players = GameObject.FindGameObjectsWithTag("Player");
        Vector3 target = new Vector3(0, 0, 0);

        for (int x = 0; x < players.Length; x++)
        {
            if (players[x] != null)
            {
                target = target + players[x].transform.position;
            } else
            {
                players = GameObject.FindGameObjectsWithTag("Player");
            }
        }
        target = target / players.Length;
        target = new Vector3(target.x, 25.0f, target.z - 10);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, lookTime);
	}
}
