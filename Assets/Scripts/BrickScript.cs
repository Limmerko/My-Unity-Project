using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrickScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] private float speed = 20f; // Prefab. Скорость движения
    [SerializeField] private GameObject[] waypointsPrefabs; // Prefab. Список всех waypoint'ов
    [SerializeField] private Sprite red;
    [SerializeField] private Sprite blue;
    [SerializeField] private Sprite yellow;
    [SerializeField] private Sprite green;
    [SerializeField] private Sprite black;
    [SerializeField] private Sprite white;

    private Brick _brick;
    private Transform _transform;

    public void SetBrick(Brick brick)
    {
        _brick = brick;
        _transform = GetComponent<Transform>();
        SetTypeBrick();
        Statics.AllBricks.Add(_brick);
    }
    
    private void FixedUpdate()
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
        if (!_brick.IsTouch && _brick.IsClickable && !Statics.IsGameOver && BrickUtils.AllTouchBricks().Count < 7)
        {
            _brick.IsTouch = true;
            _brick.Layer = 10;
            _transform.localScale = new Vector3(0.6f, 0.6f, 1);
            _brick.TargetWaypoint = BrickUtils.FindCurrentWaypoint(_brick.Type);
            BrickUtils.UpdateBricksState();
            BrickUtils.UpdateBricksPosition();
        }
    }

    /**
     * Нажатие на кирпичик
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_brick.IsTouch && _brick.IsClickable)
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
        SpriteRenderer sprite = GetComponent<SpriteRenderer>(); 
        switch (_brick.Type)
        {
            case BrickType.Red:
                sprite.sprite = red;
                break;
            case BrickType.Blue:
                sprite.sprite = blue;
                break;
            case BrickType.Yellow:
                sprite.sprite = yellow;
                break;
            case BrickType.Green:
                sprite.sprite = green;
                break;
            case BrickType.Black:
                sprite.sprite = black;
                break;
            case BrickType.White:
                sprite.sprite = white;
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