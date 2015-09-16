using UnityEngine;
using System.Collections;


public class PointTrigger : MonoBehaviour {

	//send a message to game manager to increment score?


	void Start() {

	}


	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag("ball")) {
			string netName = transform.name;
			//MatchManager.Score(netName);
			collider.gameObject.SendMessage("resetBall",0.5f, SendMessageOptions.RequireReceiver);
		}
		Debug.Log (collider);
		//GameObject.Destroy(collider.gameObject);
	}
}
