using UnityEngine;
using System.Collections;


public class PointTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag("ball")) {
			collider.gameObject.SendMessage("resetBall",0.5f, SendMessageOptions.RequireReceiver);
		}
		//GameObject.Destroy(collider.gameObject);
	}
}
