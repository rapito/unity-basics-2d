using UnityEngine;
using System.Collections;
using UnitySampleAssets._2D;


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
		Transform player = Instantiate(playerPrefab,spawnPoint.position,spawnPoint.rotation) as Transform;
		Camera2DFollow cam = Camera.main.GetComponent<Camera2DFollow> ();
		cam.target = player;
	}

	public static void KillPlayer(Player player)
	{
		Destroy(player.gameObject);
		GM.RespawnPlayer ();
	}

}
