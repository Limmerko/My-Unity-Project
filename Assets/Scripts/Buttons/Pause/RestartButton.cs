using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons.Pause
{
    /**
     * Кнопка "Перезагрузка уровня"
     */
    public class RestartButton : CommonButton
    {
        [SerializeField] private GameObject forkPausePanel; // Панель паузы
        
        private Animation _forkPausePanelAnim; // Анимация внутренней панели паузы
        
        protected override void StartProcess()
        {
            _forkPausePanelAnim = forkPausePanel.GetComponent<Animation>();
        }
    
        /**
        * Перезагрузка уровня
        */
        protected override void Process()
        {
            StartCoroutine(CoroutineProcess());
        }
        
        private IEnumerator CoroutineProcess()
        {
            _forkPausePanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_forkPausePanelAnim["PanelDying"].length);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            MainUtils.ClearProgress();
        }
    }
}
