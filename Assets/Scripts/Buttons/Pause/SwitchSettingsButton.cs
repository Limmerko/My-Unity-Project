using System;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Buttons.Pause
{
    /**
     * Кнопка влюкчения/выключения настроек
     */
    public class SwitchSettingsButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SettingsType settingsType;
        [SerializeField] private AudioSource soundOnClick; // Звук нажатия на кнопку
        
        private Animator _animator;
        private SpriteRenderer _image;

        private enum Switcher
        {
            Off, On, OnDefault
        }

        protected void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
        }

        protected void OnEnable()
        {
            _animator.SetInteger("switch", PlayerPrefs.GetInt(settingsType.ToString()).Equals((int) Switcher.Off) ? 
                (int) Switcher.Off : 
                (int) Switcher.OnDefault);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int result = PlayerPrefs.GetInt(settingsType.ToString()).Equals((int) Switcher.On) ? 
                (int) Switcher.Off : 
                (int) Switcher.On;
            _animator.SetInteger("switch", result);
            MainUtils.PlaySound(soundOnClick);
            PlayerPrefs.SetInt(settingsType.ToString(), result);
            PlayerPrefs.Save();
        }
    }
}