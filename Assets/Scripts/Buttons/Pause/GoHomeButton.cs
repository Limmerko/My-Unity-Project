using System.Collections;
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

        private Animation _forkPausePanelAnim; // Анимация внутренней панели паузы
        private Animation _goHomePanelAnim; // Анимация появления внутренней панели паузы
        
        protected override void StartProcess()
        {
            _forkPausePanelAnim = forkPausePanel.GetComponent<Animation>();
            _goHomePanelAnim = goHomePanel.GetComponent<Animation>();
        }
    
        /**
        * Возвращение на главный экран
        */
        protected override void Process()
        {
            StartCoroutine(CoroutineProcess());
        }

        private IEnumerator CoroutineProcess()
        {
            _forkPausePanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_forkPausePanelAnim["PanelDying"].length);
            forkPausePanel.SetActive(false);
            goHomePanel.SetActive(true);
            _goHomePanelAnim.Play("PanelUprise");
        }
    }
}
