using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{
		public float perspectiveZoomSpeed = 0.005f;
		public float orthoZoomSpeed = 0.005f;
		public float MINZOOM = 6.2f;
		public float MAXZOOM = 12.0f;
	
		void Update ()
		{
				if (Input.touchCount == 2) {
						Touch touchZero = Input.GetTouch (0);
						Touch touchOne = Input.GetTouch (1);
			
						Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
						Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
						float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
						float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
						float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
						float temp = camera.orthographicSize;
						temp += deltaMagnitudeDiff * orthoZoomSpeed;
				
						if (temp < MINZOOM || temp > MAXZOOM) {
								return;
						}
						camera.orthographicSize = temp;
						camera.orthographicSize = Mathf.Max (camera.orthographicSize, 0.1f);
				}
		}
}