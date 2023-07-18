using UnityEngine;

namespace Buttons
{
    /**
    * Кнопка "Продолжить" в меню паузы
    */
    public class ContinueButton : CommonButton
    {
        [SerializeField] private GameObject pausePanel; // Панель паузы

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
