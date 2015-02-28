using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats {
		public int Health = 100;
	}

	public PlayerStats playerStats = new PlayerStats();
	public int fallYBoundary = -20;

	void Damage(int damage)
	{
		playerStats.Health -= damage;
		if (playerStats.Health <= 0) {
			Debug.LogError("Player is DEAD!");
			GameMaster.KillPlayer(this);
		}

	}

	void Update()
	{
		// Check if player is below ground and kill him/her
		if (transform.position.y <= fallYBoundary) {
			Damage(99999);
		}
	}

}
