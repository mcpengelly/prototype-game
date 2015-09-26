using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//TODO: refactor this class into another ScoreManager Behaviour script
public class PongBall : MonoBehaviour {

	private Rigidbody2D rb;
	public float ballInitialVelocity = 600;
	public Text countPlayerScore;
	public Text countCPUScore;
	private int playerScore = 0;
	private int cpuScore = 0;
	private Vector2 ballOriginalPos;
	public Text playerWin = "";

	void Awake () {
		init ();
	}
	private void init() {
		playerWin.text = "";
		//StartCoroutine (bufferWait ());
		ballOriginalPos = transform.position; // establish start position
	}
	void Start () {
		sendRandomDirection ();
	}
	private void sendRandomDirection () {
		rb = GetComponent<Rigidbody2D> ();
		float random = Mathf.Floor(Random.Range (0, 2));
		if (random < 1) {
			rb.AddForce (new Vector2 (ballInitialVelocity, ballInitialVelocity));
		} else {
			rb.AddForce(new Vector2(-ballInitialVelocity, -ballInitialVelocity));
		}
	}
	private void updateUI () {
		countPlayerScore.text = "Your Score: " + playerScore.ToString ();
		countCPUScore.text = "Enemy Score: " + cpuScore.ToString ();
	}

	// coroutine started from Start
	IEnumerator bufferWait() {
		Debug.Log("Before Waiting 2 seconds");
		yield return 0; //new WaitForSeconds(2);
		Debug.Log("After Waiting 2 Seconds");
	}

	//called from game manager, checks who has won.. called in onGUI supposed to print out "YOU WON"
	void hasWon() {
		Vector2 v = GetComponent<Rigidbody2D> ().velocity;
		v.y = 0;
		v.x = 0;
		GetComponent<Rigidbody2D> ().velocity = v;

		gameObject.transform.position = new Vector2 (0, 0);
	}

	//called from game manager, resets ball pos after scoring
	void resetBall() {	
		Vector2 v = GetComponent<Rigidbody2D> ().velocity;
		v.y = 0;
		v.x = 0;
		GetComponent<Rigidbody2D> ().velocity = v;
		gameObject.transform.position = ballOriginalPos;
		//Start (); // refactor to use custom init function isntead
	}
	void checkWhoScored(Collider2D collider) {
		if (collider.gameObject.CompareTag ("CPU net")) {
			playerScore += 1;
			countPlayerScore.text = "Your Score: " + playerScore.ToString ();
			if (playerScore == 10) {
				playerWin.text = "You Win!!";
				// Application.Quit()
			}
			
		} else if (collider.gameObject.CompareTag ("Player net")) {
			cpuScore += 1;
			countCPUScore.text = "Enemy Score: " + cpuScore.ToString ();
			if (cpuScore == 10) {
				playerWin.text = "You Lose!!";
				// Application.Quit() <<< to stop game?
			}
		}
		updateUI ();
		displayWinner ();
	}
	private void displayWinner () {
		if (cpuScore >= 10) {
			playerWin.text = "You Lose!";
		} else if (playerScore >= 10 ) {
			playerWin.text = "You Win!";
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		checkWhoScored (collider);
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

