using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PongBall : MonoBehaviour {

	private Rigidbody2D rb;
	public float ballInitialVelocity = 600;
	public Text countPlayerScore;
	public Text countCPUScore;
	private int playerScore = 0;
	private int cpuScore = 0;

	// Starts ball moving. edited so that the ball starts off with an initial velocity but to a random direction - towards player vs towards cpu
	void Start() {
		countPlayerScore.text = "Your Score: " + playerScore.ToString ();
		countCPUScore.text = "Enemy Score: " + cpuScore.ToString ();
		rb = GetComponent<Rigidbody2D> ();
		float random = Random.Range (0.0f, 100.0f);
		if (random < 50.0f) {
			rb.AddForce (new Vector2 (ballInitialVelocity, ballInitialVelocity));
		} else {
			rb.AddForce(new Vector2(-ballInitialVelocity, -ballInitialVelocity));
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag ("CPU net")) {
			playerScore += 1;
			countPlayerScore.text = "Your Score: " + playerScore.ToString ();
		} else if (collider.gameObject.CompareTag ("Player net")) {
			cpuScore += 1;
			countCPUScore.text = "Enemy Score: " + cpuScore.ToString ();
		}
	}
}
	// trying to make a method that changes the velocity of the ball depending 
	//which angle it collides with paddles... seems ball slows down otherwise?
	//void onCollisionEnter2D(Collision2D deflect) {
	//	rb = GetComponent<Rigidbody2D> ();
	//	if (deflect.collider.CompareTag ("Player") || deflect.collider.CompareTag ("CPU")) {
	//		Vector2 speedY = rb.velocity;
	//		speedY.y = speedY.y/2.0f;
	//		rb.velocity = speedY;
	//	}
	//}
//}

