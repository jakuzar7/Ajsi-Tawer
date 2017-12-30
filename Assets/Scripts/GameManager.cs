#region INIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    private Rigidbody2D[] objectsArray;
    public bool paused;
    public Text platformLevelText;
    public GameObject platform, startingPlatform, rightWall, leftWall;
    private Vector3 spawnPosition, wallSpawnPosition;
    public float startingSpawnRate, spaceBetweenPlatforms;
    public int platformCount, platformLevel = -1;
    public static GameManager gameManager;
    private Rigidbody2D playerRb;
    public GameObject[] menuButtons;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        if (gameManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameManager);
    }

    void Start()
    {
        paused = true;
        spawnPosition.y = -3f;
    }

    void FindPlayerRb()
    {
        objectsArray = FindObjectsOfType<Rigidbody2D>();

        for (int i = 0; i < objectsArray.Length; i++)
        {
            if (objectsArray[i].CompareTag("Player"))
            {
                playerRb = objectsArray[i];
            }
        }

        if (playerRb == null)
        {
            Debug.LogError("playerRb = null");
        }
        else
        {
            Debug.Log("playerRb = " + playerRb.name);
        }

    }

    #endregion


    public void MenuButtonsState(bool state)
    {
        if (state)
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].SetActive(false);
            }
        }
    }

    public void MenuButtonsText(int scene)
    {
        if (scene == 1)
        {
            menuButtons[0].GetComponentInChildren<Text>().text = "Main Menu";
            menuButtons[1].GetComponentInChildren<Text>().text = "Restart";
        }
        else if (scene == 0)
        {
            menuButtons[0].GetComponentInChildren<Text>().text = "Quit";
            menuButtons[1].GetComponentInChildren<Text>().text = "Start";
        }
    }

    public void AddPlatformLevel(int amount)
    {
        platformLevel += amount;
        platformLevelText.text = "Level: " + platformLevel;

        CameraController.Cameracontroller.speedUp(platformLevel);
    }

    public void PlayerKilled()
    {
        StopCoroutine(SpawnCoroutine());
        PauseGame();
        MenuButtonsText(1);
        MenuButtonsState(true);
    }

    public void PauseGame()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    #region Platform Coroutine
    public IEnumerator SpawnCoroutine()
    {
        wallSpawnPosition.y = 0f;
        platformCount = 0;
        platformLevel = -1;
        spawnPosition.y = -3f;
        FindPlayerRb();

        do
        {
            if (!paused)
            {
                PlatformSpawner();

                yield return new WaitForSeconds(startingSpawnRate);
            }
        } while (!paused);
    }

    void PlatformSpawner()
    {

        if ((spawnPosition.y - 100f < playerRb.transform.position.y) || platformCount < 50)
        {

            if (platformCount % 10 == 0)
            {
                Debug.Log("platformCount: " + platformCount + " #PlatformSpawner->wallspawning");

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

        if (platformCount > 100)
        {
            startingSpawnRate = 0f;
        }
    }
    #endregion
}
