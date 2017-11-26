using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    AttackScript ascript;
    private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        ascript = gameObject.GetComponent<AttackScript>();
        rigidbody =gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rigidbody.AddForce(ascript.getCooldown(), 0, ascript.getCooldown());
	}
}
