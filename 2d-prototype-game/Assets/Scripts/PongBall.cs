using UnityEngine;
using System.Collections;

public class PongBall : MonoBehaviour {
	private Rigidbody2D rb;
	public float ballInitialVelocity = 600;

	// Starts ball moving. edited so that the ball starts off with an initial velocity but to a random direction - towards player vs towards cpu
	void Start() {
		rb = GetComponent<Rigidbody2D> ();
		float random = Random.Range (0.0f, 100.0f);
		if (random < 50.0f) {
			rb.AddForce (new Vector2 (ballInitialVelocity, ballInitialVelocity));
		} else {
			rb.AddForce(new Vector2(-ballInitialVelocity, -ballInitialVelocity));
		}
	}
}
	// trying to make a method that changes the velocity of the ball depending which angle it collides with paddles... seems ball slows down otherwise?
	//void onCollisionEnter2D(Collision2D deflect) {
	//	if (deflect.collider.CompareTag ("Player") || deflect.collider.CompareTag ("CPU")) {
	//		var speedY = Rigidbody2D.velocity;
	//		speedY.y = (speedY.y/2.0f));
	//		Rigidbody2D.velocity = speedY;
	//	}
	//}

// Your previous code segment
//  public float ballInitialVelocity = 600;
//private Rigidbody2D rb;
// Use this for initialization
//void Awake () {
//  rb = GetComponent<Rigidbody2D> ();
// rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
//}

// Update is called once per frame
//void Update () {

//}

