using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons
{
    /**
     * Кнопка "Возвращение на главный экран"
     */
    public class GoHomeButton : CommonButton
    {
        protected override void StartProcess()
        {
            // empty
        }
    
        /**
        * Возвращение на главный экран
        */
        protected override void Process()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
