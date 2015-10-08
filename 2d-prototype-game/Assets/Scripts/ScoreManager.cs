using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	//ScoreManager tracks score and displays/updates UI

	public Text playerScoreUI;
	public Text cpuScoreUI;
	public Text gameMessageUI;

	public static int playerScore;
	public static int cpuScore;
	
	public Camera mainCam;
	private Vector3 gameOverCameraPos;

	const int MAX_SCORE = 7;

	void Awake() {
		init ();
	}
	void init(){
		gameOverCameraPos = new Vector3 (10, 60, -10);
		playerScore = 0;
		cpuScore = 0;
		gameMessageUI.text = "";
		updateUI ();
	}
	//TODO:refactor
	private void checkWinner () {
		if (cpuScore >= MAX_SCORE) {
			gameMessageUI.text = "You Lose!";
			mainCam.transform.position = gameOverCameraPos;
			MatchManager.TogglePause();
		} else if (playerScore >= MAX_SCORE ) {
			gameMessageUI.text = "You Win!";
			mainCam.transform.position = gameOverCameraPos;
			MatchManager.TogglePause();
		}
	}

	private void updateUI () {
		playerScoreUI.text = "Your Score: " + playerScore.ToString ();
		cpuScoreUI.text = "Enemy Score: " + cpuScore.ToString ();
	}
	//method called by recieving a message from: PointTrigger.cs
	void checkWhoScored(string message) {
		if(message == "PlayerGoal"){
			playerScore++;
		} else if (message == "CPUGoal") {
			cpuScore++;
		}
		updateUI ();
		checkWinner ();
	}


	// called from PointT
	//static utility methods for accessing scores
	public static int getPlayerScore() {
		return playerScore;
	}
	public static int getCpuScore() {
		return cpuScore;
	}
}
