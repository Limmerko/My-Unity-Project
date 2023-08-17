using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Vector3 _target; // Место расположения всех монет
    private TextMeshProUGUI _text; // Текст с кол-вом монет

    public void setCoin(Vector3 target, TextMeshProUGUI text)
    {
        _target = target;
        _text = text;
    }

    void Update()
    {
        Vector3 brickPosition = gameObject.transform.position;
        float distance = Vector3.Distance(_target, brickPosition);
        _target.z = brickPosition.z;
        // Если закончил движение
        if (distance <= 0.001f)
        {
            _text.text = PlayerPrefs.GetInt("Coins").ToString();
            Destroy(gameObject);
        }
        else
        {
            float speed = MainUtils.CountSpeed(_target, brickPosition, 5f);
            MainUtils.MoveToWaypoint(_target, gameObject, speed); 
        }
    }
}
