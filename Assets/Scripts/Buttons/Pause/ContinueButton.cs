using System.Collections;
using UnityEngine;

namespace Buttons.Pause
{
    /**
    * Кнопка "Продолжить" в меню паузы
    */
    public class ContinueButton : CommonButton
    {
        [SerializeField] protected GameObject backgroundPanel; // Панель паузы
        [SerializeField] private GameObject forkPausePanel; // Панель паузы
        
        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _forkPausePanelAnim; // Анимация внутренней панели паузы
        
        protected override void StartProcess()
        {
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            _forkPausePanelAnim = forkPausePanel.GetComponent<Animation>();
        }
    
        protected override void Process()
        {
            StartCoroutine(CoroutineProcess());
        }
        
        private IEnumerator CoroutineProcess()
        {
            _backgroundPanelAnim.Play("BackgroundPanelDying");
            _forkPausePanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_forkPausePanelAnim["PanelDying"].length);
            Statics.TimeScale = 1;
            backgroundPanel.SetActive(false);
            forkPausePanel.SetActive(false);
        }
    }
}
