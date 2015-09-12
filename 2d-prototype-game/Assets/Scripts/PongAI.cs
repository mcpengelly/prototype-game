using UnityEngine;
using System.Collections;

public class PongAI : MonoBehaviour {

		Transform Enemy; 
		//public float speed = 1.0f;
		 
	// you will find that the paddle repositions its center to where the ball is, causing the paddle to slightly pass top and bottom walls.
	// Error to fix: the ball sometimes passes through the top and bottom walls when colliding with paddles on sharp angle (CPU also moves out of frame)
		void FixedUpdate ()
		{
		Enemy = GameObject.FindWithTag ("ball").transform;
			transform.position = new Vector2 
				(transform.position.x, Enemy.position.y);
		}
	}
