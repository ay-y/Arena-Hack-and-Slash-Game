using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    public float timeBetweenAttacks = 0.5f;
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
    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "Player") ;
        {
            Attack();
        }
    }
    

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        //if (timer >= timeBetweenAttacks && playerInRange)
        //{
        //    Attack();
       // }
	}

    void Attack()
    {
        if (playerHealth.currentHealth > 0 && timer > timeBetweenAttacks)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        timer = 0f;
    }
}
