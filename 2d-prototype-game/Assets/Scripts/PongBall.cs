using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//TODO: refactor this class into another ScoreManager Behaviour script
public class PongBall : MonoBehaviour {

	//move to UI/Score manager class
	public Text playerScoreUI;
	public Text cpuScoreUI;
	public Text gameMessageUI;
	//
	public float initialVelocity = 600;

	private Rigidbody2D rb;
	private Vector2 startPos;
	private int playerScore = 0;
	private int cpuScore = 0;

	void Awake () {
		init ();
	}
	void Start () {
		sendRandomDirection ();
	}
	void OnTriggerEnter2D(Collider2D collider) {
		checkWhoScored (collider);
	}

	//called from game manager, resets ball pos after scoring
	void resetBall() {
		print("resetBall, Triggered by pointTrigger");
		resetVelocity ();
		transform.position = startPos;
		sendRandomDirection();
	}
	
	private void checkWhoScored(Collider2D collider) {
		if (collider.gameObject.CompareTag ("CPU net")) {
			playerScore += 1;
			playerScoreUI.text = "Your Score: " + playerScore.ToString ();
			
		} else if (collider.gameObject.CompareTag ("Player net")) {
			cpuScore += 1;
			cpuScoreUI.text = "Enemy Score: " + cpuScore.ToString ();
		}
		updateUI ();
		displayWinner ();
	}
	private void displayWinner () {
		if (cpuScore >= 10) {
			gameMessageUI.text = "You Lose!";
		} else if (playerScore >= 10 ) {
			gameMessageUI.text = "You Win!";
		}
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
	private void updateUI () {
		playerScoreUI.text = "Your Score: " + playerScore.ToString ();
		cpuScoreUI.text = "Enemy Score: " + cpuScore.ToString ();
	}
}