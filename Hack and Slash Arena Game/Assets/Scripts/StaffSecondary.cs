using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffSecondary : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Enemy")
        {

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
