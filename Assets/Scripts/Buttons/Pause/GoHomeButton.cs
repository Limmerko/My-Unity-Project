using UnityEngine;

namespace Buttons.Pause
{
    /**
     * Кнопка "Дом" на панеле паузы
     */
    public class GoHomeButton : CommonButton
    {
        [SerializeField] private GameObject forkPausePanel; // Панель паузы
        [SerializeField] private GameObject goHomePanel; // Панель "Покинуть уровень"
        
        protected override void StartProcess()
        {
            // empty
        }
    
        /**
        * Возвращение на главный экран
        */
        protected override void Process()
        {
           forkPausePanel.SetActive(false);
           goHomePanel.SetActive(true);
        }
    }
}
