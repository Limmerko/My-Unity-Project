using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Buttons.Hint
{
    public abstract class CommonHintButton : CommonButton
    {
        [SerializeField] private GameObject count; // Кол-во доступных подсказок
        [SerializeField] protected GameObject countDown;  // Иконка при нажатой кнопки (Используется только его позиция)
        [SerializeField] private Sprite[] countSprites; // Спрайты для кол-ва подсказок
        [SerializeField] protected GameObject backgroundPanel; // Панель паузы
        [SerializeField] protected GameObject buyHintPanel; // Панель покупки подсказки
        [SerializeField] protected GameObject refreshSprite; // Инока подсказки для покупки "Перемешивания"
        [SerializeField] protected GameObject cancelLastMoveSprite; // Инока подсказки для покупки "Отмены хода" 
        [SerializeField] protected GameObject hintMoveSprite; // Инока подсказки для покупки "Подсказки хода"
        
        private Animation _backgroundPanelAnim; // Анимация фона паузы
        private Animation _buyHintPanelAnim; // Анимация панель покупки подсказки
        
        private Image _countImage; // Компонент для смены спрайтов
        private TextMeshProUGUI _countText; // Текст кол-ва подсказок
        private Transform _countTransform;
        private Vector3 _countUpPosition; // Позиция иконки, когда кнопка не нажата 
        private Vector3 _countDownPosition; // Позиция иконки, когда кнопка нажата

        protected String PrefCount { get; set; }
        
        protected override void StartProcess()
        {
            if (!PlayerPrefs.HasKey(PrefCount))
            {
                PlayerPrefs.SetInt(PrefCount, 3);
                PlayerPrefs.Save();
            }
            else // TODO временно, убрать потом
            {
                PlayerPrefs.SetInt(PrefCount, 3);
                PlayerPrefs.SetInt("Coins", 1000);
                PlayerPrefs.Save();
            }
            
            _countImage = count.gameObject.GetComponent<Image>();
            _countTransform = count.transform;
            _countUpPosition = _countTransform.localPosition;
            _countDownPosition = countDown.transform.localPosition;
            _countText = count.GetComponentInChildren<TextMeshProUGUI>();
            _countText.text = PlayerPrefs.GetInt(PrefCount).ToString();
            _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
            buyHintPanel.SetActive(false);
            _buyHintPanelAnim = buyHintPanel.GetComponent<Animation>();

            CountSymbol();
            CheckCountSprite();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (PlayerPrefs.GetInt(PrefCount) == 0)
            {
                OpenBuyHintPanel();
            }
            else
            {
                base.OnPointerClick(eventData);
                _countTransform.localPosition = _countUpPosition;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _countTransform.localPosition = _countDownPosition;
        }
        
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            _countTransform.localPosition = _countUpPosition;
        }

        private void Update()
        {
            CountSymbol();
            CheckCountSprite();
        }

        /**
         * Актуализация кол-ва подсказок
         */
        protected void CheckCount()
        {
            int countInt = PlayerPrefs.GetInt(PrefCount) - 1;
            PlayerPrefs.SetInt(PrefCount, countInt);
            _countText.text = countInt > 0 ? countInt.ToString() : "+";
            PlayerPrefs.Save();
            
            CountSymbol();
            CheckCountSprite();
        }

        private void CountSymbol()
        {
            int countInt = PlayerPrefs.GetInt(PrefCount);
            _countText.text = countInt > 0 ? countInt.ToString() : "+";
        }

        /**
         * Установка нужного спрайта
         */
        private void CheckCountSprite()
        {
            _countImage.sprite = PlayerPrefs.GetInt(PrefCount) > 0 ? countSprites[0] : countSprites[1];
        }

        private void OpenBuyHintPanel()
        {
            Statics.TimeScale = 0;
            backgroundPanel.SetActive(true);
            buyHintPanel.SetActive(true);
            _backgroundPanelAnim.Play("BackgroundPanelUprise");
            _buyHintPanelAnim.Play("PanelUprise");

            // Установка иконки какая именно подска покупается
            if ("CountCancelLastMove".Equals(PrefCount))
            {
                cancelLastMoveSprite.SetActive(true);
                refreshSprite.SetActive(false);
                hintMoveSprite.SetActive(false);
            }
            else if ("CountHintMove".Equals(PrefCount))
            {
                cancelLastMoveSprite.SetActive(false);
                refreshSprite.SetActive(false);
                hintMoveSprite.SetActive(true);
            }
            else
            {
                cancelLastMoveSprite.SetActive(false);
                refreshSprite.SetActive(true);
                hintMoveSprite.SetActive(false);
            }

            TextMeshProUGUI hintCountText = GameObject.Find("HintCount").GetComponent<TextMeshProUGUI>();
            hintCountText.text = "1";

            TextMeshProUGUI coinsPriceText = GameObject.Find("CoinsPrice").GetComponent<TextMeshProUGUI>();
            coinsPriceText.text = "100";

            
            PlayerPrefs.SetString("LastHint", PrefCount);
            PlayerPrefs.Save();
        }
    }
}
