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
		OutPlay
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
		StartCoroutine (Wait ());
		init ();
		SetState (State.InPlay);
		sendRandomDirection ();
	}

	//debug control
	void Update() {

		// check if ball is within the two nets... if past change state
		if (transform.position.x <= -18 || transform.position.x >= 18) {
			print ("in checker");
			SetState (State.OutPlay);
		}

		//stop ball with G
		if (Input.GetKeyDown (KeyCode.G)) {
			resetVelocity();
		}
		//reset ball with H
		if (Input.GetKeyDown (KeyCode.H)) {
			resetBall();
		}
		
	}


	//Method called when message recieved from another class
	//improve by adding a countdown coroutine?
	void resetBall() {
		resetVelocity ();
		transform.position = startPos;
		sendRandomDirection();
	}

	private void init() {
		rb = this.GetComponent<Rigidbody2D> ();
		startPos = transform.position; // establish start position
	}
	private void sendRandomDirection () {
		float random = Mathf.Floor(Random.Range (0, 2));
		if (random < 1) {
			rb.AddForce (new Vector2 (initialVelocity, initialVelocity));
		} else {
			rb.AddForce(new Vector2(-initialVelocity, -initialVelocity));
		}


	}

	private void resetVelocity() {

		Vector2 v = rb.velocity;
		v.y = 0;
		v.x = 0;
		rb.velocity = v;
	}

	IEnumerator Wait() {
		print (getState ());
		switch (getState ()) {
		case State.Init: 
				yield return new WaitForSeconds (3.0f);
				break;
		case State.InPlay: break;
		case State.OutPlay: 
				print ("in outplay state");
				yield return new WaitForSeconds (3.0f);
				break;

			}
		yield return 0;
	}
}