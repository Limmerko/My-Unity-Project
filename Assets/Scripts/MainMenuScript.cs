using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private AudioSource soundMusic; // Звук фоновой музки  

    private void Awake()
    {
        GameObject music = GameObject.FindWithTag("Music");
        if (music == null && MainUtils.SettingIsOn(SettingsType.MusicSettings))
        {
            soundMusic.gameObject.tag = "Music";
            soundMusic.Play();
            DontDestroyOnLoad(soundMusic);
        }
    }

    void Start()
    {
        MainUtils.VibrationInit();
        
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        
        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 0);
        }

        if (!PlayerPrefs.HasKey(SettingsType.MusicSettings.ToString()))
        {
            PlayerPrefs.SetInt(SettingsType.MusicSettings.ToString(), 1);
        }
        
        if (!PlayerPrefs.HasKey(SettingsType.SoundsSettings.ToString()))
        {
            PlayerPrefs.SetInt(SettingsType.SoundsSettings.ToString(), 1);
        }
        
        if (!PlayerPrefs.HasKey(SettingsType.VibrationSettings.ToString()))
        {
            PlayerPrefs.SetInt(SettingsType.VibrationSettings.ToString(), 1);
        }

        if (!PlayerPrefs.HasKey("Lives"))
        {
            PlayerPrefs.SetInt("Lives", Statics.MaxLives);
        }

        if (!PlayerPrefs.HasKey("MaxFinishTiles"))
        {
            PlayerPrefs.SetInt("MaxFinishTiles", Statics.MaxFinishTiles);
        }

        PlayerPrefs.Save();
    }
}
