using UnityEngine;
using System.Collections;

//временно убрал ограничения по передвижению. зуму мешает
public class CameraMovement : MonoBehaviour
{
		Vector3  startpoint;
		Vector3  endpoint;
		bool  panning = false;
		public float minPosX = -9.0f;//-2.5f*camera.orthographicSize*camera.pixelWidth/camera.pixelHeight//size=6.4,w=1280,h=(720 or other)
		public float maxPosX = 9.0f;//2.5f*camera.orthographicSize*camera.pixelWidth/camera.pixelHeight
		public float minPosY = -1.4f;//5-camera.orthographicSize
		public float maxPosY = 24.2f;//15+camera.orthographicSize
		public float dist;
		public float MINZOOM = 3.2f;
		public float MAXZOOM = 12.8f;
		float mDistancePixel = 0.0f;
		float mZoomConst = 0.6f;

		void Start ()
		{
		}

	void CameraBorder()
	{
		Vector3 tNewPos;
		
		tNewPos.x = Mathf.Clamp (transform.position.x, minPosX+camera.orthographicSize*camera.pixelWidth/camera.pixelHeight, 
		                         maxPosX-camera.orthographicSize*camera.pixelWidth/camera.pixelHeight);
		tNewPos.y = Mathf.Clamp (transform.position.y, minPosY+camera.orthographicSize, maxPosY-camera.orthographicSize);
		tNewPos.z = -10;
		transform.position = tNewPos;
	}

		void Update ()
		{

				//ZOOM
				bool pZoomMode = false;
				if (Input.touchCount == 2) {
						pZoomMode = true;
						Touch touch1 = Input.GetTouch (0);
						Touch touch2 = Input.GetTouch (1);//пальцы ординаты
						if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began) {//второй палец на экране
								float pX = Mathf.Abs (touch1.position.x - touch2.position.x);
								float pY = Mathf.Abs (touch1.position.y - touch2.position.y);
								mDistancePixel = Mathf.Sqrt (pX * pX + pY * pY) * camera.orthographicSize;//дистанция между пальцамы в пикселях
				
								Ray ray11 = Camera.main.ScreenPointToRay ((touch1.position + touch2.position) / 2.0f);
								startpoint = ray11.GetPoint (100);
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
				
								Ray ray11 = Camera.main.ScreenPointToRay ((touch1.position + touch2.position) / 2.0f);
								endpoint = ray11.GetPoint (100);
								endpoint.z = -10;
								transform.position += startpoint - endpoint;//конечная средняя точка
								CameraBorder();
								ray11 = Camera.main.ScreenPointToRay ((touch1.position + touch2.position) / 2.0f);
								startpoint = ray11.GetPoint (100);
								startpoint.z = -10;//начальная средняя точка cброс
						}
				} else {
						bool pTrue = false;
						float pZoomPlus = -1.0f * mZoomConst;
						if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
								pTrue = true;
						}
						if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
								pTrue = true;
								pZoomPlus = mZoomConst;
						}
						if (pTrue) {
								pZoomMode = true;
								camera.orthographicSize += pZoomPlus;//вычисление зума
								if (camera.orthographicSize > MAXZOOM) {
										camera.orthographicSize = MAXZOOM;
								} else if (camera.orthographicSize < MINZOOM) {
										camera.orthographicSize = MINZOOM;
								}
								CameraBorder();
						}
				}
				if (pZoomMode)
						return;

				//camera move
				Ray ray1 = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				if (panning) {
					dist = Mathf.Clamp ((Vector3.Distance (endpoint, startpoint)), 0.0f, 1.0f);
					endpoint = ray1.GetPoint (10); 
					endpoint.z = -10; 
					
					if (dist >= 0.1) {
						transform.position += startpoint - endpoint;
						CameraBorder();
						Ray ray2 = Camera.main.ScreenPointToRay (Input.mousePosition);
						startpoint = ray2.GetPoint (10); 
						startpoint.z = -10;
					}						
				}
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
				if (Input.GetMouseButtonUp (0)) {
						panning = false;
				}

				if (Input.GetKey (KeyCode.Escape)) {
						Application.Quit ();
				} 
		}

}