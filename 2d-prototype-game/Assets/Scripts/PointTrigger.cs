using UnityEngine;
using System.Collections;


public class PointTrigger : MonoBehaviour {

	GameObject scoreManager;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
		scoreManager = GameObject.FindWithTag ("ScoreManager");
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag("ball")) {
			collider.SendMessage("resetBall", 0.5f, SendMessageOptions.RequireReceiver);

			if (collider.gameObject.CompareTag ("CPU net")) {
				scoreManager.SendMessage("incrementPlayerScore", SendMessageOptions.RequireReceiver);
			} else {
				scoreManager.SendMessage("incrementCpuScore", SendMessageOptions.RequireReceiver);
			}
		}
	}
}