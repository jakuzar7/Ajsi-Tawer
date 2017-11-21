#region INIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	public Text platformLevelText;
	public GameObject platform, startingPlatform, player, wall;
	private Vector3 spawnPosition, wallSpawnPosition;
	public float startingSpawnRate, spaceBetweenPlatforms;
	public int platformCount, platformLevel = -1;
	public static GameManager gameManager;

	void Awake(){
		if(gameManager == null){
			gameManager = this;
		}
		if(gameManager != this){
			Destroy(gameManager.gameObject);
		}
	}
		
	void Start () {
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

			yield return new WaitForSeconds(startingSpawnRate);

		} while(true);
	}

	void PlatformSpawner(){
		if ((spawnPosition.y -100f < player.transform.position.y) || platformCount< 50)
		{
			if (platformCount % 10 == 0)
			{
				wallSpawnPosition.x = -20f;
				Instantiate(wall, wallSpawnPosition, transform.rotation);
				wallSpawnPosition.x = 20f;
				Instantiate(wall, wallSpawnPosition, transform.rotation);

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
