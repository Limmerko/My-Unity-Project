using Enums;
using TMPro;
using UnityEngine;

namespace Buttons.HintsShop
{
    /**
     * Кнопка "Магазин подсказок" 
     */
    public class HintShopButton : CommonButton
    {
        [SerializeField] protected GameObject backgroundPanel; // Панель паузы
        [SerializeField] protected GameObject buyAllHintsPanel; // Панель покупки подсказок
        [SerializeField] protected TextMeshProUGUI countCancelLastMove;
        [SerializeField] protected TextMeshProUGUI countRefresh;
        [SerializeField] protected TextMeshProUGUI countHintMove;
        [SerializeField] private GameObject hintCancelLastMoveMinusButtonIsDisabled;
        [SerializeField] private GameObject hintRefreshMinusButtonIsDisabled;
        [SerializeField] private GameObject hintMoveMinusButtonIsDisabled; 
        [SerializeField] private GameObject hintCancelLastMovePlusButtonIsDisabled;
        [SerializeField] private GameObject hintRefreshPlusButtonIsDisabled;
        [SerializeField] private GameObject hintMovePlusButtonIsDisabled; 
        
        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _buyAllHintsPanelAnim; // Анимация панели покупки подсказказок
        
        protected override void StartProcess()
        {
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            buyAllHintsPanel.SetActive(false);
            _buyAllHintsPanelAnim = buyAllHintsPanel.GetComponent<Animation>();
        }
        
        protected override void Process()
        {
            Statics.TimeScale = 0;
            SetAllCountsEqualsZero();
            SetDisabledAllMinusButtons();
            SetDisabledAllPlusButtons();
            StartAnims();
        }

        private void SetAllCountsEqualsZero()
        {
            countCancelLastMove.text = "0";
            countRefresh.text = "0";
            countHintMove.text = "0";
        }

        private void SetDisabledAllMinusButtons()
        {
            hintCancelLastMoveMinusButtonIsDisabled.SetActive(true);
            hintRefreshMinusButtonIsDisabled.SetActive(true);
            hintMoveMinusButtonIsDisabled.SetActive(true);
        }

        private void SetDisabledAllPlusButtons()
        {
            int countHintCancelLastMoveNow = PlayerPrefs.GetInt(HintCountType.CountCancelLastMove.ToString());
            int countHintRefreshNow = PlayerPrefs.GetInt(HintCountType.CountRefresh.ToString());
            int countHintMoveNow = PlayerPrefs.GetInt(HintCountType.CountHintMove.ToString());
            
            hintCancelLastMovePlusButtonIsDisabled.SetActive(3 == countHintCancelLastMoveNow);
            hintRefreshPlusButtonIsDisabled.SetActive(3 == countHintRefreshNow);
            hintMovePlusButtonIsDisabled.SetActive(3 == countHintMoveNow);
        }

        private void StartAnims()
        {
            backgroundPanel.SetActive(true);
            buyAllHintsPanel.SetActive(true);
            
            _backgroundPanelAnim.Play("BackgroundPanelUprise");
            _buyAllHintsPanelAnim.Play("PanelUprise");
        }
    }
}