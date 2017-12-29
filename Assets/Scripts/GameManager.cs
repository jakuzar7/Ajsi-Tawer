#region INIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	public Text platformLevelText;
	public GameObject platform, startingPlatform, player, rightWall,leftWall;
	private Vector3 spawnPosition, wallSpawnPosition;
	public float startingSpawnRate, spaceBetweenPlatforms;
	public int platformCount, platformLevel = -1;
	public static GameManager gameManager;
	Rigidbody2D rbPlayer;
	void Awake(){
		if(gameManager == null){
			gameManager = this;
		}
		if(gameManager != this){
			Destroy(gameManager.gameObject);
		}
	}
		
	void Start () {
		rbPlayer = player.GetComponent <Rigidbody2D>();
		/*if(rbPlayer = null){
			Debug.LogError("rbPlayer NULL");
		}else{
			Debug.Log("rbPlayer = " + rbPlayer.name);

		}*/
		spawnPosition.y = -3f;
		StartCoroutine("spawnCoroutine");
	}
	#endregion

	public void AddPlatformLevel(int amount){
		platformLevel += amount;
		platformLevelText.text = "Level: " + platformLevel;

		CameraController.Cameracontroller.speedUp(platformLevel);
	}



	#region Platform Coroutine
	IEnumerator spawnCoroutine(){
		do
		{
			PlatformSpawner();

			if(platformLevel % 20 == 0 ){
				Debug.Log("gravity increased");
				rbPlayer.gravityScale *= 1.5f;
			}

			yield return new WaitForSeconds(startingSpawnRate);


		} while(true);
	}

	void PlatformSpawner(){
		if ((spawnPosition.y -100f < player.transform.position.y) || platformCount< 50)
		{
			if (platformCount % 10 == 0)
			{
				wallSpawnPosition.x = -20f;
				Instantiate(leftWall, wallSpawnPosition, transform.rotation);
				wallSpawnPosition.x = 20f;
				Instantiate(rightWall, wallSpawnPosition, transform.rotation);

				spawnPosition.x = 0f;
				Instantiate(startingPlatform, spawnPosition, transform.rotation);


			}
			else
			{

				spawnPosition.x = Random.Range(-20f, 20f);// <---- spawnposition random

				Instantiate(platform, spawnPosition, gameObject.transform.rotation);

			}
			spawnPosition.y += spaceBetweenPlatforms;
			wallSpawnPosition.y += spaceBetweenPlatforms;
			platformCount++;
		}
			
		if(platformCount> 100){
			startingSpawnRate = 0f;
		}
	}
	#endregion
}
