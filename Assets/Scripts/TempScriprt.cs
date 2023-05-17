using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TempScriprt : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    private void Start()
    {
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("клик");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Нажал");
    }
}
