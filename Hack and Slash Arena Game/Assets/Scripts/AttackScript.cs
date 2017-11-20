using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

    public  float coolDown = 1.0f;
    public float speed = 60f;
    public  float destroyTime = 2.0f;
    public float damage = 25f;

	// Use this for initialization
	void Start () {
        coolDown = 1.0f;
        speed = 60.0f;
        destroyTime = 2.0f;
        damage = 25.0f;
	}
    

    public float getCooldown()
    {
        return coolDown;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getDestroyTime()
    {
        return destroyTime;
    }

    public float getDamage()
    {
        return damage;
    }
}
