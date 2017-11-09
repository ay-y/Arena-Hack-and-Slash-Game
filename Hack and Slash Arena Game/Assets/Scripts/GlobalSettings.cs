using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour {

    public bool keyboard_activated;
    
    // Use this for initialization
	void Start () {
        keyboard_activated = false;
        Cursor.lockState = CursorLockMode.Confined;
	}
	
	// Update is called once per frame
	void Update () {
        // press k to activate/deactivate keyboard input
        if (Input.GetKeyDown("k"))
        { keyboard_activated = !keyboard_activated; }
	}
}
