using System.Collections;
using UnityEngine;

namespace Buttons.Hint
{
    /**
     * Кнопка "Возобновить" на панели после проигрыша
     */
    public class ResumeAfterLoseButton : CommonButton
    {
        [SerializeField] private GameObject backgroundPanel; // Панель паузы
        [SerializeField] private GameObject losePanel; // Панель проигрыша
        [SerializeField] private CancelLastMoveButton cancelLastMoveScript;
        
        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _losePanelPanelAnim; // Анимация панели проигрыша
        
        protected override void StartProcess()
        {
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            _losePanelPanelAnim = losePanel.GetComponent<Animation>();
        }
    
        protected override void Process()
        {
            StartCoroutine(CoroutineProcess());
        }
        
        private IEnumerator CoroutineProcess()
        {
            _backgroundPanelAnim.Play("BackgroundPanelDying");
            _losePanelPanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_losePanelPanelAnim["PanelDying"].length);
            cancelLastMoveScript.CancelLastMove();
            Statics.TimeScale = 1;
            Statics.IsGameOver = false;
            backgroundPanel.SetActive(false); 
            losePanel.SetActive(false);
        }
    }
}
