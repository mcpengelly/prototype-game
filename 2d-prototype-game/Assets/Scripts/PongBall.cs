using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PongBall : MonoBehaviour {
	public float initialVelocity = 600;

	private Rigidbody2D rb;
	private Vector2 startPos;

	public enum State
	{
		Init,
		InPlay,
		OutOfPlay
	}

	private State currentState = State.Init;
	private State previousState;

	public State getState() { return currentState; }
	public State getPrevState() { return previousState; }
	
	public void SetState(State newState) { 
		previousState = currentState;
		currentState = newState;
		//print("Exit: " + previousState.ToString() + " State, Enter: " + currentState.ToString() + " State.");
	}
	
	void Start () {
		init ();
	}
	private void init() {
		rb = this.GetComponent<Rigidbody2D> ();
		startPos = transform.position; // establish start position
		StartCoroutine (checkStateInterval());
		SetState (State.InPlay);
	}
	
	//debug controls
	void Update() {
		if (transform.position.x <= -21 || transform.position.x >= 21) {
			SetState (State.OutOfPlay);
		} else {
			SetState (State.InPlay);
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			resetVelocity();
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			resetBall();
		}	
	}
	
	void resetBall() {
		resetVelocity ();
		transform.position = startPos;
	}

	private void resetVelocity() {
		Vector2 v = rb.velocity;
		v.y = 0;
		v.x = 0;
		rb.velocity = v;
	}
	
	private void sendRandomDirection () {
		float random = Mathf.Floor(Random.Range (0, 2));
		if (random < 1) {
			rb.AddForce (new Vector2 (initialVelocity, initialVelocity));
		} else {
			rb.AddForce(new Vector2(-initialVelocity, -initialVelocity));
		}
	}	
	//displays the state, then waits for 3 seconds.
	IEnumerator checkStateInterval() {
		while (true) {
			switch (getState ()) {
			case State.Init:
				print ("init state");
				yield return new WaitForSeconds (3.0f);
				sendRandomDirection ();
				break;
			case State.InPlay:
				print ("ball in play");
				break;
			case State.OutOfPlay: 
				print ("ball NOT in play");
				yield return new WaitForSeconds (3.0f);
				sendRandomDirection();
				break;
				
			}
			yield return 0;
		}
	}

}