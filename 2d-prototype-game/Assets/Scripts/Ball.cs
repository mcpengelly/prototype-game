using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float ballInitialVelocity = 600;
	private Rigidbody rb;
	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
	}
	
	// Update is called once per frame
	void Update () {

	}
}
