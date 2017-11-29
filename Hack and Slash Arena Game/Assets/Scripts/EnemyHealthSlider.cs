using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour {
    EnemyHealth enemyHealth;
    GameObject healthUI;
    private int health;
    private bool damaged;
    private Slider healthSlider;
    private float timer;
    GameObject parent;
    // Use this for initialization
    void Start () {
        parent = gameObject.transform.parent.gameObject;
        enemyHealth = parent.GetComponent<EnemyHealth>();
        damaged = enemyHealth.isDamaged;
        healthSlider = GetComponentInChildren<Slider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer > .5f )
        {
            health = enemyHealth.currentHealth;
            healthSlider.value = health;
        }
    }

}
