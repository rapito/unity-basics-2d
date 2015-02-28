using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster GM;
	public Transform playerPrefab;
	public Transform spawnPoint;


	void Start()
	{
		if (GM == null) {
			GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}

	public void RespawnPlayer()
	{
		Transform player = Instantiate(playerPrefab,spawnPoint.position,spawnPoint.rotation);

	}

	public static void KillPlayer(Player player)
	{
		Destroy(player.gameObject);
		GM.RespawnPlayer ();
	}

}
