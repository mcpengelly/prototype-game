using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PongBall : MonoBehaviour {

	public float initialVelocity = 600;

	private Rigidbody2D rb;
	private Vector2 startPos;

	void Awake () {
		init ();
	}
	void Start () {
		sendRandomDirection ();
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

	//debug control
	void Update() {
		//stop ball with G
		if (Input.GetKeyDown (KeyCode.G)) {
			resetVelocity();
		}
		//reset ball with H
		if (Input.GetKeyDown (KeyCode.H)) {
			resetBall();
		}

	}
}