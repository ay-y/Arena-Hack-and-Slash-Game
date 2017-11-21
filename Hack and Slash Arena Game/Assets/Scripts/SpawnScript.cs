using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    private int spawnCount = 0;
    private GameObject spawnType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnCount > 0)
        {
            GameObject enemy = (GameObject)Instantiate(
            spawnType,
            new Vector3(transform.position.x + Random.Range(-5.0f, 5.0f), 0, transform.position.z + Random.Range(-5.0f, 5.0f)),
            transform.rotation);

            spawnCount = spawnCount - 1;
        }
	}

    public void SpawnEnemies(int numEnemies, GameObject typeEnemy)
    {
        spawnCount = numEnemies;
        spawnType = typeEnemy;
    }
}
