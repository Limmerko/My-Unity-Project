using UnityEngine;

namespace Buttons
{
    /**
    * Кнопка "Пауза" на экране игры
    */
    public class PauseButton : CommonButton
    {
        [SerializeField] private GameObject pausePanel; // Панель паузы

        protected override void StartProcess()
        {
            pausePanel.SetActive(false);
        }
    
        /**
         * Открытие меню паузы
         */
        protected override void Process()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }
}
