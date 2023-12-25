using UnityEngine;

namespace Buttons.Pause
{
    /**
    * Кнопка "Настройки" на экране игры
    */
    public class SettingsButton : CommonButton
    {
        [SerializeField] private GameObject backgroundPanel; // Основная панель паузы
        [SerializeField] protected GameObject settingsPanel; // Панель настроек
        
        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _settingsAnim; // Анимация внутренней панели паузы
        
        protected override void StartProcess()
        {
            backgroundPanel.SetActive(false);
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            settingsPanel.SetActive(false);
            _settingsAnim = settingsPanel.GetComponent<Animation>();
        }
        
        /**
         * Открытие меню настроек
         */
        protected override void Process()
        {
            Statics.TimeScale = 0;
            backgroundPanel.SetActive(true);
            settingsPanel.SetActive(true);
            _backgroundPanelAnim.Play("BackgroundPanelUprise");
            _settingsAnim.Play("PanelUprise");
        }
    }
}
