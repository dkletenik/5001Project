﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Checks the respective challenge and makes changes to the game if the user is correct or not
 * Challenge: Conditional challenge 1
 */ 
public class LogicalAndCompletion : MonoBehaviour {

	//Draggable Tiles and their Replacements, and flag bools
	public ArrayReaction slotOneSuccess, slotTwoSuccess, andSuccess;
	public ArrayReaction replacementTrue, replacementTrueToo, replacementAnd;
	public bool puzzleFinished, camToggled, leftPylonFlag, rightPylonFlag, doorOpened, scoreChanged;
	public GameObject[] arrayTiles; // the tiles that will be dragged
	public GameObject[] replacementTiles; //The replacements when tiles dragged into the slots

	//Pylon GameObjects
	public GameObject leftPylonClosed;
	public GameObject rightPylonClosed;
	public GameObject leftPylonRaised;
	public GameObject rightPylonRaised;
	public GameObject doorOne;

	public AudioSource solved, raisePillarSound, raiseDoor;

	//GameObject spawn in locations
	private float rightX = 116.4f;
	private float leftX = 102.4f;
	private float inSceneY = 9f;
	private float offScreenY = 48f;

	//Door Starting and Ending Locations
	private Vector3 doorOneStartingPosition, doorOneOpenPosition;

	// Use this for initialization
	void Start () {
		arrayTiles = GameObject.FindGameObjectsWithTag ("ArrayTile");
		puzzleFinished = false;
		camToggled = false;
		leftPylonFlag = false;
		rightPylonFlag = false;

		doorOpened = false;
		doorOneStartingPosition = doorOne.transform.position; //The starting position of the door in the scene
		doorOneOpenPosition = new Vector3 (doorOne.transform.position.x, doorOne.transform.position.y + 11.0f, 
			doorOne.transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		if (slotOneSuccess.success && replacementTrue.giveName == "ReplacementTrue") {
			if (!GlobalController.Instance.logicalAndComplete && !puzzleFinished) {
				controlLeft ();
			}
		}

		if (slotTwoSuccess.success && replacementTrueToo.giveName == "ReplacementTrue") {
			if (!GlobalController.Instance.logicalAndComplete && !puzzleFinished) {
				controlRight ();
			}
		}

		if (andSuccess.success && replacementAnd.giveName == "ReplacementAND") {
			if (leftPylonFlag && rightPylonFlag && !doorOpened && slotOneSuccess.success && slotTwoSuccess.success) {
				openDoor ();

				puzzleFinished = true;
				solved.Play ();
				if (!camToggled) {
					GlobalController.Instance.toggleCamera ();
					camToggled = true;
				}
				//add to score
				if (!scoreChanged) {
					raiseDoor.Play ();
					raiseDoor.loop = false;
					GlobalController.Instance.incScore ();
					scoreChanged = true;
				}

			}
		}


		if (Input.GetKeyDown (KeyCode.R)) {
			resetPuzzle ();
		}
	}
	public void resetPuzzle(){
		if (GlobalController.Instance.camName == "LogicalAndCamera") {
			GlobalController.Instance.logicalAndComplete = false;
			closeDoor ();
			doorOpened = false;
			resetPylon ();
			resetTiles ();
			resetSlots ();
			resetActive ();
			resetCheckValues ();
			camToggled = false;
			puzzleFinished = false;
			leftPylonFlag = false;
			rightPylonFlag = false;
			//Lower Score
			GlobalController.Instance.decAdditive ();
		}
	}
		
	public void controlLeft(){
		if (!leftPylonFlag) {
			leftPylonRaised.transform.position = new Vector3 (leftX, inSceneY, 0);
			leftPylonClosed.transform.position = new Vector3 (leftX, offScreenY, 0);
			leftPylonFlag = true;
			raisePillarSound.Play ();
			raisePillarSound.loop = false;
		}	
	}

	public void controlRight(){
		if (!rightPylonFlag) {
			rightPylonRaised.transform.position = new Vector3 (rightX, inSceneY, 0);
			rightPylonClosed.transform.position = new Vector3 (rightX, offScreenY, 0);
			rightPylonFlag = true;
			raisePillarSound.Play ();
			raisePillarSound.loop = false;
		}	
	}

	void openDoor(){
		doorOne.transform.position = doorOneOpenPosition;
		doorOpened = true;
	}

	void closeDoor(){
		doorOne.transform.position = doorOneStartingPosition;
		raiseDoor.Play ();
		raiseDoor.loop = false;
	}

	public void resetPylon (){
		leftPylonRaised.transform.position = new Vector3 (leftX, offScreenY, 0);
		rightPylonRaised.transform.position = new Vector3 (rightX, offScreenY, 0);
		leftPylonClosed.transform.position = new Vector3 (leftX, inSceneY - 3.0f, 0);
		rightPylonClosed.transform.position = new Vector3 (rightX, inSceneY -3.0f, 0);
	}

	public void resetCheckValues(){
		slotOneSuccess.resetSuccessBool ();
		slotTwoSuccess.resetSuccessBool ();
		andSuccess.resetSuccessBool ();
	}


	public void resetSlots(){
		replacementTiles = GameObject.FindGameObjectsWithTag ("ReplaceTile");

		foreach (GameObject repTile in replacementTiles) {
			Destroy (repTile);
		}
	}

	public void resetTiles(){
		for (int i = 0; i < arrayTiles.Length; i++) {
			arrayTiles[i].GetComponent<TileDrag>().onReset();
		}
	}

	public void resetActive(){
		for (int i = 0; i < arrayTiles.Length; i++) {
			arrayTiles[i].gameObject.SetActive(true);
		}
	}
}
