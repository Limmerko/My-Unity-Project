using System.Collections;
using TMPro;
using UnityEngine;

namespace Buttons.Hint
{
    /**
     * Кнопка "Купить подсказку за монеты" на панели покупки подсказок
     */
    public class BuyHintForCoinsButton : CommonButton
    {
        [SerializeField] protected GameObject backgroundPanel; // Панель паузы
        [SerializeField] protected GameObject buyHintPanel; // Панель покупки подсказки
        [SerializeField] private TextMeshProUGUI coinsText; // Текст с кол-вом монет
        
        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _buyHintPanelAnim; // Анимация панель покупки подсказки
        
        protected override void StartProcess()
        {
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            _buyHintPanelAnim = buyHintPanel.GetComponent<Animation>();
        }
        
        protected override void Process()
        {
            int coinsCount = PlayerPrefs.GetInt("Coins");
            if (coinsCount < 100)
            {
                return;
            }

            PlayerPrefs.SetInt("Coins", coinsCount - 100);
            PlayerPrefs.SetInt(PlayerPrefs.GetString("LastHint"), 1);
            PlayerPrefs.Save();
            
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
            
            StartCoroutine(CoroutineProcess());
        }
        
        private IEnumerator CoroutineProcess()
        {
            _backgroundPanelAnim.Play("BackgroundPanelDying");
            _buyHintPanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_buyHintPanelAnim["PanelDying"].length);
            Statics.TimeScale = 1;
            backgroundPanel.SetActive(false);
        }
    }
}
