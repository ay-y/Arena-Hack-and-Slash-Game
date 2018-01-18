using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour {

    public bool keyboard_activated;
    private GameObject[] players;


    // Use this for initialization
	void Start () {
        keyboard_activated = false;
        Cursor.lockState = CursorLockMode.Confined;
        players = GameObject.FindGameObjectsWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {


        // press k to activate/deactivate keyboard input
        // will be a menu setting rather than a key press in the final version
        if (Input.GetKeyDown("k"))
        { keyboard_activated = !keyboard_activated; }





    }
}
