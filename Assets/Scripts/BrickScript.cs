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
    [SerializeField] private GameObject waypointPrefab; // Prefab. Центральный waypoint
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
    [SerializeField] private Sprite[] lollipop;
    [SerializeField] private Sprite[] lollipopCandy;
    [SerializeField] private Sprite[] donut;
    [SerializeField] private Sprite[] fries;
    [SerializeField] private Sprite[] muffin;
    [SerializeField] private Sprite[] cake;
    [SerializeField] private Sprite[] cookie;
    [SerializeField] private Sprite[] coffee;
    [SerializeField] private Sprite[] sandwichWithEggs;
    [SerializeField] private Sprite[] cupOfCoffee;

    private Brick _brick;
    private SpriteRenderer _sprite;
    private float _sizeFinishBrick;
    private List<Vector3> _waypointsPrefabs = new(); // Prefab. Список всех waypoint'ов

    public void SetBrick(Brick brick, float sizeFinishBrick)
    {
        _brick = brick;
        _sprite = GetComponent<SpriteRenderer>(); 
        _sizeFinishBrick = sizeFinishBrick;
        
        SetSpriteBrick();
        Statics.AllBricks.Add(_brick);
        SetPositionWaypoints();
    }

    private void FixedUpdate()
    {
        if (Statics.TimeScale == 0) return;
        
        if (_brick.IsTouch)
        {
            MoveBrickOnWaypoint();
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
        SwipeBrick();
        CancelLastMove();
    }

    /**
     * Клик на кирпичик
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_brick.IsTouch || 
            !_brick.IsClickable || 
            Statics.IsGameOver ||
            BrickUtils.AllTouchAndIsNotToDestroyBricks().Count >= 7) return;
        
        // Добавление плитки в список последний ходов
        _brick.LastMoveState = (Brick) _brick.Clone();
        Statics.LastMoves.Add(_brick);
            
        // Изменение положения
        _brick.TargetWaypoint = BrickUtils.FindCurrentWaypoint(_brick.Type);
        _brick.IsTouch = true;
        _brick.IsDown = false;
        _brick.Layer = 100;
        BrickUtils.UpdateBricksPosition();
            
        // Изменение состояния
        BrickUtils.UpdateBricksState();
            
        // Вибрация
        MainUtils.Vibrate();
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
     * Курсор уходит от кирпичика
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
            case BrickType.Lollipop:
                setType = lollipop[_brick.IsDown ? 1 : 0];
                break; 
            case BrickType.LollipopCandy:
                setType = lollipopCandy[_brick.IsDown ? 1 : 0];
                break; 
            case BrickType.Donut:
                setType = donut[_brick.IsDown ? 1 : 0];
                break; 
            case BrickType.Fries:
                setType = fries[_brick.IsDown ? 1 : 0];
                break; 
            case BrickType.Muffin:
                setType = muffin[_brick.IsDown ? 1 : 0];
                break; 
            case BrickType.Cake:
                setType = cake[_brick.IsDown ? 1 : 0];
                break; 
            case BrickType.Cookie:
                setType = cookie[_brick.IsDown ? 1 : 0];
                break; 
            case BrickType.Coffee:
                setType = coffee[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.SandwichWithEggs:
                setType = sandwichWithEggs[_brick.IsDown ? 1 : 0];
                break;
            case BrickType.CupOfCoffee:
                setType = cupOfCoffee[_brick.IsDown ? 1 : 0];
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
        float y = Camera.main.ScreenToWorldPoint(waypointPrefab.GetComponent<RectTransform>().transform.position).y;
        _waypointsPrefabs.Add(new Vector3(-3 * sizeAdd, y, 0));
        _waypointsPrefabs.Add(new Vector3(-2 * sizeAdd, y, 0));
        _waypointsPrefabs.Add(new Vector3(-1 * sizeAdd, y, 0));
        _waypointsPrefabs.Add(new Vector3(-0 * sizeAdd, y, 0));
        _waypointsPrefabs.Add(new Vector3(1 * sizeAdd, y, 0));
        _waypointsPrefabs.Add(new Vector3(2 * sizeAdd, y, 0));
        _waypointsPrefabs.Add(new Vector3(3 * sizeAdd, y, 0));
    }

    /**
     * Движение кирпичика до waypoint'a
     */
    private void MoveBrickOnWaypoint()
    {
        if (_brick.TargetWaypoint < _waypointsPrefabs.Count)
        {
            Vector3 target = _waypointsPrefabs[_brick.TargetWaypoint];
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
                float speed = MainUtils.CountSpeed(target, brickPosition, moveSpeed);
                MainUtils.MoveToWaypoint(target, _brick.GameObject, speed); 
            }
        }
    }

    /**
     * Изменение размера кирпичика
     */
    private void ChangeSizeBrick(float size)
    {
        Vector3 target = new Vector3(size, size, 0);
        if (Vector3.Distance(target, _brick.GameObject.transform.localScale) >= 0.001f)
        {
            MainUtils.ChangeSize(target, _brick.GameObject, sizeSpeed);
        }
    }

    /**
     * Изменение состояния и положения кирпичика при нажатии на подсказку "Перемешать"
     */
    private void SwipeBrick()
    {
        if (_brick.IsSwipe)
        {
            if (_brick.TargetPosition != _brick.GameObject.transform.position)
            {
                MainUtils.MoveToWaypoint(_brick.TargetPosition, _brick.GameObject, 5f);
                _brick.GameObject.GetComponent<SpriteRenderer>().sortingOrder = _brick.Layer;
            }
            else
            {
                _brick.IsSwipe = false;
            }
        }
    }
    
    /**
     * Изменение состояния и положения кирпичика при нажатии на подсказку "Отменить последний ход"
     */
    private void CancelLastMove()
    {
        if (_brick.IsLastMove)
        {
            if (_brick.TargetPosition != _brick.GameObject.transform.position)
            {
                Vector3 target = _brick.TargetPosition;
                Vector3 brickPosition = _brick.GameObject.transform.position;
                float speed = MainUtils.CountSpeed(target, brickPosition, moveSpeed);
                MainUtils.MoveToWaypoint(target, _brick.GameObject, speed); 
            }
            else
            {
                _brick.IsLastMove = false;
            }
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