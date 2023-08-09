using System.Collections;
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

        private Animation _forkPausePanelAnim; // Анимация внутренней панели паузы
        private Animation _goHomePanelAnim; // Анимация появления внутренней панели паузы

        protected override void StartProcess()
        {
            _forkPausePanelAnim = forkPausePanel.GetComponent<Animation>();
            _goHomePanelAnim = goHomePanel.GetComponent<Animation>();
        }

        protected override void Process()
        {
            StartCoroutine(CoroutineProcess());
        }
        
        private IEnumerator CoroutineProcess()
        {
            _goHomePanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_goHomePanelAnim["PanelDying"].length);
            forkPausePanel.SetActive(true);
            goHomePanel.SetActive(false);
            _forkPausePanelAnim.Play("PanelUprise");
        }
    }
}
