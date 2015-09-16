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

	//getters for member variables
	public State getState() { return currentState; }
	public State getPrevState() { return previousState; }
	
	public void SetState(State newState) { 
		previousState = currentState;
		currentState = newState;
		print("Exiting: " + previousState.ToString() + " State."
		      + " ... " 
		      + "Entering: " + currentState.ToString() + " State.");
	}
	
	void Awake() {
		pongBall = GameObject.FindGameObjectWithTag("ball");
		SetState(State.Wandering);
	}

	private Vector2 targetPos;

	//need to refactor this out of update. or at least fire off a Coroutine to do the movement parts
	void Update() {
		print(getState());
		pongBall = GameObject.FindGameObjectWithTag("ball");

		if(pongBall != null) {
			float pongBallYPos = pongBall.transform.position.y;

			if (getState() == State.Wandering) {
				//move up cuz why not
				targetPos = new Vector2(transform.position.x, transform.position.y + 0.1f);

			} else if (getState() == State.Blocking) {
				//move towards ball
				targetPos = new Vector2(transform.position.x, pongBallYPos);
			}
		}

		transform.position = targetPos;
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


}