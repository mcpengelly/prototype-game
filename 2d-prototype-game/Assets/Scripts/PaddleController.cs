using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	//player controlled paddle

	public Rigidbody2D rb2D;
	public float paddleSpeed = 1.0f;
	public float maxHeight;
	public float minHeight;
	private Vector2 startPos;
	public Transform ballPos;
	Vector2 rawPosition;

	void Awake () {
		//get handle on rigidbody2d of this object so it can be moved in update
		rb2D = GetComponent<Rigidbody2D>();
	}
	void Start () {
		startPos = new Vector2(transform.position.x, transform.position.y);

	}

	void Update () {
		rawPosition = transform.position;
		Vector2 targetPosition = new Vector2 (transform.position.x, transform.position.y);

		//setup maxHeight range from inspector
		if (Input.GetKey (KeyCode.W) && transform.position.y <= startPos.y + maxHeight) {
			targetPosition = new Vector2 (rawPosition.x, rawPosition.y + paddleSpeed);

		} else if (Input.GetKey (KeyCode.S) && transform.position.y >= startPos.y + minHeight) {
			targetPosition = new Vector2 (rawPosition.x, rawPosition.y - paddleSpeed);

		}

		//move the paddle
		rb2D.MovePosition (targetPosition);
		speedBoost (targetPosition);
	}

	// just messing around.. trying to add a small speed boost 
	//-- used same strategy as above but using same button for both directions.. didn't work
	void  speedBoost(Vector2 targetPosition) {
		if (Input.GetKey (KeyCode.Space) && transform.position.y <= startPos.y + maxHeight) {
			rb2D.AddRelativeForce (rawPosition, ForceMode2D.Impulse);
			targetPosition = new Vector2 (transform.position.x, transform.position.y + paddleSpeed * 2);
		} 
		else if (Input.GetKey (KeyCode.Space) && transform.position.y >= startPos.y + minHeight) {
			rb2D.AddRelativeForce (rawPosition, ForceMode2D.Impulse);
			targetPosition = new Vector2 (transform.position.x, transform.position.y - paddleSpeed * 2);
		}
			rb2D.MovePosition (targetPosition);
	}
}