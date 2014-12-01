using UnityEngine;
using System.Collections;

public class RocketBehavior : MonoBehaviour
{
		public int forcex;
		public int forcey;
		private bool start = false;
		//public Transform COM;
		// Use this for initialization
		void Start ()
		{
				Debug.Log ("1");
				int a = 5;
				//rigidbody2D.centerOfMass = new Vector3 (-1000, -1000, 0);
				//	rigidbody2D.centerOfMass = COM.localPosition;
		}
	
		// Update is called once per frame
		void Update ()
		{
		}

		void FixedUpdate ()
		{
				if (start) {
						Vector2 qwe = rigidbody2D.velocity;

						if (qwe.x >= 0) {
								transform.rotation = Quaternion.Euler (0, 0, 57.2957795f * Mathf.Atan (qwe.y / qwe.x));
						} else if (qwe.x < 0) {
								transform.rotation = Quaternion.Euler (0, 0, 180+57.2957795f * Mathf.Atan (qwe.y / qwe.x));
						}
						Debug.Log (57.2957795 * Mathf.Atan ((qwe.y / qwe.x)));
						//transform.forward = Vector3.Slerp (transform.forward, rigidbody2D.velocity.normalized, Time.deltaTime);
				}
		}
	
		public void WTF ()
		{
				rigidbody2D.AddForce (new Vector2 (forcex, forcey));
				Debug.Log ("asd");
				start = true;
		}

}

