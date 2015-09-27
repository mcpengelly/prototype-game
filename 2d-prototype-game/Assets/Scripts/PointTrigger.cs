using UnityEngine;
using System.Collections;


public class PointTrigger : MonoBehaviour {

	void Start() {

	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag("ball")) {
			string netName = transform.name;
			collider.gameObject.SendMessage("resetBall",0.5f, SendMessageOptions.RequireReceiver);
		}
		Debug.Log (collider);
		//GameObject.Destroy(collider.gameObject);
	}
}
