using System;
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
        [SerializeField] private TextMeshProUGUI hintCount; // Кол-во подсказок для покупки

        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _buyHintPanelAnim; // Анимация панели покупки подсказки
        
        protected override void StartProcess()
        {
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            _buyHintPanelAnim = buyHintPanel.GetComponent<Animation>();
        }
        
        protected override void Process()
        {
            int coinsCount = PlayerPrefs.GetInt("Coins");
            int hintCountInt = Int32.Parse(hintCount.text);
            int hintPrice = PlayerPrefs.GetInt("HintPrice");
            int price = hintPrice * hintCountInt;

            if (coinsCount < price)
            {
                return;
            }

            PlayerPrefs.SetInt("Coins", coinsCount - price);
            PlayerPrefs.SetInt(PlayerPrefs.GetString("LastHint"), hintCountInt);
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
            buyHintPanel.SetActive(false);
        }
    }
}
