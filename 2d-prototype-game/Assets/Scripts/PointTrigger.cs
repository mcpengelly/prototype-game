using UnityEngine;
using System.Collections;

public class PointTrigger : MonoBehaviour {

	//send a message to game manager to increment score?

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log (collider);
		GameObject.Destroy(collider, 0.1f);
	}
}
