using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

	//Game manager should:
	//interface with all other objects to pause their state
	//register puased/menu/playing messages
	//wait few seconds before spawning new balls after round is over

	public static int playerScore = 0;
	public static int cpuScore = 0;
	public GUISkin layout;
	private bool isPaused;

	void Start() {

	}

	void Awake () {
		isPaused = false;
		DontDestroyOnLoad (this);
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.CompareTag ("ball")) {
			print ("Ball has exited playable zone. Reset ball.");
			collider.gameObject.SendMessage("resetBall", 0.5f, SendMessageOptions.RequireReceiver);
		}
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