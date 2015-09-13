using UnityEngine;
using System.Collections;


public class PongAI : MonoBehaviour {

	public Transform ballPos; 
	public float speed = 1.0f;



	// AI doesn't move until ball is past x=5, more "human" effect. Fixed clamping. Ball still goes out of bounds sometimes.
	void Update (){
			ballPos = GameObject.FindGameObjectWithTag ("ball").transform;

		if (ballPos.position.x > 5) {

			transform.position = new Vector2 (transform.position.x, Mathf.Lerp(transform.position.y, ballPos.position.y, speed*Time.deltaTime));
			Vector2 temp = new Vector2 (transform.position.x, -3.5f);
			Vector2 temp2 = new Vector2 (transform.position.x, 18.5f);
			if (ballPos.position.y < -3.5) {
				transform.position = temp;
			} else if (ballPos.position.y > 18.5) {
				transform.position = temp2;
			}
		}
	}




	// method to change the velocity of the ball when it collides, depending on location of collision on paddle. 
	// detect that collision occured
	void onCollisionEnter(Collision coll) {
		// get position and transform of ball
		ballPos = GameObject.FindGameObjectWithTag ("ball").transform;

		Vector2 v = coll.rigidbody.velocity;

		// velo stores magnitude of velocity of the ball immediately after it collides
		float velo = v.magnitude;

		// y of ball's velocity replaced by distance between center of paddle and location of point of contact of ball
		//multiplied by random number... 10
		v.y = (coll.transform.position.y - ballPos.transform.position.y) * 10;

		//increase velocity if new velocity is less than before. 
		if (v.magnitude < velo) {
			coll.rigidbody.velocity *= velo/coll.rigidbody.velocity.magnitude;
		}
	}
	
}

