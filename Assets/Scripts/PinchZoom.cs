using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{
		public float MINZOOM = 3.2f;
		public float MAXZOOM = 12.8f;
		float mDistancePixel = 0.0f;
		float mZoomConst=0.6f;
		Vector3  startpoint;
		Vector3  endpoint;

		void Update ()
		{
				if (Input.touchCount == 2) {
						Touch touch1 = Input.GetTouch (0);
						Touch touch2 = Input.GetTouch (1);//пальцы ординаты
						if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began) {//второй палец на экране
								float pX = Mathf.Abs (touch1.position.x - touch2.position.x);
								float pY = Mathf.Abs (touch1.position.y - touch2.position.y);
								mDistancePixel = Mathf.Sqrt (pX * pX + pY * pY) * camera.orthographicSize;//дистанция между пальцамы в пикселях

								Ray ray1 = Camera.main.ScreenPointToRay ((touch1.position + touch2.position) / 2.0f);
								startpoint = ray1.GetPoint (100);
								startpoint.z = -10;//начальная средняя точка
						} else if ((touch1.phase == TouchPhase.Moved && touch2.phase != TouchPhase.Ended) ||
								(touch2.phase == TouchPhase.Moved && touch1.phase != TouchPhase.Ended)) {//один из пальцев двигается
								float pX = Mathf.Abs (touch1.position.x - touch2.position.x);
								float pY = Mathf.Abs (touch1.position.y - touch2.position.y);
								camera.orthographicSize = mDistancePixel / Mathf.Sqrt (pX * pX + pY * pY);//вычисление зума
								if (camera.orthographicSize > MAXZOOM) {
										camera.orthographicSize = MAXZOOM;
										mDistancePixel = Mathf.Sqrt (pX * pX + pY * pY) * camera.orthographicSize;//ограничение зума по максу
								} else if (camera.orthographicSize < MINZOOM) {
										camera.orthographicSize = MINZOOM;
										mDistancePixel = Mathf.Sqrt (pX * pX + pY * pY) * camera.orthographicSize;//ограничение зума по минимуму
								}

								Ray ray1 = Camera.main.ScreenPointToRay ((touch1.position + touch2.position) / 2.0f);
								endpoint = ray1.GetPoint (100);
								endpoint.z = -10;
								transform.position += startpoint - endpoint;//конечная средняя точка
						}
				} else {
			bool pTrue=false;
			float pZoomPlus = -1.0f * mZoomConst;
			if (Input.GetAxis ("Mouse ScrollWheel") < 0){pTrue=true; }
			if (Input.GetAxis ("Mouse ScrollWheel") > 0){pTrue=true; pZoomPlus = mZoomConst;}
			if(pTrue){
				camera.orthographicSize += pZoomPlus;//вычисление зума
				if (camera.orthographicSize > MAXZOOM) {
					camera.orthographicSize = MAXZOOM;
				} else if (camera.orthographicSize < MINZOOM) {
					camera.orthographicSize = MINZOOM;
				}
			}
}
						
				
		}
}