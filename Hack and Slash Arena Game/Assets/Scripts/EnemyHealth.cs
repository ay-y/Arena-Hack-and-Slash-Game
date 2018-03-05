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

    public void TakeDamage(int damage, GameObject lastProj)
    {

        isDamaged = true;
        currentHealth -= damage;
        //healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            this.Death();
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
