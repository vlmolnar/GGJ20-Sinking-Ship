using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Control : MonoBehaviour
{
    public string nextSceneName;
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        Debug.Log("started");
        DontDestroyOnLoad(this.gameObject);
    }
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
