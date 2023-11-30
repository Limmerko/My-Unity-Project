using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons.Pause
{
    /**
     * Кнопка "Следующий уровень"
     */
    public class NextLevelButton : CommonButton
    {
        [SerializeField] protected GameObject backgroundPanel; // Панель паузы
        [SerializeField] private GameObject nextLevelPanel; // Панель перехода на следующий уровень
        
        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _nextLevelPanelAnim; // Анимация панели перехода на следующий уровень
        
        protected override void StartProcess()
        {
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            _nextLevelPanelAnim = nextLevelPanel.GetComponent<Animation>();
        }
        
        protected override void Process()
        {
            StartCoroutine(RunNextLevel());
        }
        
        /**
        * Запуск Следующего уровня
        */
        private IEnumerator RunNextLevel()
        {
            _backgroundPanelAnim.Play("BackgroundPanelDying");
            _nextLevelPanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_nextLevelPanelAnim["PanelDying"].length);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}