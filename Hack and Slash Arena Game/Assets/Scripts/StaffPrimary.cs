using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffPrimary : MonoBehaviour {
    int damage;
	// Use this for initialization
	void Start () {
        damage = GetComponent<AttackScript>().getDamage();
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Terrain")
        {
            EnemyHealth e = collision.gameObject.GetComponent<EnemyHealth>();
            if (e != null)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, gameObject);
            }
            Destroy(this.gameObject); 
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
