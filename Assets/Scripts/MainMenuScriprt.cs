using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScriprt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
