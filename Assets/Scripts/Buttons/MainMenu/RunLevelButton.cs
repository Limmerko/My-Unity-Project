using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons.MainMenu
{
    public class RunLevelButton : CommonButton
    {
        protected override void StartProcess()
        {
            string text = "Уровень " + (PlayerPrefs.GetInt("Level") + 1)  ;
            iconUp.GetComponent<TextMeshProUGUI>().text = text;
            iconDown.GetComponent<TextMeshProUGUI>().text = text;
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
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}