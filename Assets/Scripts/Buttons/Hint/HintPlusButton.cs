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
        
        protected override void StartProcess()
        {
            // empty
        }
        
        protected override void Process()
        {
            int hintCountInt = Int32.Parse(hintCount.text);
            if (hintCountInt == 3)
            {
                return;
            }
            hintCount.text = (hintCountInt + 1).ToString();
        }
    }
}
