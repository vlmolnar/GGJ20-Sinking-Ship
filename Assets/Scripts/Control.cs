using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public string nextSceneName;
    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
        Debug.Log("Game start");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
