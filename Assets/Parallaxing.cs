using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] bgs; 		//Sprites to be parallaxed
	private float[] scales; 		// proportion of cameras movement to move bgs by.

	public float smoothing = 1f; 	//how smooth the parallax is: >0

	private Transform cam; 			// main camera transform
	private Vector3 lastCamPos; 	// camera pos in previous frame


	public void AddBG(Transform bg)
	{
		Transform[] newBgs = new Transform[bgs.Length + 1];

		for (int i=0;i<bgs.Length; i++) {
			newBgs[i] = bgs[i];
		}

		newBgs[bgs.Length] = bg;

		bgs = newBgs;

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
		scales = new float[bgs.Length];
		for (int i = 0; i<bgs.Length; i++) {
			scales[i] = bgs[i].position.z *-1;
		}

	}


	
	// Update is called once per frame
	void Update () {


		// for each bg 
		for (int i = 0; i<bgs.Length; i++) {
			// the prallax is the opposite of the camara movement because the prevuous frame multiplied by the scale
			float parallax = (lastCamPos.x - cam.position.x) * (scales[i]);

			// set a target x position which is the current pos + the parallax 
			float bgTargetPosX = bgs[i].position.x + parallax;


			// Create the target position for the bg based on the bgTargetPosX
			Vector3 bgTargetPos = new Vector3(bgTargetPosX,bgs[i].position.y,bgs[i].position.z);

			// fade between current pos and target pos using Lerp
			bgs[i].position = Vector3.Lerp(bgs[i].position,bgTargetPos, smoothing * Time.deltaTime);


		}

		// set the lastPosition to the current camera pos.
		lastCamPos = cam.position;
	}
}
