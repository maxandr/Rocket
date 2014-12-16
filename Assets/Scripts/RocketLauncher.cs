using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour
{
		bool startLaunch = false;
		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
#if !UNITY_EDITOR
				foreach (Touch touch in Input.touches) {
						if (touch.phase == TouchPhase.Began && Input.touches.Length == 1) {
								Vector2 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
								RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
								if (hit.collider != null) {
										if (hit.collider.tag == "Player") {
												startLaunch = true;
										}	
								}
						}
						if (touch.phase == TouchPhase.Ended && Input.touches.Length == 1) {
								if (startLaunch) {
										Vector3 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
										Vector3 dest = transform.position - worldPoint;
										GetComponent<RocketBehavior> ().forcex = dest.x * 1.0f / 3.6f;
										GetComponent<RocketBehavior> ().forcey = dest.y * 0.8f / 2.9f;
										GetComponent<RocketBehavior> ().WTF ();
								}
								startLaunch = false;
						}
				}
#else
				if (Input.GetMouseButtonDown (0)) {
						Vector2 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
						if (hit.collider != null) {
								if (hit.collider.tag == "Player") {
										startLaunch = true;
								}	
						}
				}
				if (Input.GetMouseButtonUp (0)) {
						if (startLaunch) {
								Vector3 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
								Vector3 dest = worldPoint - transform.position;
								GetComponent<RocketBehavior> ().forcex = dest.x;// * 1.0f / 3.6f;
								GetComponent<RocketBehavior> ().forcey = dest.y;// * 0.8f / 2.9f;
								GetComponent<RocketBehavior> ().WTF ();
						}
						startLaunch = false;
				}
				if (startLaunch) {
						Vector3 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						Vector3 dest = worldPoint- transform.position  ;
						DrawLine (transform.position, worldPoint);
						Debug.Log (dest);

						//DrawLine (worldPoint, transform.position);
				}
#endif
		}

		void DrawLine (Vector2 a, Vector2 b)
		{
				Debug.Log ("a:" + a);
				Debug.Log ("b:" + b);
				Debug.DrawLine (a, b);
		
		}

}
