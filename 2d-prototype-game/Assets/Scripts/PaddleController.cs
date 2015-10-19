using UnityEngine;
using System.Collections;

//main controls for user
public class PaddleController : MonoBehaviour {

	public Rigidbody2D rb2D;
	public float paddleSpeed = 1.0f;
	public float maxHeight;
	public float minHeight;
	private Vector2 startPos;
	public Transform ballPos;
	Vector2 rawPosition;

	void Awake () {
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
		rb2D.MovePosition (targetPosition);
	}
}