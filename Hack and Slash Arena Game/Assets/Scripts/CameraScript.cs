using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float lookTime = 0.4f;
    private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 target = GameObject.Find("Player").transform.position;
        target = new Vector3(target.x, 25.0f, target.z - 10);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, lookTime);
	}
}
