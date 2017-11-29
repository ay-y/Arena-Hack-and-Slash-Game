using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth;
    public int currentHealth;
    private GameObject lastProj;
    public bool isDamaged;
    // UI things
    //private Slider healthSlider;
    // Use this for initialization
    void Start()
    {
        isDamaged = false;
        currentHealth = startingHealth;
        //healthSlider = gameObject.GetComponentInChildren<Slider>();
    }

    public void TakeDamage(int damage)
    {
        isDamaged = true;
        currentHealth -= damage;
        //healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            this.Death();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile" && collision.gameObject != lastProj)
        {
            int damage = collision.gameObject.GetComponent<AttackScript>().getDamage();
            this.TakeDamage(damage);
            lastProj = collision.gameObject;
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
