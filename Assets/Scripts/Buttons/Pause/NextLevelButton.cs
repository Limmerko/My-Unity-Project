using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons.Pause
{
    /**
     * Кнопка "Следующий уровень"
     */
    public class NextLevelButton : CommonButton
    {
        protected override void StartProcess()
        {
            // empty
        }
        
        protected override void Process()
        {
            StartCoroutine(RunNextLevel());
        }
        
        /**
        * Запуск Следующего уровня
        */
        private IEnumerator RunNextLevel()
        {
            PlayerPrefs.SetString("LevelType", "Next");
            PlayerPrefs.Save();
            
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}