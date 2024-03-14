using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private RefreshButton refreshScript;
        [SerializeField] private TextMeshProUGUI livesText; // Текст с кол-вом жизней
        
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
            int lives = PlayerPrefs.GetInt("Lives");
            if (lives > 0)
            {
                int newCountLives = lives - 1;
                PlayerPrefs.SetInt("Lives", newCountLives);
                PlayerPrefs.Save();
                livesText.text = newCountLives.ToString();
                yield return Continue();
            }
            else
            {
                // TODO возможно это стоит разнести на 2 разных скрипта т.к. кнопки 2 (но это не точно)
            }
            yield return new WaitForSeconds(0);
        }

        private IEnumerator Continue()
        {
            _backgroundPanelAnim.Play("BackgroundPanelDying");
            _losePanelPanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_losePanelPanelAnim["PanelDying"].length);
            cancelLastMoveScript.CancelLastMove();
            refreshScript.Refresh();
            Statics.TimeScale = 1;
            Statics.IsGameOver = false;
            backgroundPanel.SetActive(false);
            losePanel.SetActive(false);
        }
    }
}
