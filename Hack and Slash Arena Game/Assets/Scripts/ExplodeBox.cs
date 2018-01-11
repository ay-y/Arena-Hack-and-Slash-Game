using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBox : MonoBehaviour {
    float fuseTime;
    public int damage;
	// Use this for initialization
	void Start () {
        fuseTime = 2.0f;
	}

    void Explode()
    {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        GetComponent<Renderer>().enabled = false;
        exp.Play();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
                Vector3 diff = player.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                // heal every half second
                if (curDistance < 80.0f)
                {
                    PlayerHealth pHealth = player.GetComponent<PlayerHealth>();
                    pHealth.TakeDamage(damage);
                }

            
        }
        Destroy(gameObject, exp.main.duration);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Explode", fuseTime);
            GetComponent<Renderer>().material.color = new Color(240, 0, 0);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
