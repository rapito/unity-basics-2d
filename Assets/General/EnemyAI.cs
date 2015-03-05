using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]

public class EnemyAI : MonoBehaviour {

	// What to chase
	public Transform target;
	// How many times a second we update the path
	public float updateRate = 2f;

	// Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	// Calculated path
	public Path path;

	//AIs speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;

	// Max distance from the AI to the waypoint 
	// for it to to continue to the next waypoint
	public float nextWaypointDistance = 3f;

	// The waypoint index we are currently moving towards
	private int currentWaypoint = 0;

	void Start(){
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		// TODO: If we don't have a target, find one 
		if (target == null) {
			Debug.LogError("Target Needed!!!!!");
			return;
		}

		StartNewPath ();

		// Don't update path on each frame.
		StartCoroutine (UpdatePath());
	}
	
	IEnumerator UpdatePath()
	{
		if (target == null) {
			// TODO: Insert a player search here
			return false;
		}
		
		StartNewPath ();
		
		yield return new WaitForSeconds (1f / updateRate );
		StartCoroutine (UpdatePath ());
	}

	void FixedUpdate(){
		if (target == null) {
			// TODO: Insert a player search here
			return;
		}
	
		// TODO: Always look at player

		if (path == null)
			return;

		// If our currentWaypoint is ge that the vector 
		// count it means we reached the path's end
		if (currentWaypoint >= path.vectorPath.Count) {
			if(pathIsEnded) return;

			Debug.Log("End of path reached");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		// Current waypoint Position
		Vector3 waypointPos = path.vectorPath [currentWaypoint];
		 
		// Get Normalized Direction to the next waypoint
		Vector3 dir = (waypointPos - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime; // note that we are using fixedDeltaTime.

		// Move the AI
		rb.AddForce (dir, fMode);

		// check if we are close enough to the next 
		// waypoint and follow it if yes 
		float dist = Vector3.Distance (transform.position, waypointPos);
		if (dist < nextWaypointDistance) {
			currentWaypoint++;
		}

		return;
	}

	void StartNewPath(){
		// Start new path to the targte pos, and call 
		// our callback method OnPathComplete
		seeker.StartPath (transform.position, target.position, OnPathComplete);
	}

	public void OnPathComplete(Path p){

		Debug.Log ("Got Path, error check: " + p.error);

		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

}
