using UnityEngine;

namespace Buttons.Pause
{
    /**
     * Кнопка "Вернуться" на экране подтверждения закрытия уровня
     */
    public class GoBackPauseButton : CommonButton
    {
        [SerializeField] private GameObject forkPausePanel; // Панель паузы
        [SerializeField] private GameObject goHomePanel; // Панель "Покинуть уровень"

        protected override void StartProcess()
        {
            // empty
        }

        protected override void Process()
        {
           forkPausePanel.SetActive(true);
           goHomePanel.SetActive(false);
        }
    }
}
