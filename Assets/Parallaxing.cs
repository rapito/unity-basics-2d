using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] objectsToParallax; 		//Sprites to be parallaxed
	private float[] scales; 					// proportion of cameras movement to move bgs by.

	public float smoothing = 1f; 				//how smooth the parallax is: >0

	private Transform cam; 						// main camera transform
	private Vector3 lastCamPos; 				// camera pos in previous frame


	/// <summary>
	/// Adds the object to the objectToParallax list.
	/// </summary>
	/// <param name="bg">Background to parallax</param>
	public void AddBG(Transform bg)
	{
		Transform[] newBgs = new Transform[objectsToParallax.Length + 1];

		for (int i=0;i<objectsToParallax.Length; i++) {
			newBgs[i] = objectsToParallax[i];
		}

		newBgs[objectsToParallax.Length] = bg;

		objectsToParallax = newBgs;

		StartScales ();
	}

	// Called before start, good for references
	void Awake ()
	{
		// setup camera reference
		cam = Camera.main.transform;

	}

	// Use this for initialization
	void Start () {
		
		// previous frame had the current frame pos
		lastCamPos = cam.position;

		StartScales();
	}

	void StartScales()
	{
		// assign corresponding parallax size
		scales = new float[objectsToParallax.Length];
		for (int i = 0; i<objectsToParallax.Length; i++) {
			scales[i] = objectsToParallax[i].position.z *-1;
		}

	}


	
	// Update is called once per frame
	void Update () {


		// for each bg 
		for (int i = 0; i<objectsToParallax.Length; i++) {
			// the prallax is the opposite of the camara movement because the prevuous frame multiplied by the scale
			float parallax = (lastCamPos.x - cam.position.x) * (scales[i]);

			// set a target x position which is the current pos + the parallax 
			float bgTargetPosX = objectsToParallax[i].position.x + parallax;


			// Create the target position for the bg based on the bgTargetPosX
			Vector3 bgTargetPos = new Vector3(bgTargetPosX,objectsToParallax[i].position.y,objectsToParallax[i].position.z);

			// fade between current pos and target pos using Lerp
			objectsToParallax[i].position = Vector3.Lerp(objectsToParallax[i].position,bgTargetPos, smoothing * Time.deltaTime);


		}

		// set the lastPosition to the current camera pos.
		lastCamPos = cam.position;
	}
}
