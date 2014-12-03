using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Wind : MonoBehaviour
{
		public float WindForceX = 0.1f;
		public float WindForceY = 0.1f;
		public Text tText ;
		// Use this for initialization
		void Start ()
		{
				WindForceX = Random.Range (-0.1f, 0.1f);
				WindForceY = Random.Range (-0.1f, 0.1f);
		}

		public void FixedUpdate ()
		{
				tText.text = "WIND: SpeedX:" + WindForceX + " SpeedY:" + WindForceY;
				if (rigidbody2D.IsAwake ()) {
						rigidbody2D.AddForce (new Vector2 (WindForceX, WindForceY), ForceMode2D.Force);
				}
		}
}
