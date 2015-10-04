using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//TODO: refactor this class into another ScoreManager Behaviour script
public class PongBall : MonoBehaviour {

	//move to UI/Score manager class
	public Text playerScoreUI;
	public Text cpuScoreUI;
	public Text gameMessageUI;
	public Text gameOverUI;
	public Camera cameraPoint;
	private int playerScore = 0;
	private int cpuScore = 0;
	//
	public float initialVelocity = 600;

	private Rigidbody2D rb;
	private Vector2 startPos;
	private Vector3 gameOverCameraPos;


	void Awake () {
		gameMessageUI.text = "";
		gameOverCameraPos= new Vector3(10, 60, -10);

		init ();
	}
	void Start () {
		sendRandomDirection ();
	}
	void OnTriggerEnter2D(Collider2D collider) {
		checkWhoScored (collider);
	}
	void onCollisionEnter2D(Collider2D collider) {
		//add relative force to ball on each collision with a paddle *not working atm*
		//rb.AddRelativeForce (transform.up * 10);
	}

	//Method called when message recieved from another class
	//improve by adding a countdown coroutine?
	void resetBall() {
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
		Vector3 temp = cameraPoint.transform.position;
		temp = new Vector3(10, 60, -10);
		if (cpuScore >= 1) {
			gameMessageUI.text = "You Lose!";
			Time.timeScale = 0.0f;
			gameOverUI.text = "Game Over, You Suck!";
			cameraPoint.transform.position = temp;

		} else if (playerScore >= 1 ) {
			gameMessageUI.text = "You Win!";
			Time.timeScale = 0.0f;
			gameOverUI.text = "You Win!";
			cameraPoint.transform.position = temp;
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