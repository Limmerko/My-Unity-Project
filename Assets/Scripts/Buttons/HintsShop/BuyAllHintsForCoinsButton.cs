using System;
using System.Collections;
using Enums;
using TMPro;
using UnityEngine;

namespace Buttons.HintsShop
{
    /**
     * Кнопка "Купить подсказки за монеты" в магазине подсказок 
     */
    public class BuyAllHintsForCoinsButton : CommonButton
    {
        [SerializeField] protected GameObject backgroundPanel; // Панель паузы
        [SerializeField] protected GameObject buyAllHintsPanel; // Панель Магазина подсказок
        [SerializeField] private TextMeshProUGUI countCancelLastMoveText; // Текст с кол-вом подсказок отмены хода
        [SerializeField] private TextMeshProUGUI countRefreshText; // Текст с кол-вом подсказок перемешивания
        [SerializeField] private TextMeshProUGUI countHintMoveText; // Текст с кол-вом подсказок подсказки хода
        [SerializeField] private TextMeshProUGUI coinsAllPriceText; // Текст с со стоимостью всех подсказок
        [SerializeField] private TextMeshProUGUI allCoinsText; // Текст с кол-вом монет
                
        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _buyAllHintsPanelAnim; // Анимация панели Магазина подсказок
        
        protected override void StartProcess()
        {
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            _buyAllHintsPanelAnim = buyAllHintsPanel.GetComponent<Animation>();
        }
        
        protected override void Process()
        {
            int coinsCount = PlayerPrefs.GetInt("Coins");
            int price = Int32.Parse(coinsAllPriceText.text);

            int countCancelLastMove = PlayerPrefs.GetInt(HintCountType.CountCancelLastMove);
            int countRefresh = PlayerPrefs.GetInt(HintCountType.CountRefresh);
            int countHintMoveInt = PlayerPrefs.GetInt(HintCountType.CountHintMove);
            
            int addCountCancelLastMove = Int32.Parse(countCancelLastMoveText.text);
            int addCountRefresh = Int32.Parse(countRefreshText.text);
            int addCountHintMoveInt = Int32.Parse(countHintMoveText.text);
            
            int newCountCancelLastMove = countCancelLastMove + addCountCancelLastMove;
            int newCountRefresh = countRefresh + addCountRefresh;
            int newCountHintMoveInt = countHintMoveInt + addCountHintMoveInt;
            
            if (price == 0 || 
                coinsCount < price ||
                newCountCancelLastMove > 3 ||
                newCountRefresh > 3 ||
                newCountHintMoveInt > 3)
            {
                return;
            }
            
            PlayerPrefs.SetInt(HintCountType.CountCancelLastMove, newCountCancelLastMove);
            PlayerPrefs.SetInt(HintCountType.CountRefresh, newCountRefresh);
            PlayerPrefs.SetInt(HintCountType.CountHintMove, newCountHintMoveInt);
            PlayerPrefs.SetInt("Coins", coinsCount - price);
            PlayerPrefs.Save();
            
            allCoinsText.text = PlayerPrefs.GetInt("Coins").ToString();
            
            StartCoroutine(CoroutineProcess());
        }
        
        private IEnumerator CoroutineProcess()
        {
            _backgroundPanelAnim.Play("BackgroundPanelDying");
            _buyAllHintsPanelAnim.Play("PanelDying");
            yield return new WaitForSeconds(_buyAllHintsPanelAnim["PanelDying"].length);
            Statics.TimeScale = 1;
            backgroundPanel.SetActive(false); 
            buyAllHintsPanel.SetActive(false);
        }
    }
}
