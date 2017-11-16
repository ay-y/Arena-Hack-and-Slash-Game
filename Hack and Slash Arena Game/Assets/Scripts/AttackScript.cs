using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

    private float coolDown;
    private float speed;
    private float destroyTime;
    private float damage;

	// Use this for initialization
	void Start () {
        coolDown = 1.0f;
        speed = 60f;
        destroyTime = 2.0f;
        damage = 25f;
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
