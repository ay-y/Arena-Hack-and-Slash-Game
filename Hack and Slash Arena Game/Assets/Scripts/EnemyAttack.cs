using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    public float timeBetweenAttacks = 2.0f;
    public int attackDamage = 25;

    GameObject player;
    PlayerHealth playerHealth;
    bool playerInRange;
    float timer;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
		
	}
   
    void OnTriggerStay(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "Player" && timer > timeBetweenAttacks) 
        {
            Attack(other);
        }
    }
    

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
	}

    void Attack(Collider other)
    {
        GameObject player = other.gameObject;
        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth.currentHealth > 0 && timer > timeBetweenAttacks)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        timer = 0f;
    }
}
