using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

	public float fireRate = 0f;				// Rate at which to fire bullets.
	public float cooldown = 5f;				// Cooldown time after weapon has overloaded
	public float damage = 1f;
	public LayerMask whatToHit;				// Layers that won't be 'hit' by the weapon

	float fireInterval = 0f;				// Interval to wait between burst fire.
	Transform firePoint;
	float shootReach = 100f;
	
	// Use this for initialization
	void Start ()
	{
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("There's no FirePoint object inside the weapon's handler");
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		// Debug Shooting
		// Shoot ();
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		} else {
			// If Fire1 is active and the timePassed is 
			// greater that our interval then shoot
			if (Input.GetButton ("Fire1") && Time.time > fireInterval) {
				// 1 / rate gives us the delay or 
				// frequeny in milliseconds we need.
				fireInterval = Time.time + 1 / fireRate;
				Shoot ();
			}
		}
	}

	void Shoot ()
	{
		Debug.Log (" Bang! ");
		// Translate mouse position to game coordinates.
		Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 targetPos = new Vector2 (mouse.x,mouse.y);


		Vector2 firePointPos = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPos, targetPos - firePointPos, shootReach , whatToHit);

		Debug.DrawLine (firePointPos, (targetPos - firePointPos) * 6f, Color.green);
		if (hit.collider != null) {

			Debug.DrawLine (firePointPos, hit.point, Color.red);

		}
	}
}
