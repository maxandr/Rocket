using UnityEngine;
using System.Collections;

public class Light : MonoBehaviour {
	public GameObject lightScaler ;
	public GameObject light2 ;
	BoxCollider2D boxCollider2D;
	// Use this for initialization
	void Start () {
		boxCollider2D = light2.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = boxCollider2D.size.y;
		Vector3 t = lightScaler.transform.localScale;
		Vector3 t1 = transform.position;
		t.y = transform.position.y/(h*transform.localScale.y);
		lightScaler.transform.localScale=t;
		t1.y -= transform.position.y/2;
		lightScaler.transform.position=t1;
	}
}
