﻿using UnityEngine;
using System.Collections;

//placed on the goal/nets to handle scoring
public class PointTrigger : MonoBehaviour {
	GameObject scoreManager;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
		scoreManager = GameObject.FindWithTag ("ScoreManager");
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag("ball")) {
			collider.SendMessage("resetBall", 0.5f, SendMessageOptions.RequireReceiver);

			//check which net was scored on. by checking this gameobject and sending message with argument
			if (this.gameObject.CompareTag("CPU net")) {
				scoreManager.SendMessage("checkWhoScored", "PlayerGoal", SendMessageOptions.RequireReceiver);
			} else if (this.gameObject.CompareTag("Player net")) {
				scoreManager.SendMessage("checkWhoScored", "CPUGoal", SendMessageOptions.RequireReceiver);
			} else {

			}
		}
	}
}