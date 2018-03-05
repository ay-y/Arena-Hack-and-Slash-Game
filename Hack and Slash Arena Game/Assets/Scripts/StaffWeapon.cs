using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffWeapon : MonoBehaviour {
    public GameObject Primary;
    private float primaryCooldown;
    public Image arcaneAttack;

    public GameObject Secondary;
    private float secondaryCooldown;
    private float actualSecondaryCooldown;
    // Use this for initialization
    void Start () {
        primaryCooldown = 0.0f;
        secondaryCooldown = 0.0f;
        actualSecondaryCooldown = Secondary.GetComponent<AttackScript>().getCooldown();

    }
	
	// Update is called once per frame
	void Update () {
        secondaryCooldown += Time.deltaTime;
        primaryCooldown += Time.deltaTime;
        if (secondaryCooldown < actualSecondaryCooldown)
        {
            float fill = secondaryCooldown / actualSecondaryCooldown;
            arcaneAttack.fillAmount = fill;
        } else if (arcaneAttack.fillAmount < 1)
        {
            arcaneAttack.fillAmount = 1;
        }

	}

    public void PrimaryAttack()
    {
        if (primaryCooldown > Primary.GetComponent<AttackScript>().getCooldown())
        {
            var fireHeight = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

            // Create the projectile 

            GameObject attack = (GameObject)Instantiate(
            Primary,
            fireHeight,
            transform.rotation);

            // Add velocity to the bullet
            attack.GetComponent<Rigidbody>().velocity = attack.transform.forward * Primary.GetComponent<AttackScript>().getSpeed();

            // Destroy the bullet after specified amount of time

            Destroy(attack, Primary.GetComponent<AttackScript>().getDestroyTime());

            //Primary cooldown 
            primaryCooldown = 0.0f;
        }
    }


    public void SecondaryAttack()
    {
        if (secondaryCooldown > actualSecondaryCooldown)
        {
            var fireHeight = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            
            // Create the projectile 
            GameObject attack = null;
            attack = (GameObject)Instantiate(
            Secondary,
            fireHeight,
            transform.rotation);


            // Add velocity to the bullet
            attack.GetComponent<Rigidbody>().velocity = attack.transform.forward * Secondary.GetComponent<AttackScript>().getSpeed();

        

            // Destroy the bullet after specified amount of time
            if (Secondary != null)
            {
                Destroy(attack, Secondary.GetComponent<AttackScript>().getDestroyTime());
            }

            secondaryCooldown = 0.0f;
        }
    }

}
