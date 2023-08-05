using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    void Start()
    {
        Vibration.Init();
        
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        
        if (!PlayerPrefs.HasKey("LevelType"))
        {
            PlayerPrefs.SetString("LevelType", "Random");
        }
    }
}
