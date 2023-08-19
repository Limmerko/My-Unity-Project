using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Vector3 _targetPosition; // Место расположения всех монет
    private TextMeshProUGUI _text; // Текст с кол-вом монет

    public void SetCoin(Vector3 target, TextMeshProUGUI text)
    {
        _targetPosition = target;
        _text = text;
    }

    void Update()
    {
        MoveToCoins();
        ChangeSize(0.8f);
    }

    private void MoveToCoins()
    {
        Vector3 position = gameObject.transform.position;
        float distance = Vector3.Distance(_targetPosition, position);
        _targetPosition.z = position.z;
        // Если закончил движение
        if (distance <= 0.001f)
        {
            _text.text = PlayerPrefs.GetInt("Coins").ToString();
            Destroy(gameObject);
        }
        else
        {
            float speed = MainUtils.CountSpeed(_targetPosition, position, 15f);
            MainUtils.MoveToWaypoint(_targetPosition, gameObject, speed); 
        }
    }

    private void ChangeSize(float size)
    {
        Vector3 target = new Vector3(size, size, 0);
        MainUtils.ChangeSize(target, gameObject, 5f);
    }
}
