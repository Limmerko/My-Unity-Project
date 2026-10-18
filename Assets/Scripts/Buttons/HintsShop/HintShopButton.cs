using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons.HintsShop
{
    /**
     * Кнопка "Магазин подсказок" 
     */
    public class HintShopButton : CommonButton
    {
        [SerializeField] protected GameObject backgroundPanel; // Панель паузы
        [SerializeField] protected GameObject buyAllHintsPanel; // Панель покупки подсказок
        [SerializeField] protected TextMeshProUGUI coinsAllPrice; // Текст с стоимостью всех подсказок
        [SerializeField] protected TextMeshProUGUI countCancelLastMove; // Текст с кол-вом подсказок отмены хода
        [SerializeField] protected TextMeshProUGUI countRefresh; // Текст с кол-вом подсказок перемешивания
        [SerializeField] protected TextMeshProUGUI countHintMove; // Текст с кол-вом подсказок подсказки хода
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

            int currentLevel = PlayerPrefs.GetInt("Level", 0) + 1;

            if (!PlayerPrefs.HasKey("Unlocked_" + HintCountType.CountCancelLastMove) ||
                !PlayerPrefs.HasKey("Unlocked_" + HintCountType.CountRefresh) ||
                !PlayerPrefs.HasKey("Unlocked_" + HintCountType.CountHintMove))
            {
                gameObject.GetComponent<Image>().color = Statics.IsNotClickableColor;
                enabled = false;
            }
        }
        
        protected override void Process()
        {
            Statics.TimeScale = 0;
            SetAllCountsEqualsZero();
            SetDisabledAllMinusButtons();
            SetDisabledAllPlusButtons();
            OneHintPlusButton.UpdateAllPlusButtonStates();
            StartAnims();
        }

        private void SetAllCountsEqualsZero()
        {
            coinsAllPrice.text = "0";
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
