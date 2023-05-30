using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrickScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{ 
    [SerializeField] private float moveSpeed = 20f; // Prefab. Скорость движения
    [SerializeField] private float sizeSpeed = 5f; // Prefab. Скорость изменения размера
    [SerializeField] private GameObject[] waypointsPrefabs; // Prefab. Список всех waypoint'ов
    [SerializeField] private Sprite red;
    [SerializeField] private Sprite blue;
    [SerializeField] private Sprite yellow;
    [SerializeField] private Sprite green;
    [SerializeField] private Sprite black;
    [SerializeField] private Sprite white;
    [SerializeField] private Sprite orange;
    [SerializeField] private Sprite pink;
    [SerializeField] private Animation anim;
    
    private Brick _brick;
    private Transform _transform;
    private float _sizeFinishBrick;
    private bool _isDown = false;

    public void SetBrick(Brick brick)
    {
        _brick = brick;
        _transform = GetComponent<Transform>();
        _sizeFinishBrick = BrickUtils.BrickSize(7);
        
        SetTypeBrick();
        Statics.AllBricks.Add(_brick);
        SetPositionWaypoints();
    }
    
    private void FixedUpdate()
    {
        if (_brick.IsTouch)
        {
            moveBrickOnWaypoint();
            ChangeSizeBrick(_sizeFinishBrick);
        }
        else
        {
            if (_isDown)
            {
                ChangeSizeBrick(_brick.Size * 0.9f);
            }
            else
            {
                ChangeSizeBrick(_brick.Size);
            }
        }
    }

    /**
     * Клик на кирпичик
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_brick.IsTouch && _brick.IsClickable && !Statics.IsGameOver && BrickUtils.AllTouchBricks().Count < 7)
        {
            // Изменение положения
            _brick.TargetWaypoint = BrickUtils.FindCurrentWaypoint(_brick.Type);
            _brick.IsTouch = true;
            _brick.Layer = 100;
            BrickUtils.UpdateBricksPosition();
            // Изменение состояния
            BrickUtils.UpdateBricksState();
            Vibration.VibrateAndroid(1); // TODO хз как на IOS будет
        }
    }

    /**
     * Нажатие на кирпичик
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_brick.IsTouch && _brick.IsClickable)
        {
            _isDown = true;
        }
    }
    
    /**
     * После нажатия курсор уходит от кирпичика
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_brick.IsTouch)
        {

            _isDown = false;
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
            case BrickType.Orange:
                sprite.sprite = orange;
                break;
            case BrickType.Pink:
                sprite.sprite = pink;
                break;
        }
    }

    private void SetPositionWaypoints()
    {
        float sizeAdd = _sizeFinishBrick / 2;
        waypointsPrefabs[0].transform.position = new Vector3(-3 * sizeAdd, waypointsPrefabs[0].transform.position.y, 0);
        waypointsPrefabs[1].transform.position = new Vector3(-2 * sizeAdd, waypointsPrefabs[1].transform.position.y, 0);
        waypointsPrefabs[2].transform.position = new Vector3(-1 * sizeAdd, waypointsPrefabs[2].transform.position.y, 0);
        waypointsPrefabs[3].transform.position = new Vector3(0, waypointsPrefabs[3].transform.position.y, 0);
        waypointsPrefabs[4].transform.position = new Vector3(1 * sizeAdd, waypointsPrefabs[4].transform.position.y, 0);
        waypointsPrefabs[5].transform.position = new Vector3(2 * sizeAdd, waypointsPrefabs[5].transform.position.y, 0);
        waypointsPrefabs[6].transform.position = new Vector3(3 * sizeAdd, waypointsPrefabs[6].transform.position.y, 0);
    }

    /**
     * Движение кирпичика до waypoint'a
     */
    private void moveBrickOnWaypoint()
    {
        if (_brick.TargetWaypoint < waypointsPrefabs.Length)
        {
            Vector3 target = waypointsPrefabs[_brick.TargetWaypoint].transform.position;
            Vector3 brickPosition = _brick.GameObject.transform.position;
            float distance = Vector3.Distance(target, brickPosition);
            target.z = brickPosition.z;
            // Если закончил движение
            if (distance <= 0.001f && !_brick.IsFinish)
            {
                _brick.IsFinish = true;
            }
            else
            {
                float minSpeed = 3f;
                float speed = distance >= 0.1f ? distance / 2 * moveSpeed : moveSpeed / 10f; // Замедление в конце
                speed = speed > moveSpeed ? moveSpeed : speed < minSpeed ? minSpeed : speed; // Ограничение максимальной и минимальной скорости
                MainUtils.MoveToWaypoint(target, _brick.GameObject, speed); 
            }
        }
    }

    private void ChangeSizeBrick(float size)
    {
        Vector3 target = new Vector3(size, size, 0);
        if (Vector3.Distance(target, _brick.GameObject.transform.localScale) >= 0.001f)
        {
            MainUtils.ChangeSize(target, _brick.GameObject, sizeSpeed);
        }
    }

    public float LengthClearAnim()
    {
        return anim["BrickClear"].length;
    }

    public void PlayClearAnim()
    {
        anim.Play("BrickClear");
    }
}