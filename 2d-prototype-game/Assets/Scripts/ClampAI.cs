using UnityEngine;
using System.Collections;

public class ClampAI : MonoBehaviour {
	public float yMin = -10.0f;
	public float yMax = 10.0f;
	static PongAI pongAI = new PongAI();

	
	// Update is called once per frame
	void Update () {
		//transform.Translate(Input.acceleration.x * pongAI.speed * Time.deltaTime, Input.acceleration.y * pongAI.speed * Time.deltaTime, 0);
		transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, yMin, yMax));
	}
}
