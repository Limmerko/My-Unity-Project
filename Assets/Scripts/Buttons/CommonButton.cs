using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Buttons
{
    public abstract class CommonButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private Sprite[] sprites; // Спрайты (0 - ненажатый, 1 - нажатый)
        [SerializeField] private TextMeshProUGUI textMeshUp; // Текст при ненажатой кнопки

        [SerializeField]
        private TextMeshProUGUI textMeshDown; // Текст при нажатой кнопки (Используется только его позиция)

        private Image _image; // Компонент для смены спрайтов
        private Transform _textTransform; // Позиция текста

        private Vector3 _upPosition; // Позиция текста, когда кнопка не нажата 
        private Vector3 _downPosition; // Позиция текста, когда кнопка нажата

        protected void Start()
        {
            _image = gameObject.GetComponent<Image>();
            _textTransform = textMeshUp.transform;
            _upPosition = _textTransform.localPosition;
            _downPosition = textMeshDown.transform.localPosition;
            StartProcess();
        }

        /**
         * Клик на кнопку
        */
        public void OnPointerClick(PointerEventData eventData)
        {
            _textTransform.localPosition = new Vector3(_upPosition.x, _upPosition.y, 0);
            _image.sprite = sprites[0];
            Process();
        }

        /**
        * Нажатие на кнопку
        */
        public void OnPointerDown(PointerEventData eventData)
        {
            _textTransform.localPosition = new Vector3(_downPosition.x, _downPosition.y, 0);
            _image.sprite = sprites[1];
        }

        /**
        * Курсор уходит от кнопки
        */
        public void OnPointerExit(PointerEventData eventData)
        {
            _textTransform.localPosition = new Vector3(_upPosition.x, _upPosition.y, 0);
            _image.sprite = sprites[0];
        }

        /**
        * Выполнение действий при нажатии на кнопку
        */
        protected abstract void Process();

        /**
         * Выполнение действий при старте
        */
        protected abstract void StartProcess();
    }
}