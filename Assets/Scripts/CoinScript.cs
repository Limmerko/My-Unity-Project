using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Vector3 _targetPosition; // Место расположения всех монет
    private Vector3 _targetSize; // Финальный размер монеты
    private TextMeshProUGUI _text; // Текст с кол-вом монет
    private Vector3 _startPosition;
    private Vector3 _startSize;
    private float _totalDistance;

    public void SetCoin(Vector3 targetPosition, Vector3 targetSize, TextMeshProUGUI text)
    {
        _targetPosition = targetPosition;
        _targetSize = targetSize;
        _text = text;

        _startPosition = transform.position;
        _startSize = transform.localScale;

        // Монета движется только по X/Y, Z во время движения сохраняется текущим.
        // Поэтому сразу приводим Z цели к Z монеты, чтобы полная дистанция была рассчитана корректно.
        _targetPosition.z = _startPosition.z;
        _totalDistance = Vector3.Distance(_startPosition, _targetPosition);
    }

    void Update()
    {
        Vector3 position = transform.position;
        float distance = Vector3.Distance(_targetPosition, position);
        float distanceFromStart = Vector3.Distance(_startPosition, position); // Расстояние от места появления

        MoveToCoins(position, distance, distanceFromStart);
        ChangeSize(distanceFromStart);
    }

    private void MoveToCoins(Vector3 position, float distance, float distanceFromStart)
    {
        _targetPosition.z = position.z;

        // Если закончил движение
        if (distance <= 0.005f)
        {
            // Гарантированно устанавливаем точный финальный размер.
            transform.localScale = _targetSize;

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

    private void ChangeSize(float distanceFromStart)
    {
        if (_totalDistance <= Mathf.Epsilon)
        {
            transform.localScale = _targetSize;
            return;
        }

        float progress = Mathf.Clamp01(distanceFromStart / _totalDistance);

        // В начале размер меняется немного быстрее, а ближе к цели плавно догоняет target.
        // При progress = 1 easedProgress тоже всегда равен 1, поэтому финальный размер точный.
        float easedProgress = 1f - Mathf.Pow(1f - progress, 2f);

        transform.localScale = Vector3.Lerp(_startSize, _targetSize, easedProgress);
    }
}
