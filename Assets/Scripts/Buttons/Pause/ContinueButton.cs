using UnityEngine;

namespace Buttons.Pause
{
    /**
    * Кнопка "Продолжить" в меню паузы
    */
    public class ContinueButton : CommonButton
    {
        [SerializeField] protected GameObject pausePanel; // Панель паузы
        
        protected override void StartProcess()
        {
            // empty
        }
    
        protected override void Process()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }
}
