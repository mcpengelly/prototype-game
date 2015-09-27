using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	//move to UI/Score manager class
	public Text playerScoreUI;
	public Text cpuScoreUI;
	public Text gameMessageUI;

	public static int playerScore = 0;
	public static int cpuScore = 0;

	//when I recieve a message that a net has been scored on
//	void netScoredOn(Collider2D collider) {
//		checkWhoScored (collider);
//	}
	
	private void checkWhoScored(Collider2D collider) {
		if (collider.gameObject.CompareTag ("CPU net")) {
			playerScore += 1;
			playerScoreUI.text = "Your Score: " + playerScore.ToString ();
			if (playerScore == 10) {
				gameMessageUI.text = "You Win!!";
				// Application.Quit()
			}
			
		} else if (collider.gameObject.CompareTag ("Player net")) {
			cpuScore += 1;
			cpuScoreUI.text = "Enemy Score: " + cpuScore.ToString ();
			if (cpuScore == 10) {
				gameMessageUI.text = "You Lose!!";
				// Application.Quit() <<< to stop game?
			}
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
	public static void incrementPlayerScore() {
		playerScore += 1;
	}
	public static void incrementCpuScore() {
		cpuScore += 1;
	}
}
