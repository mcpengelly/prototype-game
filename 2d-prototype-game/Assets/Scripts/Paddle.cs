using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public float paddleSpeed = 1;
	private Vector3 playerPos = new Vector3(0, -9.5f, 0);


	
	// Update is called once per frame
	void Update () 
	{
		float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
		
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			
			if (touch.position.x > Screen.width/2)
			{
				xPos = transform.position.x +  paddleSpeed;
			}else{
				xPos = transform.position.x - paddleSpeed;
			}
			//xPos = -7.5f + 15 * touch.position.x/Screen.width;
			
		}
		playerPos = new Vector3 (Mathf.Clamp (xPos, -8f, 8f), -9.5f, 0f);
		transform.position = playerPos;
	}
}
