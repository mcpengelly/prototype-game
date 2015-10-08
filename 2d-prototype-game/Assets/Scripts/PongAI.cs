﻿using UnityEngine;
using System.Collections;


public class PongAI : MonoBehaviour {

	//state machine
	//create a circle collider around the cpu paddle that detects if the ball is within "range"
	//make the paddle behave a particular way when it is in range or out of range.
	//allow for this class to be extensible, in this case. able to incorperate more states. ie: boosting or something.
	
	public enum State
	{
		Init,
		Wandering,
		Blocking
	}

	public float speed = 1.0f;
	public GameObject pongBall;
	
	private State currentState = State.Init;
	private State previousState;
	
	private Vector2 targetPos;
	private Vector2 currentPosition;
	//getters for member variables
	public State getState() { return currentState; }
	public State getPrevState() { return previousState; }
	
	public void SetState(State newState) { 
		previousState = currentState;
		currentState = newState;
		//print("Exit: " + previousState.ToString() + " State, Enter: " + currentState.ToString() + " State.");
	}
	
	void Awake() {
		SetState(State.Wandering);
	}
	void Start() {
		StartCoroutine ("aiUpdate");
	}

	void OnTriggerEnter2D (Collider2D other) {
		if(other.CompareTag("ball")) {
			SetState(State.Blocking);
		}
	}
	void OnTriggerExit2D (Collider2D other) {
		if(other.CompareTag("ball")) {
			SetState(State.Wandering);
		}
	}

	IEnumerator MoveObject(Vector3 source, Vector3 target, float overTime)
	{
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			transform.position = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		transform.position = target;
	}

	IEnumerator aiUpdate() {
		//TODO:run repeatedly while game state != gameover
		while (true) {
			pongBall = GameObject.FindGameObjectWithTag ("ball");

			if(pongBall != null){
				float pongBallYPos = pongBall.transform.position.y;

				switch(getState ()) {
					case State.Init: 
					break;

					case State.Wandering:
					float randomYDist = Random.Range (-10, 10);
					float tryDistance = currentPosition.y + randomYDist;
					currentPosition = transform.position;
					//add randomyYDistance to current position to check if it would be passed the boundry.
					//if it would set the boundry too high or low, flip the sign of randomYDist
					if(tryDistance < 10 && tryDistance > -10){
						print("tryDistance1: " + tryDistance);
						targetPos = new Vector2(currentPosition.x, currentPosition.y + randomYDist);
						yield return StartCoroutine(MoveObject(currentPosition, targetPos, 1.5f));
					} else { // TODO: make the paddle wander back if the tryDistance is too high or low.
						print("tryDistance2: " + tryDistance);
						targetPos = new Vector2(currentPosition.x, currentPosition.y + (randomYDist * -1.0f));
						yield return StartCoroutine(MoveObject(currentPosition, targetPos, 1.5f));
					}
					break;

					case State.Blocking:
					targetPos = new Vector2 (transform.position.x, pongBallYPos);
					transform.position = Vector2.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
					break;
				}
			}
			yield return 0;
		}
	}
	private void wanderOneTime () {

	}

}