using System;
using Enums;
using TMPro;
using UnityEngine;

namespace Buttons.HintsShop
{
    public class OneHintPlusButton : CommonButton
    {
        [SerializeField] private TextMeshProUGUI hintCount;
        [SerializeField] private TextMeshProUGUI coinsPrice;
        [SerializeField] private GameObject buyHintForCoinsButtonIsDisabled; // Изменение цвета кнопки "купить подсказку" в случае её недоступности
        [SerializeField] private GameObject hintMinusButtonIsDisabled; // Изменение цвета кнопки "-" в случае её недоступности
        [SerializeField] private GameObject hintPlusButtonIsDisabled; // Изменение цвета кнопки "+" в случае её недоступности
        [SerializeField] private HintType hintType; // Тип подсказки
        
        private int _hintPrice; // Цена подсказки
        private string _prefCount;
        
        protected override void StartProcess()
        {
            switch (hintType)
            {
                case HintType.CancelLastMove:
                    _hintPrice = HintPrice.CancelLastMovePrice;
                    _prefCount = HintCountType.CountCancelLastMove;
                    break;
                case HintType.Refresh:
                    _hintPrice = HintPrice.HintRefreshPrice;
                    _prefCount = HintCountType.CountRefresh;
                    break;
                case HintType.HintMove:
                    _hintPrice = HintPrice.HintMovePrice;
                    _prefCount = HintCountType.CountHintMove;
                    break;
            }
        }
        
        protected override void Process()
        {
            int hintCountInt = Int32.Parse(hintCount.text);
            int hintCountNow = PlayerPrefs.GetInt(_prefCount);
            int hintCountMax = 3 - hintCountNow;
            if (hintCountInt == hintCountMax)
            {
                return;
            }

            int newHintCount = hintCountInt + 1;
            hintCount.text = newHintCount.ToString();
            int newCoinsPrice = Int32.Parse(coinsPrice.text) + _hintPrice;
            coinsPrice.text = newCoinsPrice.ToString();
            buyHintForCoinsButtonIsDisabled.SetActive(PlayerPrefs.GetInt("Coins") < newCoinsPrice);
            
            hintMinusButtonIsDisabled.SetActive(false);
            hintPlusButtonIsDisabled.SetActive(newHintCount.Equals(hintCountMax));
        }
    }
}