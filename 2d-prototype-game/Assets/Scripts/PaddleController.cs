using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	//player controlled paddle

	public Rigidbody2D rb2D;
	public float paddleSpeed = 1.0f;
	public float maxHeight;
	public float minHeight;
	private Vector2 startPos;

	void Awake () {
		//get handle on rigidbody2d of this object so it can be moved in update
		rb2D = GetComponent<Rigidbody2D>();
	}
	void Start () {
		startPos = new Vector2(transform.position.x, transform.position.y);

	}
	//FixedUpdate runs every game tick, but is used for physics objects. 
	void FixedUpdate () {
		Vector2 rawPosition = transform.position;
		Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y);

		//if player hits W and the y position is under 2.5f then set target position higher
		if(Input.GetKey(KeyCode.W)&& transform.position.y <= startPos.y + maxHeight) {
			targetPosition = new Vector2(rawPosition.x, rawPosition.y + paddleSpeed);

		} else if (Input.GetKey(KeyCode.S) && transform.position.y >= startPos.y + minHeight) {
			targetPosition = new Vector2(rawPosition.x, rawPosition.y - paddleSpeed);

		}

		//move the paddle
		rb2D.MovePosition(targetPosition);
	}



	// Update is called once per frame
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
