using UnityEngine;
using System.Collections;

public class MatchManager : MonoBehaviour {

	//Game manager should:
	//track scores
	//track ball position?? 
	//affect timescale
	//interface with all other objects to pause their state
	//register puased/menu/playing messages

	public bool isPaused;
	private int playerScore = 0;
	private int cpuScore = 0;

	void Awake () {
		isPaused = false;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			TogglePause ();
		}
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
	private void incrementScore(int currentScore) {

	}
}
