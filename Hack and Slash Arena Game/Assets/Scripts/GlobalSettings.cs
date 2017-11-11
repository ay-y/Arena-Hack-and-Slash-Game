using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour {

    public bool keyboard_activated;
    private Text timerText;
    private float timer = 0.0f;

    // Use this for initialization
	void Start () {
        keyboard_activated = false;
        Cursor.lockState = CursorLockMode.Confined;
        //timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timer = 100.0f;
    }
	
	// Update is called once per frame
	void Update () {

        // press k to activate/deactivate keyboard input
        // will be a menu setting rather than a key press in the final version
        if (Input.GetKeyDown("k"))
        { keyboard_activated = !keyboard_activated; }



        // wave timer
        //timer = timer - Time.deltaTime;
        //timerText.text = timer.ToString();


        // enemy counter

	}
}
