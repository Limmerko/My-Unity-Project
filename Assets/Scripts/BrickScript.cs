using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class BrickScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{ 
    [SerializeField] private float moveSpeed = 20f; // Prefab. Скорость движения
    [SerializeField] private float sizeSpeed = 5f; // Prefab. Скорость изменения размера
    [SerializeField] private GameObject waypointPrefab; // Prefab. Центральный waypoint
    [SerializeField] private Animation anim; // Анимация исчезнование
    [SerializeField] private AudioSource soundMoveForFinish; // Звук движения до финиша
    [SerializeField] private List<TileType> types; // Типы
    [SerializeField] private Sprite[] unknownTile; // Спрайты для "неизвестной" плитки
    [SerializeField] private GameObject objectForAnimUnknownTile; // Объект для анимации "Неизвестной" плитки

    private Brick _brick;
    private SpriteRenderer _sprite;
    private float _sizeFinishBrick;
    private List<Vector3> _waypointsPrefabs = new(); // Prefab. Список всех waypoint'ов

    public void SetBrick(Brick brick, float sizeFinishBrick)
    {
        objectForAnimUnknownTile.SetActive(false);
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
        SetSpriteBrick(); // TODO наверно стоит вынести этот метод отсюда и вызывать только когда нужно
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
        
        MainUtils.PlaySound(soundMoveForFinish);
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
        TileType type = types.First(type => type.type.Equals(_brick.Type));

        if (_brick.IsUnknownTile)
        {
            _sprite.sprite = _brick.IsDown ? unknownTile[1] : unknownTile[0];
            return;
        }
        
        if (_brick.IsGolden())
        {
            TileType.AnyType golden = type.goldens[_brick.GoldenStateMoves - 1];
            _sprite.sprite = _brick.IsDown ? golden.spriteDown : golden.spriteUp;
        }
        else if (_brick.IsLive())
        {
            TileType.AnyType golden = type.lives[_brick.LiveStateMoves - 1];
            _sprite.sprite = _brick.IsDown ? golden.spriteDown : golden.spriteUp;
        }
        else
        {
            _sprite.sprite = _brick.IsDown ? type.spriteDown : type.spriteUp;
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
                if (_brick.IsUnknownTile) {
                    _brick.IsUnknownTile = false;
                    StartCoroutine(AnimUnknownTile());
                }
                MainUtils.SaveProgress();
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

    private IEnumerator AnimUnknownTile()
    {
        objectForAnimUnknownTile.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        objectForAnimUnknownTile.SetActive(false);
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