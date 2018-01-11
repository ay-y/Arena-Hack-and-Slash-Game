using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHeal : MonoBehaviour {
    Component glow;
    float timer;
    GameObject[] players;
    float checkDist;
    private bool on;
    float regenTimer;
    public int healAmount;

	// Use this for initialization
	void Start () {
        glow = GetComponent("Halo");
        on = false;
        checkDist = .6f;
        timer = 0.0f;
        regenTimer = 0.0f;
        glow.GetType().GetProperty("enabled").SetValue(glow, false, null);
        players = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        regenTimer += Time.deltaTime;
        if (timer > 40.0f)
        {
            timer = 0.0f;
            checkDist = 0.6f;
            on = false;
            glow.GetType().GetProperty("enabled").SetValue(glow, false, null);
        } else if (timer > 30.0f)
        {
            checkDist += Time.deltaTime;
            if (checkDist > .5f)
            {
                checkDist = 0.0f;
                foreach (GameObject player in players)
                {
                    if (player == null)
                    {
                        players = GameObject.FindGameObjectsWithTag("Player");
                    }
                    else
                    {
                        Vector3 diff = player.transform.position - transform.position;
                        float curDistance = diff.sqrMagnitude;
                        // heal every half second
                        if (curDistance < 80.0f && regenTimer > 0.5f)
                        {
                            PlayerHealth pHealth = player.GetComponent<PlayerHealth>();
                            pHealth.currentHealth += healAmount;
                            if (pHealth.currentHealth > pHealth.startingHealth)
                            {
                                pHealth.currentHealth = pHealth.startingHealth;
                            }
                            pHealth.healtherSlider.value = pHealth.currentHealth;
                        }
                        
                    }
                }
                regenTimer = 0.0f;
            }
            if (!on)
            {
                glow.GetType().GetProperty("enabled").SetValue(glow, true, null);
                on = true;
            }
        } 
	}
}
