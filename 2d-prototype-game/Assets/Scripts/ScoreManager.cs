using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//manages score and related UI between the player and the CPU player
public class ScoreManager : MonoBehaviour {
	public static int playerScore;
	public static int cpuScore;

	public Text playerScoreUI;
	public Text cpuScoreUI;
	public Text gameMessageUI;
	
	private Vector3 gameOverCameraPos;
	public Camera mainCam;

	const int MAX_SCORE = 7;

	void Awake() {
		init ();
	}

	void init(){
		gameOverCameraPos = new Vector3 (mainCam.transform.position.x, 60, mainCam.transform.position.z);
		playerScore = 0;
		cpuScore = 0;
		gameMessageUI.text = "";
		updateUI ();
	}
	
	//method called when recieved a message from: PointTrigger.cs
	void checkWhoScored(string message) {
		if(message == "PlayerGoal"){
			playerScore++;
		} else if (message == "CPUGoal") {
			cpuScore++;
		}
		updateUI ();
		checkWinner ();
	}

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

	public static int getPlayerScore() {
		return playerScore;
	}

	public static int getCpuScore() {
		return cpuScore;
	}
}
