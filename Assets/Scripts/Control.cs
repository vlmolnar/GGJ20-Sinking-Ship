using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public string startSceneName;
    public string controlsSceneName;
    public string titleSceneName;
    //AudioSource audioData;
    //private static Control self;
   
    void Start()
    {

    }

    public void StartScene()
    {
        SceneManager.LoadScene(startSceneName);
        Debug.Log("Game start");
    }

    public void ControlsScene()
    {
        SceneManager.LoadScene(controlsSceneName);
        Debug.Log("Game controls");
    }

    public void TitleScene()
    {
        SceneManager.LoadScene(titleSceneName);
        Debug.Log("Game title screen");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
