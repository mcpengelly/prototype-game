using UnityEngine;
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
		print("Exit: " + previousState.ToString() + " State, Enter: " + currentState.ToString() + " State.");
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
	//need to refactor this out of update. or at least fire off a Coroutine to do the movement parts
	//paddle currently tries to move to a new random position every update frame
	//need to make the update trigger a movement that waits till its movement is completed (or until blocking state is trigged)
	//also need to make sure paddle have limits on its y axis.
	//could use a coroutine that runs every 2 seconds ? while wandering. picking random spots and moving to them
	IEnumerator aiUpdate() {
		while (true) {
			pongBall = GameObject.FindGameObjectWithTag ("ball");


			if(pongBall != null){
				float pongBallYPos = pongBall.transform.position.y;

				switch(getState ()) {
					case State.Init: 
					break;

					case State.Wandering:
					int randomYDist = Random.Range (-5, 5);
					float tryDistance = currentPosition.y + randomYDist;

					if(tryDistance < 10 && tryDistance > -10){
						currentPosition = transform.position;
						targetPos = new Vector2(currentPosition.x, currentPosition.y + randomYDist);
						yield return StartCoroutine(MoveObject(currentPosition, targetPos, 0.5f));
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