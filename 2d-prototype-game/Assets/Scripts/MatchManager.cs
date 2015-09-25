using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

	//Game manager should:
	//track scores
	//track ball position?? 
	//affect timescale
	//interface with all other objects to pause their state
	//register puased/menu/playing messages
	//wait few seconds before spawning new balls after round is over.

	public bool isPaused;
	public static int playerScore = 0;
	public static int cpuScore = 0;
	public GUISkin layout;
	Transform ball;

	void Start() {
		ball = GameObject.FindGameObjectWithTag ("ball").transform;
	}

	void Awake () {
		isPaused = false;
		DontDestroyOnLoad (this);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			TogglePause ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel(0);
		}

		if (Input.GetKey("escape"))
			Application.Quit();

	}

	public void TogglePause () {
		if (isPaused) {
			UnPauseGame ();
		} else {
			PauseGame ();
		}
	}

	private void PauseGame () {
		isPaused = true;
		Time.timeScale = 0.0f;
	}
	private void UnPauseGame () {
		isPaused = false;
		Time.timeScale = 1.0f;
	}

	// called from PointTrigger script on each the net colliders, increments score accordingly
	public static void Score(string netID) {
		if (netID == "P_CPUnet") {
			playerScore++;
		} else {
			cpuScore++;;
		}
	}
	
}
// this method i'm trying to create a different type of gui.. for reset 
	//void onGUI() {
	//	GUI.skin = layout;
	//	GUI.Label (new Rect (Screen.width / 2 - 150 - 12, 20, 100, 100), "" + playerScore);
	//	GUI.Label (new Rect (Screen.width / 2 + 150 + 12, 20, 100, 100), "" + cpuScore);
		
	//	if (GUI.Button (new Rect (Screen.width / 2 - 60, 35, 120, 53), "RESET")) {
	//		playerScore = 0;
	//		cpuScore = 0;
	//		ball.gameObject.SendMessage ("resetBall", .5f, SendMessageOptions.RequireReceiver);
	//	}
	//	
	//	if (playerScore == 10) {
	//		GUI.Label (new Rect (Screen.width / 2 - 150, 200, 2000, 1000), "YOU WIN!");
	//	} else if (cpuScore == 10) {
	//		GUI.Label (new Rect (Screen.width / 2 - 150, 200, 2000, 1000), "YOU LOSE!");
	//		ball.gameObject.SendMessage ("hasWon", null, SendMessageOptions.RequireReceiver);
	//	}
	//}







