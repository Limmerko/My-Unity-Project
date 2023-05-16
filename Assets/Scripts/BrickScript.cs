using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject[] waypointsPrefabs; // Prefab. Список всех waypoint'ов
    [SerializeField] private float speed = 10f; // Prefab. Скорость движения

    private Brick _brick;
    private Transform _transform;

    public void SetBrick(Brick brick)
    {
        _brick = brick;
        _transform = GetComponent<Transform>();
        SetTypeBrick();
        Statics.AllBricks.Add(_brick);
    }

    void Update()
    {
        if (_brick.IsTouch)
        {
            moveBrickOnWaypoint();
        }
    }

    /**
     * Нажатие на кирпичик
     */
    public void ClickUpOnBrick()
    {
        if (!_brick.IsTouch && !Statics.IsGameOver && BrickUtils.AllTouchBricks().Count < 7)
        {
            _brick.TargetWaypoint = BrickUtils.FindCurrentWaypoint(_brick.Type);
            _brick.IsTouch = true;
            _transform.localScale = new Vector3(1.2f, 1.2f, 1);
            BrickUtils.UpdateBricksPosition();
        }
    }

    public void ClickDownOnBrick()
    {
        _transform.localScale = new Vector3(0.9f, 0.9f, 1);
    }

    public void ClickExitFromBrick()
    {
        if (!_brick.IsTouch)
        {
            _transform.localScale = new Vector3(1f, 1f, 1);
        }
    }

    /**
     * Установка спрайта кирпичика по типу
     */
    private void SetTypeBrick()
    {
        Image image = _brick.GameObject.GetComponent<Image>();
        switch (_brick.Type)
        {
            case BrickType.Red:
                image.color = Color.red;
                break;
            case BrickType.Blue:
                image.color = Color.blue;
                break;
            case BrickType.Yellow:
                image.color = Color.yellow;
                break;
            case BrickType.Green:
                image.color = Color.green;
                break;
            case BrickType.Black:
                image.color = Color.black;
                break;
            case BrickType.White:
                image.color = Color.white;
                break;
        }
    }

    /**
     * Движение кирпичика до waypoint'a
     */
    private void moveBrickOnWaypoint()
    {
        if (_brick.TargetWaypoint < waypointsPrefabs.Length)
        {
            Vector2 target = waypointsPrefabs[_brick.TargetWaypoint].transform.position;

            // Если закончил движение
            if (Vector2.Distance(target, _brick.GameObject.transform.position) <= 0.0001f && !_brick.IsFinish)
            {
                _brick.IsFinish = true;
            }
            else
            {
                MainUtils.MoveToWaypoint(target, _brick.GameObject, speed); 
            }
        }
    }
}