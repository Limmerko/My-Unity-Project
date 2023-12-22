using System.Collections;
using UnityEngine;

namespace Buttons.Pause
{
    /**
    * Кнопка "Пауза" на экране игры
    */
    public class PauseButton : CommonButton
    {
        [SerializeField] private GameObject backgroundPanel; // Основная панель паузы
        [SerializeField] private GameObject forkPausePanel; // Внутрення панель паузы
        [SerializeField] private GameObject goHomePanel; // Панель "Покинуть уровень"
        [SerializeField] private GameObject losePanel; // Панель проигрыша
        [SerializeField] private GameObject nextLevelPanel; // Панель следующего уровня
        [SerializeField] protected GameObject buyHintPanel; // Панель покупки подсказки
        [SerializeField] protected GameObject settingsPanel; // Панель настроек
        

        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _forkPausePanelAnim; // Анимация внутренней панели паузы
        
        protected override void StartProcess()
        {
            backgroundPanel.SetActive(false);
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            forkPausePanel.SetActive(false);
            _forkPausePanelAnim = forkPausePanel.GetComponent<Animation>();
            goHomePanel.SetActive(false);
            losePanel.SetActive(false);
            nextLevelPanel.SetActive(false);
            buyHintPanel.SetActive(false);
            settingsPanel.SetActive(false);
        }

        /**
         * Открытие меню паузы
         */
        protected override void Process()
        {
            Statics.TimeScale = 0;
            backgroundPanel.SetActive(true);
            forkPausePanel.SetActive(true);
            goHomePanel.SetActive(false);
            buyHintPanel.SetActive(false);
            _backgroundPanelAnim.Play("BackgroundPanelUprise");
            _forkPausePanelAnim.Play("PanelUprise");
        }
    }
}
