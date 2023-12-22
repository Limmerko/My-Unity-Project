using System;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Buttons.Pause
{
    /**
     * Кнопка влюкчения/выключения настроек
     */
    public class SwitchSettingsButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SettingsType settingsType;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            int result = PlayerPrefs.GetInt(settingsType.ToString()).Equals(1) ? 0 : 1;
            PlayerPrefs.SetInt(settingsType.ToString(), result);
            PlayerPrefs.Save();
        }
    }
}