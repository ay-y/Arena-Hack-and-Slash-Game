using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth;
    public int currentHealth;
    bool isDead;
    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            this.Death();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            int damage = collision.gameObject.GetComponent<AttackScript>().getDamage();
            this.TakeDamage(damage);
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
