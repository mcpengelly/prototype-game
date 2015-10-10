using UnityEngine;
using System.Collections;

public class PongAI : MonoBehaviour {
	//State Machine:
	//checks as seperate behaviour for when the ball is within range or out of range.
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
	private Vector2 currentPos;
	private Vector2 startPos;
	//getters for member variables
	public State getState() { return currentState; }
	public State getPrevState() { return previousState; }
	
	public void SetState(State newState) { 
		previousState = currentState;
		currentState = newState;
		//print("Exit: " + previousState.ToString() + " State, Enter: " + currentState.ToString() + " State.");
	}
	
	void Awake() {
		startPos = transform.position;
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
		//while the game isnt paused or gameover update ai
		while (true) {
			pongBall = GameObject.FindGameObjectWithTag ("ball");

			if(pongBall != null){
				float pongBallYPos = pongBall.transform.position.y;

				switch(getState ()) {
					case State.Init: 
					break;

					case State.Wandering:
					currentPos = transform.position;
					float randomYDist = Random.Range (-5.0f, 5.0f);
					float tryDistance = transform.position.y + randomYDist;
					//add randomyYDistance to current position to check if it would be passed the boundry.
					//if it would set the boundry too high or low, flip the sign of randomYDist
					if(tryDistance <= 16.0f && tryDistance >= -10.0f){ // 
						targetPos = new Vector2(currentPos.x, currentPos.y + randomYDist);
						yield return StartCoroutine(MoveObject(currentPos, targetPos, 0.5f));
					} else { 
						yield return StartCoroutine(MoveObject(currentPos, startPos, 1.0f));
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
}