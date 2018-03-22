using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {

    public LevelManager levelManager;

    public AudioMixer audioMixer;

    public void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadNext()
    {
       levelManager.NextScene();
    }

    // volume
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetAmbientVolume(float volume)
    {
        audioMixer.SetFloat("Ambient", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

}
