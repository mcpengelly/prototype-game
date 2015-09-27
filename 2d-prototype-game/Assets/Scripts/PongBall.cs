using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//TODO: refactor this class into another ScoreManager Behaviour script
public class PongBall : MonoBehaviour {

	public Text countPlayerScore;
	public Text countCPUScore;
	public Text playerWin;
	public float ballInitialVelocity = 600;

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
		resetVelocity ();
		transform.position = startPos;
		sendRandomDirection();
	}
	IEnumerator bufferWait() {
		Debug.Log("Before Waiting 2 seconds");
		yield return 0; //new WaitForSeconds(2);
		Debug.Log("After Waiting 2 Seconds");
	}
	
	private void checkWhoScored(Collider2D collider) {
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
	private void init() {
		rb = this.GetComponent<Rigidbody2D> ();
		startPos = transform.position; // establish start position
		//StartCoroutine (bufferWait ());
	}
	private void sendRandomDirection () {
		float random = Mathf.Floor(Random.Range (0, 2));
		if (random < 1) {
			print("randNum:" + random);
			rb.AddForce (new Vector2 (ballInitialVelocity, ballInitialVelocity));
		} else {
			print("randNum:" + random);
			rb.AddForce(new Vector2(-ballInitialVelocity, -ballInitialVelocity));
		}
	}
	private void resetVelocity() {
		Vector2 v = rb.velocity;
		v.y = 0;
		v.x = 0;
		rb.velocity = v;
	}
	private void updateUI () {
		countPlayerScore.text = "Your Score: " + playerScore.ToString ();
		countCPUScore.text = "Enemy Score: " + cpuScore.ToString ();
	}
}