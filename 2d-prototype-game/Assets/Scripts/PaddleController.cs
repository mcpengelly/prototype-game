using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	public float paddleSpeed = 1;
	public Vector3 currentPosition;
	private Vector2 startPosition;
	private Vector2 playerPos = new Vector2(0, 0);

	void Awake () {
		startPosition = transform.position;
	}
	void Update () {
		currentPosition = transform.position;
		float yPos = transform.position.y + (Input.GetAxis("Vertical") * paddleSpeed);

		if(Input.GetKeyDown(KeyCode.W)) {
			yPos = transform.position.y + paddleSpeed;
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			yPos = transform.position.y - paddleSpeed;
		}
		playerPos = new Vector2(transform.position.x, yPos);
		transform.position = playerPos;
	}

//	// Update is called once per frame
//	void Update () 
//	{
//		float xPos = transform.position.y + (Input.GetAxis("Horizontal") * paddleSpeed);
//		
//		if (Input.touchCount == 1) {
//			Touch touch = Input.GetTouch(0);
//			
//			if (touch.position.x > Screen.width/2)
//			{
//				xPos = transform.position.x + paddleSpeed;
//			}else{
//				xPos = transform.position.x - paddleSpeed;
//			}
//			//xPos = -7.5f + 15 * touch.position.x/Screen.width;
//			
//		}
//		//playerPos = new Vector3 (Mathf.Clamp (xPos, -8f, 8f), -9.5f, 0f);
//		transform.position = playerPos;
//	}
}
