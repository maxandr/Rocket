using UnityEngine;
using System.Collections;

public class RocketBehavior : MonoBehaviour
{
		public float forcex;
		public float forcey;
		public bool start = false;
		//public Transform COM;
		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
		}

		void FixedUpdate ()
		{
				if (start) {
						Vector2 qwe = rigidbody2D.velocity;
						if (qwe.x > 0) {
								transform.rotation = Quaternion.Euler (0.0f, 0.0f, 57.2957795f * Mathf.Atan (qwe.y / qwe.x));
						} else if (qwe.x < 0) {
								transform.rotation = Quaternion.Euler (0.0f, 0.0f, 180 + 57.2957795f * Mathf.Atan (qwe.y / qwe.x));
						}
						//rigidbody2D.AddForce (new Vector2 (forcex, forcey), ForceMode2D.Force);
				}


				if (transform.position.y < 0) {
						Restart ();
				}
		}
	
		public void WTF ()
		{
				rigidbody2D.isKinematic = false;
				rigidbody2D.AddForce (new Vector2 (forcex, forcey), ForceMode2D.Impulse);
				start = true;
		}

		public void Restart ()
		{
				Application.LoadLevel ("MainRocket");
		}

		void OnCollisionEnter2D (Collision2D coll)
		{
				Debug.Log ("LOOOSSEEER11");
				if (coll.gameObject.tag == "Obstacle") {
						Debug.Log ("LOOOSSEEER");
						Restart ();
				}
		}

		public void OnTriggerEnter2D (Collider2D other)
		{
				if (other.gameObject.tag == "Target") {
						GameObject TM = GameObject.Find ("TargetManager");
						TargetManagercs t = TM.GetComponent<TargetManagercs> ();
						t.TargetPicked ();
						Destroy (other.gameObject);
				}
		}
}

