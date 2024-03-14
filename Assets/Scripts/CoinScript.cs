using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Vector3 _targetPosition; // Место расположения всех монет
    private TextMeshProUGUI _text; // Текст с кол-вом монет
    private Vector3 _startPosition; 

    public void SetCoin(Vector3 target, TextMeshProUGUI text)
    {
        _targetPosition = target;
        _text = text;
        _startPosition = gameObject.transform.position;
    }

    void Update()
    {
        Vector3 position = gameObject.transform.position;
        float distance = Vector3.Distance(_targetPosition, position);
        float distanceFromStart = Vector3.Distance(_startPosition, position); // Растояние от места появления
        MoveToCoins(position, distance, distanceFromStart);
        var targetSize = (float)Screen.width / Screen.height * 1.35f; // Просто такой размер подходит и равен монете в приземлении
        ChangeSize(targetSize, distance, distanceFromStart);
    }

    private void MoveToCoins(Vector3 position, float distance, float distanceFromStart)
    {
        _targetPosition.z = position.z;
        // Если закончил движение
        if (distance <= 0.005f)
        {
            _text.text = PlayerPrefs.GetInt("Coins").ToString();
            Destroy(gameObject);
        }
        else
        {
            float speed = distanceFromStart <= 0.1f ? distanceFromStart + 0.7f : distance / 0.1f; // Замедление в самом начале
            speed = speed >= 15f ? 15f : speed;
            MainUtils.MoveToWaypoint(_targetPosition, gameObject, speed); 
        }
    }

    private void ChangeSize(float size, float distance, float distanceFromStart)
    {
        Vector3 target = new Vector3(size, size, 0);
        var speed = distanceFromStart <= 0.1f ? 15f : distance / 0.09f;
        MainUtils.ChangeSize(target, gameObject, speed);
    }
}
