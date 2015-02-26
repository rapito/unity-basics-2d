using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	// This is used to maintain a threshold 
	// limit in between the difference of cameras 
	// right angle and sprites limits
	public float offsetX = 2f; 

	// This is to know ifwe already
	// created instances to the objects side
	public bool rightBuddy = false;
	public bool leftBuddy = false;

	public bool reverseScale = false; // Used if the object is not tileable

	private float width = 0f;
	private Camera cam;
	private Transform trans;

	void Awake()
	{
		cam = Camera.main;
		trans = transform;
	}

	// Use this for initialization
	void Start () {

		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		width = renderer.sprite.bounds.size.x;

	}
	
	// Update is called once per frame
	void Update () {

        // if it doesn't need buddies then don't do anything
        if(!leftBuddy || !rightBuddy)
        {

            // Get Half of what cameras with (what it can see)
            float camWidth = cam.orthographicSize * Screen.width / Screen.height;

            //ACalculate edges where cameras can see teh sprites
            float rightEdge = (trans.position.x + width /2) - camWidth;
            float leftEdge = (trans.position.x - width / 2) + camWidth;

            // Checking if we can see the element and then assign a new buddy
            if(cam.transform.position.x >= rightEdge - offsetX && !rightBuddy)
            {
                MakeNewBuddy(1);
                rightBuddy = true;
            }
            else if(cam.transform.position.x <= leftEdge + offsetX && !leftBuddy)
            {
                MakeNewBuddy(-1);
                leftBuddy = true;
            }

        }

        
	}

    // Creates a buddy on the required side
    void MakeNewBuddy(int leftOrRight)
    {
        // create the position for the new buddy
        Vector3 pos = new Vector3(trans.position.x + width * leftOrRight, trans.position.y, trans.position.z );

        Transform buddy = Instantiate (trans, pos, trans.rotation) as Transform;
        /// if not tileable then reverse the x size 
        /// of the object to make it seems as it is repeating
        if(reverseScale)
        {
            buddy.localScale = new Vector3(buddy.localScale.x*-1,buddy.localScale.y,buddy.localScale.z);
        }

        buddy.parent = trans.parent;

        if(leftOrRight > 0)
        {
            buddy.GetComponent<Tiling>().leftBuddy = true;
        }
        else 
        {
            buddy.GetComponent<Tiling>().rightBuddy = true;
        }

		GameObject gm = GameObject.Find ("_GM"); 
		Parallaxing parallax = gm.GetComponent<Parallaxing> ();
		parallax.AddBG(buddy);
    }
}