using UnityEngine;
using System.Collections;
using UnitySampleAssets._2D;


public class GameMaster : MonoBehaviour {

	public static GameMaster GM;
	public Transform playerPrefab;
	public Transform spawnPoint;
	public Transform spawnPrefab; // Effect to use for Respawning.

	public int spawnDelay = 0; // Tim eto wait before respawning the player.


	void Start()
	{
		if (GM == null) {
			GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}

	public IEnumerator RespawnPlayer()
	{
		audio.Play ();
		yield return new WaitForSeconds (spawnDelay);

		Transform player = Instantiate(playerPrefab,spawnPoint.position,spawnPoint.rotation) as Transform;
		GameObject effect = (Instantiate(spawnPrefab,spawnPoint.position,spawnPoint.rotation) as Transform).gameObject;

		// Delete effect after someTime
		Destroy (effect, 1.8f);

		// Point main camera back to character;
		Camera2DFollow cam = Camera.main.GetComponent<Camera2DFollow> ();
		cam.target = player;
	}

	public static void KillPlayer(Player player)
	{
		Destroy(player.gameObject);
		GM.StartCoroutine (GM.RespawnPlayer());
	}

}
