using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

    public  float coolDown = 1.0f;
    public float speed = 60f;
    public  float destroyTime = 2.0f;
    public int damage = 25;

	// Use this for initialization
	void Start () {
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

    public int getDamage()
    {
        return damage;
    }
}
