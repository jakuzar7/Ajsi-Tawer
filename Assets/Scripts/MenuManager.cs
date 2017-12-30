using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager menuManager;
    void Awake()
    {
        if (menuManager == null)
        {
            menuManager = this;
        }
        if (menuManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(menuManager);
    }

    public void StartSceneButton()
    {
        GameManager.gameManager.PauseGame();
        GameManager.gameManager.MenuButtonsState(false);
        GameManager.gameManager.MenuButtonsText(1);
        SceneManager.LoadScene(1);
        //GameManager.gameManager.StartCoroutine("SpawnCoroutine");

    }

    public void ButtonExit()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            GameManager.gameManager.MenuButtonsState(true);
            GameManager.gameManager.MenuButtonsText(0);
            SceneManager.LoadScene(0);
        }
        else
        {
            Application.Quit();
            Debug.Log("Application.Quit");
        }
    }
}
