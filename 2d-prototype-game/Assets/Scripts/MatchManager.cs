using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Manages game pausing, resetting and quitting
public class MatchManager : MonoBehaviour {
	private static bool isPaused;
	
	void Awake () {
		isPaused = false;
		DontDestroyOnLoad (this);
	}
	
	void OnTriggerExit2D(Collider2D collider) {
		if (collider.CompareTag ("ball")) {
			print ("Ball has exited playable zone. Resetting ball.");
			collider.gameObject.SendMessage("resetBall", 0.5f, SendMessageOptions.RequireReceiver);
		}
	}

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
	
	public static void TogglePause () {
		if (isPaused) {
			UnPauseGame ();
		} else {
			PauseGame ();
		}
	}
	
	private static void PauseGame () {
		isPaused = true;
		Time.timeScale = 0.0f;
	}

	private static void UnPauseGame () {
		isPaused = false;
		Time.timeScale = 1.0f;
	}
}