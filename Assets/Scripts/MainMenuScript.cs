using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private AudioSource soundMusic; // Звук фоновой музки  

    private void Awake()
    {
        GameObject music = GameObject.FindWithTag("Music");
        if (music == null)
        {
            soundMusic.gameObject.tag = "Music";
            soundMusic.Play();
            DontDestroyOnLoad(soundMusic);
        }
    }

    void Start()
    {
        Vibration.Init();
        
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        
        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 0);
        }
        
        PlayerPrefs.Save();
    }
}
