﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {

	private int buttonWidth = 200;
	private int buttonHeight = 50;
	private int groupWidth = 200;
	private int groupHeight = 270;

	bool paused = false;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	
	}
	/** 
	 * Creates a pause menu that hovers above the screen and allows the player to do things like:
	 * Quit the Game
	 * Resume Play
	 * Go to the Settings
	 * Return to the Ttile Screen
	*/
	void OnGUI(){
		if (paused) {
			GUI.BeginGroup (new Rect (((Screen.width / 2) - (groupWidth / 2)), ((Screen.height / 2) - (groupHeight / 2)), groupWidth, groupHeight));

			if (GUI.Button (new Rect (0, 0, buttonWidth, buttonHeight), "Settings")) {
				GlobalController.Instance.changeScene ("Settings");
			}

			if (GUI.Button (new Rect (0, 60, buttonWidth, buttonHeight), "Resume Game")) {
				paused = togglePause ();
			}

			if (GUI.Button (new Rect (0, 120, buttonWidth, buttonHeight), "Return To Title")) {
				GlobalController.Instance.changeScene ("STARTUP");
			}

			if (GUI.Button (new Rect (0, 180, buttonWidth, buttonHeight), "Quit Game")) {
				Application.Quit();
			}

			GUI.EndGroup ();
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			paused = togglePause ();
		}
	}

	bool togglePause(){
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
			return false;
		} else {
			Time.timeScale = 0;
			return true;
		}
	}
		
}
