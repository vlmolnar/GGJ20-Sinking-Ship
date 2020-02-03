using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{

    AudioSource audioData;
    private static MusicPlayer self;

    void Awake()
    {
        if (!self) self = this;
        else Destroy(this.gameObject);
    }
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        Debug.Log("started");
        DontDestroyOnLoad(this.gameObject);
        if (self.audioData.isPlaying)
        {
            audioData.Play(0);
        }
    }
}
