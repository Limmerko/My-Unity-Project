using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons.Pause
{
    /**
     * Кнопка "Выйти" на экране подтверждения закрытия уровня
     */
    public class GoHomeApproveButton : CommonButton
    {
        protected override void StartProcess()
        {
            // empty
        }
        
        /**
        * Подтвержение возвращения на главный экран
        */
        protected override void Process()
        {
            // Time.timeScale = 1;
            Statics.TimeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
