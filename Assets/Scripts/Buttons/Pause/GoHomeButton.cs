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
        private Animation _forkPausePanelAnim; // Анимация внутренней панели паузы
        [SerializeField] private GameObject goHomePanel; // Панель "Покинуть уровень"
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
            StartCoroutine(ChangePanel());
        }

        private IEnumerator ChangePanel()
        {
            _forkPausePanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_forkPausePanelAnim["PanelDying"].length);
            forkPausePanel.SetActive(false);
            goHomePanel.SetActive(true);
            _goHomePanelAnim.Play("PanelUprise");
        }
    }
}
