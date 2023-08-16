using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Buttons
{
    public abstract class CommonButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private Sprite[] sprites; // Спрайты (0 - ненажатый, 1 - нажатый)
        [SerializeField] protected GameObject iconUp; // Иконка или текст  при ненажатой кнопки
        [SerializeField] protected GameObject iconDown; // Иконка или текст при нажатой кнопки (Используется только его позиция)

        private Image _image; // Компонент для смены спрайтов
        private Transform _iconTransform; // Позиция иконки или текста

        private Vector3 _upPosition; // Позиция иконки или текста, когда кнопка не нажата 
        private Vector3 _downPosition; // Позиция иконки или текста, когда кнопка нажата

        protected void Start()
        {
            _image = gameObject.GetComponent<Image>();
            _iconTransform = iconUp.transform;
            _upPosition = _iconTransform.localPosition;
            _downPosition = iconDown.transform.localPosition;
            StartProcess();
        }

        /**
         * Клик на кнопку
        */
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            _iconTransform.localPosition = new Vector3(_upPosition.x, _upPosition.y, 0);
            _image.sprite = sprites[0];
            Process();
            MainUtils.Vibrate();
        }

        /**
        * Нажатие на кнопку
        */
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _iconTransform.localPosition = new Vector3(_downPosition.x, _downPosition.y, 0);
            _image.sprite = sprites[1];
        }

        /**
        * Курсор уходит от кнопки
        */
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _iconTransform.localPosition = new Vector3(_upPosition.x, _upPosition.y, 0);
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