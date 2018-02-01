using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeStaffSecondary : MonoBehaviour
{
    public int damage;
    // Use this for initialization
    void Start()
    {
        damage = 50;
    }

    void Explode()
    {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        exp.Play();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < 120.0f)
            {
                EnemyHealth eHealth = enemy.GetComponent<EnemyHealth>();
                eHealth.TakeDamage(damage);
            }


        }
        Destroy(gameObject, exp.main.duration);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Terrain")
        {
            Explode();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
