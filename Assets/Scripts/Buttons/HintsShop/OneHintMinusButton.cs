using System;
using Enums;
using TMPro;
using UnityEngine;

namespace Buttons.HintsShop
{
    /**
     * Кнопка уменьшения кол-ва подсказок при покупке в "Магазине подсказок"
     */
    public class OneHintMinusButton : CommonButton
    {
        [SerializeField] private TextMeshProUGUI hintCount;
        [SerializeField] private TextMeshProUGUI coinsPrice;
        [SerializeField] private GameObject buyHintForCoinsButtonIsDisabled; // Изменение цвета кнопки "купить подсказку" в случае её недоступности
        [SerializeField] private GameObject hintMinusButtonIsDisabled; // Изменение цвета кнопки "-" в случае её недоступности
        [SerializeField] private GameObject hintPlusButtonIsDisabled; // Изменение цвета кнопки "+" в случае её недоступности
        [SerializeField] private HintType hintType; // Тип подсказки
        
        private int _hintPrice; // Цена подсказки
        
        protected override void StartProcess()
        {
            switch (hintType)
            {
                case HintType.CancelLastMove:
                    _hintPrice = HintPrice.CancelLastMovePrice;
                    break;
                case HintType.Refresh:
                    _hintPrice = HintPrice.HintRefreshPrice;
                    break;
                case HintType.HintMove:
                    _hintPrice = HintPrice.HintMovePrice;
                    break;
            }
        }
        
        protected override void Process()
        {
            int hintCountInt = Int32.Parse(hintCount.text);
            if (hintCountInt == 0)
            {
                return;
            }
            
            int newHintCount = hintCountInt - 1;
            hintCount.text = newHintCount.ToString();
            int newCoinsPrice = Int32.Parse(coinsPrice.text) - _hintPrice;
            coinsPrice.text = newCoinsPrice.ToString();
            buyHintForCoinsButtonIsDisabled.SetActive(PlayerPrefs.GetInt("Coins") < newCoinsPrice);
            
            hintMinusButtonIsDisabled.SetActive(newHintCount.Equals(0));
            int hintCountNow = PlayerPrefs.GetInt(GetHintCountPreference());
            int hintCountMax = 3 - hintCountNow;
            bool isAtMax = newHintCount >= hintCountMax;
            bool cannotAffordNext = PlayerPrefs.GetInt("Coins") < newCoinsPrice + _hintPrice;
            hintPlusButtonIsDisabled.SetActive(isAtMax || cannotAffordNext);
            OneHintPlusButton.UpdateAllPlusButtonStates();
        }

        private string GetHintCountPreference()
        {
            switch (hintType)
            {
                case HintType.CancelLastMove:
                    return HintCountType.CountCancelLastMove;
                case HintType.Refresh:
                    return HintCountType.CountRefresh;
                case HintType.HintMove:
                    return HintCountType.CountHintMove;
                default:
                    return string.Empty;
            }
        }
    }
}
