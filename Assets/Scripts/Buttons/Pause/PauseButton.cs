using UnityEngine;

namespace Buttons.Pause
{
    /**
    * Кнопка "Пауза" на экране игры
    */
    public class PauseButton : CommonButton
    {
        [SerializeField] private GameObject pausePanel; // Панель паузы
        [SerializeField] private GameObject forkPausePanel; // Панель паузы
        [SerializeField] private GameObject goHomePanel; // Панель "Покинуть уровень"
        
        protected override void StartProcess()
        {
            pausePanel.SetActive(false);
            forkPausePanel.SetActive(false);
            goHomePanel.SetActive(false);
        }
    
        /**
         * Открытие меню паузы
         */
        protected override void Process()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            forkPausePanel.SetActive(true);
            goHomePanel.SetActive(false);
        }
    }
}
