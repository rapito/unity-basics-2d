using UnityEngine;
using System.Collections;

public class BulletTrail : MonoBehaviour {

	public float speed = 15f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * speed);
		//TODO: change this to Destroy itself when it collides an outer box .
		Destroy (gameObject, 2);
	}
}
