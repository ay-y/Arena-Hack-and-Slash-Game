﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveScript : MonoBehaviour {


    private bool debug = true;
    private Text timerText;
    private float timer = 0.0f;
    private int wave = 0;
    private GameObject[] players;
    private GameObject[] enemies;
    private GameObject[] spawnPoints;
    public GameObject virusEnemy;
    public Squad[] squadList;

    public class Squad {
        private int viruscount;
        private int othercount;
        private int othercount2;
    
        public int GetVirusCount()
        {
            return viruscount;
        }
        public void SetVirusCount(int n)
        {
            viruscount = n;
        }

    }


    // Use this for initialization
    void Start() {
        //timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timer = 10.0f;
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        squadList = new Squad[30];
        squadList = InitializeSquads();

    }

    // Update is called once per frame
    void Update() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (debug && Input.GetKeyDown("m") && enemies.Length < 101)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            //Debug.Log(randomSpawn);
            spawnPoints[randomSpawn].GetComponent<SpawnScript>().SpawnEnemies(4, virusEnemy);
        }

        if (Input.GetKeyDown("1") && enemies.Length < 101)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            int v = squadList[0].GetVirusCount();
            spawnPoints[randomSpawn].GetComponent<SpawnScript>().SpawnEnemies(v, virusEnemy);
        }
        if (Input.GetKeyDown("2") && enemies.Length < 101)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            int v = squadList[4].GetVirusCount();
            spawnPoints[randomSpawn].GetComponent<SpawnScript>().SpawnEnemies(v, virusEnemy);
        }


        // wave timer
        //timer = timer - Time.deltaTime;
        //timerText.text = timer.ToString();
    }

    private Squad[] InitializeSquads() {

        /*
        Intensity = difficulty of the squads

        Intensity : Indexes
        1: 0 - 4
        2: 5 - 10
        3: 11 - 16
        4: 17 - 20
        5: 21 - 30
        */

        Squad[] squadList = new Squad[30];
        squadList[0].SetVirusCount(1);
        squadList[1].SetVirusCount(2);
        squadList[2].SetVirusCount(3);
        squadList[3].SetVirusCount(4);
        squadList[4].SetVirusCount(10);

        return squadList;
    } 
    
}
