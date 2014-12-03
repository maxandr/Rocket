using UnityEngine;
using System.Collections;

public class TargetManagercs : MonoBehaviour
{
		static int CommonTargetCount = 1;
		// Use this for initialization
		void Start ()
		{
				CommonTargetCount = GameObject.FindGameObjectsWithTag ("Target").Length;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void TargetPicked ()
		{
				CommonTargetCount -= 1;
				if (CommonTargetCount == 0) {
						Application.LoadLevel ("MainRocket");
						Debug.Log ("WIN!!!!!!!");
				}
		}
	

}
