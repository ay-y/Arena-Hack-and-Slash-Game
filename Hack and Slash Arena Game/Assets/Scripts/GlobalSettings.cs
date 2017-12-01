using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour {

    public bool keyboard_activated;
    private Text timerText;
    private float timer = 0.0f;
    private int wave = 0;
    private GameObject[] players;
    private GameObject[] enemies;
    private GameObject[] spawnPoints;
    public GameObject virusEnemy;

    // Use this for initialization
	void Start () {
        keyboard_activated = false;
        Cursor.lockState = CursorLockMode.Confined;
        //timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timer = 10.0f;
        players = GameObject.FindGameObjectsWithTag("Player");
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
    }
	
	// Update is called once per frame
	void Update () {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");


        // press k to activate/deactivate keyboard input
        // will be a menu setting rather than a key press in the final version
        if (Input.GetKeyDown("k"))
        { keyboard_activated = !keyboard_activated; }

        if (Input.GetKeyDown("m") && enemies.Length < 101)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            Debug.Log(randomSpawn);
            spawnPoints[randomSpawn].GetComponent<SpawnScript>().SpawnEnemies(4, virusEnemy);
        }

        // wave timer
        //timer = timer - Time.deltaTime;
        //timerText.text = timer.ToString();


        // enemy counter

    }
}
