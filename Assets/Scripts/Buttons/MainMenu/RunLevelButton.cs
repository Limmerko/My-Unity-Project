using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons.MainMenu
{
    public class RunLevelButton : CommonButton
    {
        protected override void StartProcess()
        {
            // empty
        }
        
        protected override void Process()
        {
            StartCoroutine(RunLevel());
        }
        
        /**
        * Запуск Следующего уровня
        */
        private IEnumerator RunLevel()
        {
            PlayerPrefs.SetString("LevelType", "Next");
            PlayerPrefs.Save();
            
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}