using UnityEngine.SceneManagement;

namespace Buttons.Pause
{
    /**
     * Кнопка "Перезагрузка уровня"
     */
    public class RestartButton : CommonButton
    {
        protected override void StartProcess()
        {
            // empty
        }
    
        /**
        * Перезагрузка уровня
        */
        protected override void Process()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
