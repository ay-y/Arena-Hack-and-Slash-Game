using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth;
    public int currentHealth;
    public Slider healtherSlider;
    public Image damageImage;
    bool damaged;
    bool isDead;
    public float flashSpeed = 5f;
    public Color flashcolour = new Color(1f, 0f, 0f, 0.1f);
	// Use this for initialization
	void Start () {

        currentHealth = startingHealth;
	}

    public void TakeDamage (int damage)
    {
        damaged = true;
        currentHealth -= damage;
        healtherSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Death();
        }
    }

    private void Death ()
    {
        isDead = true;
        Destroy(this.gameObject);
    }



    // Update is called once per frame
    void Update () {
		if (damaged)
        {
            damageImage.color = flashcolour;
        } else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}
}
