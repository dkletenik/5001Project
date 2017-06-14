﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayMovingObjectTrigger : MonoBehaviour {

	public PlayerMovement player;
	public JITScript triggerCollider;
	public GameObject objToMove; // object to be moved

	//points that object will move between back and forth
	public Transform endPoint; // ending point
	public float moveSpeed; // how fast the object moves
	private Vector3 currentTarget; // the current point it's going to
	public Vector3 initialPosition; // initial pos of the object

	// Use this for initialization
	void Start () {
		currentTarget = endPoint.transform.position;
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//if it has been deactivated because the player touched it
		if (!triggerCollider.gameObject.activeSelf) { 
			//move the barrier
			moveObjectOneWay ();
		}
	}

	public void moveObjectOneWay(){
		// uses move towards to let the object move towards it's goal
		if (objToMove != null) {
			objToMove.transform.position = Vector3.MoveTowards (objToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);
		}
	}

	public void resetPosition(){
		transform.position = initialPosition;
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			//set the flag
			other.gameObject.transform.parent = this.transform;
			other.GetComponent<PlayerMovement> ().zoomOut ();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			other.gameObject.transform.parent = null;
		}
	}
	
}
