using System;
using TMPro;
using UnityEngine;

namespace Buttons.Hint
{
    /**
     * Кнопка уменьшения кол-ва подсказок при покупке
     */
    public class HintMinusButton : CommonButton
    {
        [SerializeField] private TextMeshProUGUI hintCount;
        [SerializeField] private TextMeshProUGUI coinsPrice;
        
        protected override void StartProcess()
        {
            // empty
        }
        
        protected override void Process()
        {
            int hintCountInt = Int32.Parse(hintCount.text);
            if (hintCountInt == 1)
            {
                return;
            }
            int newHintCount = hintCountInt - 1;
            hintCount.text = newHintCount.ToString();
            coinsPrice.text = (newHintCount * 100).ToString();
        }
    }
}
