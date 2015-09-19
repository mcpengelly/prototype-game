using UnityEngine;
using System.Collections;


public class PongAI : MonoBehaviour {

	//state machine
	//create a circle collider around the cpu paddle that detects if the ball is within "range"
	//make the paddle behave a particular way when it is in range or out of range.
	//allow for this class to be extensible, in this case. able to incorperate more states. ie: boosting or something.
	private float minY = 0.0f; // Bottom of the screen 
	private float maxY = 0.0f;

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

	//getters for member variables
	public State getState() { return currentState; }
	public State getPrevState() { return previousState; }
	
	public void SetState(State newState) { 
		previousState = currentState;
		currentState = newState;
		print("Exiting: " + previousState.ToString() + " State"
		      + " ... " 
		      + "Entering: " + currentState.ToString() + " State");
	}
	
	void Awake() {
		SetState(State.Wandering);
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

	//need to refactor this out of update. or at least fire off a Coroutine to do the movement parts
	//paddle currently tries to move to a new random position every update frame
	//need to make the update trigger a movement that waits till its movement is completed (or until blocking state is trigged)
	//also need to make sure paddle have limits on its y axis.
	void Update() {
		pongBall = GameObject.FindGameObjectWithTag("ball");

		if(pongBall != null) {
			float pongBallYPos = pongBall.transform.position.y;
			int randIncrement = Random.Range(-3,3);

			if (getState() == State.Wandering) {
				targetPos = new Vector2(transform.position.x, transform.position.y + randIncrement);

			} else if (getState() == State.Blocking) {
				//move towards ball
				targetPos = new Vector2(transform.position.x, pongBallYPos);
			}
		}

		transform.position = Vector2.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
	}
}