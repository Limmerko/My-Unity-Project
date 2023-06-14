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
    [SerializeField] private Animation anim; // Анимация исчезнование
    
    [SerializeField] private Sprite[] iceCream;
    [SerializeField] private Sprite[] pizza;
    [SerializeField] private Sprite[] burger;
    [SerializeField] private Sprite[] escimo; 
    [SerializeField] private Sprite[] watermelon;
    [SerializeField] private Sprite[] peach;
    [SerializeField] private Sprite[] cherry;
    [SerializeField] private Sprite[] apple;
    [SerializeField] private Sprite[] avocado;
    [SerializeField] private Sprite[] strawberry;

    private Brick _brick;
    private SpriteRenderer _sprite;
    private float _sizeFinishBrick;

    public void SetBrick(Brick brick)
    {
        _brick = brick;
        _sprite = GetComponent<SpriteRenderer>(); 
        _sizeFinishBrick = BrickUtils.BrickSize(7);
        
        SetSpriteBrick();
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
            if (!_brick.IsDown)
            {
                ChangeSizeBrick(_brick.Size);
            }
        }
        SetSpriteBrick();
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
            _brick.IsDown = false;
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
            _brick.IsDown = true;
        }
    }
    
    /**
     * После нажатия курсор уходит от кирпичика
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_brick.IsTouch)
        {
            _brick.IsDown = false;
        }
    }

    /**
     * Установка спрайта кирпичика по типу
     */
    private void SetSpriteBrick()
    {
        Sprite setType = null;
        switch (_brick.Type)
        {
            // TODO порефакторить
            case BrickType.IceCream:
                setType = iceCream[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.Pizza:
                setType = pizza[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.Burger:
                setType = burger[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.Escimo:
                setType = escimo[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.Watermelon:
                setType = watermelon[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.Peach:
                setType = peach[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.Cherry:
                setType = cherry[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.Apple:
                setType = apple[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.Avocado:
                setType = avocado[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.Strawberry:
                setType = strawberry[_brick.IsDown ? 1 : 0];
                break; 
        }

        if (!_sprite.sprite.Equals(setType))
        {
            _sprite.sprite = setType;
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