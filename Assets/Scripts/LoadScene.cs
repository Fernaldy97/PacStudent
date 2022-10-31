using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    //Load level 1
    public void LoadFirstLevel()
    {
        SceneManager.LoadSceneAsync("PacStudent", LoadSceneMode.Single);
    }

    //Load level 2
    public void LoadSecondLevel()

    {
        SceneManager.LoadSceneAsync("SecondLevel", LoadSceneMode.Single);
    }

    //Load Start Scene
    public void QuitGame()
    {
        SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single);
    }

    //Exit function
    public void ExitApplication()
    {
        Application.Quit();
    }
}
