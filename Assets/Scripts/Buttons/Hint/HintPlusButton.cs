using System;
using TMPro;
using UnityEngine;

namespace Buttons.Hint
{
    /**
     * Кнопка увеличения кол-ва подсказок при покупке
     */
    public class HintPlusButton : CommonButton
    {
        [SerializeField] private TextMeshProUGUI hintCount;
        [SerializeField] private TextMeshProUGUI coinsPrice;
        [SerializeField] private GameObject buyHintForCoinsButtonIsDisabled; // Изменение цвета кнопки "купить подсказку" в случае её недоступности
        [SerializeField] private GameObject hintMinusButtonIsDisabled; // Изменение цвета кнопки "-" в случае её недоступности
        [SerializeField] private GameObject hintPlusButtonIsDisabled; // Изменение цвета кнопки "+" в случае её недоступности
        
        protected override void StartProcess()
        {
            // empty
        }
        
        protected override void Process()
        {
            int hintCountInt = Int32.Parse(hintCount.text);
            int hintPrice = PlayerPrefs.GetInt("HintPrice");
            if (hintCountInt == 3)
            {
                return;
            }

            int currentCoinsPrice = Int32.Parse(coinsPrice.text);
            int newCoinsPrice = currentCoinsPrice + hintPrice;
            if (PlayerPrefs.GetInt("Coins") < newCoinsPrice)
            {
                hintPlusButtonIsDisabled.SetActive(true);
                return;
            }
            
            int newHintCount = hintCountInt + 1;
            hintCount.text = newHintCount.ToString();
            coinsPrice.text = newCoinsPrice.ToString();
            buyHintForCoinsButtonIsDisabled.SetActive(PlayerPrefs.GetInt("Coins") < newCoinsPrice);
            
            hintMinusButtonIsDisabled.SetActive(false);
            bool cannotAffordNext = PlayerPrefs.GetInt("Coins") < newCoinsPrice + hintPrice;
            hintPlusButtonIsDisabled.SetActive(newHintCount >= 3 || cannotAffordNext);
        }
    }
}
