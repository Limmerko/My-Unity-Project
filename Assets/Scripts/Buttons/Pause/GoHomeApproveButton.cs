using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons.Pause
{
    /**
     * Кнопка "Выйти" на экране подтверждения закрытия уровня
     */
    public class GoHomeApproveButton : CommonButton
    {
        
        [SerializeField] private GameObject goHomePanel; // Панель "Покинуть уровень"
        private Animation _goHomePanelAnim; // Анимация появления внутренней панели паузы
        
        protected override void StartProcess()
        {
            _goHomePanelAnim = goHomePanel.GetComponent<Animation>();
        }
        
        /**
        * Подтвержение возвращения на главный экран
        */
        protected override void Process()
        {
            StartCoroutine(CoroutineProcess());
        }
        
        private IEnumerator CoroutineProcess()
        {
            _goHomePanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_goHomePanelAnim["PanelDying"].length);
            Statics.TimeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
