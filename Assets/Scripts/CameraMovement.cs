using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
		Vector3  startpoint;
		Vector3  endpoint;
		bool  panning = false;
		public float minPosX = 10;
		public float maxPosX = 10;
		public float minPosY = -10;
		public float maxPosY = 10;
		public float dist;
		int moveFingerId = -1;

		void Start ()
		{
		}

		void Update ()
		{
#if !UNITY_EDITOR
				Ray ray1 = Camera.main.ScreenPointToRay (Input.mousePosition);
				Ray ray2 = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Input.touchCount == 1) {
						Touch touch = Input.touches [0];
						if (touch.phase == TouchPhase.Began && Input.touches.Length == 1) {
								Vector2 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
								RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
								if (hit.collider == null || hit.collider.tag != "Player") {
										startpoint = ray1.GetPoint (10); // Returns a point at distance units along the ray
										startpoint.z = -10; // fix z to 0
										panning = true;
										moveFingerId = touch.fingerId;
								} else {
										return;
								}
						}
		
						if (panning && moveFingerId == touch.fingerId) {
								dist = Mathf.Clamp ((Vector3.Distance (endpoint, startpoint)), 0.0f, 1.0f);
								endpoint = ray2.GetPoint (10); // Returns a point at distance units along the ray
								endpoint.z = 0; // fix z, somehow its not always 0?
			
								if (dist >= 0.1) {
										transform.position += startpoint - endpoint;
										Vector3 tNewPos;
										tNewPos.x = Mathf.Clamp (transform.position.x, minPosX, maxPosX);
										tNewPos.y = Mathf.Clamp (transform.position.y, minPosY, maxPosY);
										tNewPos.z = -10;
										transform.position = tNewPos;
								}
						}
		
						if (touch.phase == TouchPhase.Ended && Input.touches.Length == 1) {
								panning = false;
								moveFingerId=-1;
						}			
							
				}
#else
				Ray ray1 = Camera.main.ScreenPointToRay (Input.mousePosition);
				Ray ray2 = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Input.GetMouseButtonDown (0)) {
						Vector2 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
						if (hit.collider == null || hit.collider.tag != "Player") {
								startpoint = ray1.GetPoint (10); 
								startpoint.z = -10;
								panning = true;
						} else {
								return;
						}
				}
				if (panning) {
						dist = Mathf.Clamp ((Vector3.Distance (endpoint, startpoint)), 0.0f, 1.0f);
						endpoint = ray2.GetPoint (10); 
						endpoint.z = -10; 
			
						if (dist >= 0.1) {
								transform.position += startpoint - endpoint;
								Vector3 tNewPos;
								tNewPos.x = Mathf.Clamp (transform.position.x, minPosX, maxPosX);
								tNewPos.y = Mathf.Clamp (transform.position.y, minPosY, maxPosY);
								tNewPos.z = -10;
								transform.position = tNewPos;
						}
				}
		
		
				if (Input.GetMouseButtonUp (0)) {
						panning = false;
				}
#endif
		}

}