using UnityEngine;
using System.Collections;

public class PongBall : MonoBehaviour {

	public float ballInitialVelocity = 600;
	private Rigidbody2D rb;
	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
	}
	
	// Update is called once per frame
	void Update () {

	}
}
