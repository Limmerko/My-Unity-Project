using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons
{
    /*
    * Кнопка "Случайный" на главном экране
    */
    public class RandomLevelButton : CommonButton
    {
        protected override void StartProcess()
        {
            // empty
        }
    
        protected override void Process()
        {
            StartCoroutine(RunRandomLevel());
        }
    
        /**
        * Запуск случайного уровня
        */
        private IEnumerator RunRandomLevel()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
