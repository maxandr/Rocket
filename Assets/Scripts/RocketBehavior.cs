using UnityEngine;
using System.Collections;

public class RocketBehavior : MonoBehaviour
{
		public int forcex;
		public int forcey;
		public Transform COM;
		// Use this for initialization
		void Start ()
		{
				Debug.Log ("1");
				int a = 5;
				//rigidbody2D.centerOfMass = new Vector3 (-1000, -1000, 0);
				rigidbody2D.centerOfMass = COM.localPosition;

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void WTF ()
		{
				rigidbody2D.AddForce (new Vector2 (forcex, forcey));
				Debug.Log ("asd");
		}

}

