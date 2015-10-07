using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	//ScoreManager tracks score and displays/updates UI

	public Text playerScoreUI;
	public Text cpuScoreUI;
	public Text gameMessageUI;

	public static int playerScore = 0;
	public static int cpuScore = 0;
	
	public Camera mainCam;
	private Vector3 gameOverCameraPos;

	const int MAX_SCORE = 7;

	void Awake() {
		init ();
	}
	void init(){
		gameMessageUI.text = "";
		gameOverCameraPos = new Vector3 (10, 60, -10);
	}
	//TODO:refactor
	private void checkWinner () {
		if (cpuScore >= MAX_SCORE) {
			gameMessageUI.text = "You Lose!";
			mainCam.transform.position = gameOverCameraPos;
		} else if (playerScore >= MAX_SCORE ) {
			gameMessageUI.text = "You Win!";
			mainCam.transform.position = gameOverCameraPos;
		}
	}

	private void updateUI () {
		playerScoreUI.text = "Your Score: " + playerScore.ToString ();
		cpuScoreUI.text = "Enemy Score: " + cpuScore.ToString ();
	}

	// called from PointTrigger script on each the net colliders, increments score accordingly
	//getting tired. need to refactor this into skimpler solution.
	void incrementCpuScore() {
		cpuScore++;
		updateUI ();
		checkWinner ();
	}
	void incrementPlayerScore() {
		playerScore++;
		updateUI ();
		checkWinner ();
	}

	//static utility methods for accessing scores
	public static int getPlayerScore() {
		return playerScore;
	}
	public static int getCpuScore() {
		return cpuScore;
	}
}
