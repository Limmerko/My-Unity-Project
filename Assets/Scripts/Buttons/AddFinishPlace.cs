using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Buttons
{
    public class AddFinishPlace : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private AudioSource soundOnClick; // Звук нажатия на кнопку

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Statics.MaxFinishTiles = 8;
            _spriteRenderer.color = Statics.IsClickableColor;
            MainUtils.Vibrate();
            gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _spriteRenderer.color = Statics.IsNotClickableColor;
            MainUtils.PlaySound(soundOnClick);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _spriteRenderer.color = Statics.IsClickableColor;
        }
    }
}