using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleKnockback : MonoBehaviour {
    private bool activated;
	// Use this for initialization
	void Start () {
        activated = false;
	}

    public void Knockback()
    {
        ParticleSystem smoke = GetComponent<ParticleSystem>();
        GetComponent<Renderer>().enabled = false;
        smoke.Play();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
           
            Vector3 diff = player.transform.position - transform.position;
            diff.y = 0;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < 81.0f)
            {
                //the closer, the larger the knockback
                float multiplier = 82.0f - curDistance;
                Rigidbody rigidbody = player.GetComponent<Rigidbody>();
                rigidbody.AddForce(diff.normalized*150*multiplier);
            }


        }
        Destroy(gameObject, smoke.main.duration);

    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && activated ==false)
        {
            activated = true;
            GetComponent<Renderer>().material.color = new Color(0, 0, 240);
            Invoke("Knockback", 2.0f);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
