using UnityEngine;

namespace Buttons.Pause
{
    /**
    * Кнопка "Пауза" на экране игры
    */
    public class PauseButton : CommonButton
    {
        [SerializeField] private GameObject pausePanel; // Основная панель паузы
        [SerializeField] private GameObject forkPausePanel; // Внутрення панель паузы
        [SerializeField] private GameObject goHomePanel; // Панель "Покинуть уровень"
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject nextLevel;

        protected override void StartProcess()
        {
            pausePanel.SetActive(false);
            forkPausePanel.SetActive(false);
            goHomePanel.SetActive(false);
            losePanel.SetActive(false);
            nextLevel.SetActive(false);
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
