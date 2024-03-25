using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Buttons
{
    public class AddFinishPlace : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private AudioSource soundOnClick; // Звук нажатия на кнопку
        [SerializeField] private GameObject buttonAnimationObject; // Анимация исчезновения кнопки

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            gameObject.SetActive(PlayerPrefs.GetInt("MaxFinishTiles") == Statics.MaxFinishTiles);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerPrefs.SetInt("MaxFinishTiles", Statics.MaxFinishTiles + 1);
            PlayerPrefs.Save();
            _spriteRenderer.color = Statics.IsClickableColor;
            MainUtils.Vibrate();
            DisappearingAnimation();
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
        
        private void DisappearingAnimation()
        {
            gameObject.SetActive(false);
            buttonAnimationObject.SetActive(true);
        }
    }
}