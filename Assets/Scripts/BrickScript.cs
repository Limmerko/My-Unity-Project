using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrickScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] private GameObject[] waypointsPrefabs; // Prefab. Список всех waypoint'ов
    [SerializeField] private float speed = 20f; // Prefab. Скорость движения

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
     * Клик на кирпичик
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_brick.IsTouch && !Statics.IsGameOver && BrickUtils.AllTouchBricks().Count < 7)
        {
            _brick.TargetWaypoint = BrickUtils.FindCurrentWaypoint(_brick.Type);
            _brick.IsTouch = true;
            _transform.localScale = new Vector3(0.55f, 0.55f, 1);
            BrickUtils.UpdateBricksPosition();
        }
    }

    /**
     * Нажатие на кирпичик
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_brick.IsTouch)
        {
            _transform.localScale = new Vector3(0.45f, 0.45f, 1);
        }
    }
    
    /**
     * После нажатия курсор уходит от кирпичика
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_brick.IsTouch)
        {
            _transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
    }

    /**
     * Установка спрайта кирпичика по типу
     */
    private void SetTypeBrick()
    {
        SpriteRenderer sprite = _brick.GameObject.GetComponent<SpriteRenderer>();
        switch (_brick.Type)
        {
            case BrickType.Red:
                sprite.color = Color.red;
                break;
            case BrickType.Blue:
                sprite.color = Color.blue;
                break;
            case BrickType.Yellow:
                sprite.color = Color.yellow;
                break;
            case BrickType.Green:
                sprite.color = Color.green;
                break;
            case BrickType.Black:
                sprite.color = Color.black;
                break;
            case BrickType.White:
                sprite.color = Color.white;
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
            Vector3 target = waypointsPrefabs[_brick.TargetWaypoint].transform.position;

            // Если закончил движение
            if (Vector3.Distance(target, _brick.GameObject.transform.position) <= 0.0001f && !_brick.IsFinish)
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