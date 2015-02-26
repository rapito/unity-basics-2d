using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	public float rotationOffset = 0f; // Angles to sum to the resulting angle.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Convert mouse coordinates to game coordinates
		// and calculate the distance between the mouse an the arm
		Vector3 mouse = Input.mousePosition;
		Vector3 diff = Camera.main.ScreenToWorldPoint (mouse) - transform.position;
		// Normalize the angle to obtain the 
		// amplitude of each vector component.
		diff.Normalize ();

		// Finally, triangle the angle and assign 
		// it to the transform object.
		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
	}
}
