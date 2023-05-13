using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject[] waypointsPrefabs; // Prefab. Список всех waypoint'ов
    [SerializeField] private float speed = 10f; // Prefab. Скорость движения
    
    private Collider2D _collider; // Коллейдер для определения нажатия

    private Brick _brick;
    
    public void SetBrick(Brick brick)
    {
        _brick = brick;
        _collider = _brick.GameObject.GetComponent<Collider2D>();
        SpriteRenderer spriteRenderer = _brick.GameObject.GetComponent<SpriteRenderer>();
        switch (_brick.Type)
        {
            case BrickType.Red:
                spriteRenderer.color = Color.red;
                break;
            case BrickType.Blue:
                spriteRenderer.color = Color.blue;
                break;
            case BrickType.Yellow:
                spriteRenderer.color = Color.yellow;
                break;
                
        }

        Statics.AllBricks.Add(_brick);
    }

    void Update()
    {
        // Если было нажатие на экран и кирпич не приехал
        if (Input.touchCount > 0 && !_brick.IsTouch)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // Если нажатие было на сам кирпич
            if (_collider == Physics2D.OverlapPoint(touchPosition) && !_brick.IsTouch)
            {
                _brick.TargetWaypoint = BrickUtils.FindCurrentWaypoint(_brick.Type);
                _brick.IsTouch = true;
                BrickUtils.UpdateBricksPosition();
            }
        }

        if (_brick.IsTouch)
        {
            moveBrickOnWaypoint();
        }
    }

    /**
     * Движение кирпичика до waypoint'a
     */
    private void moveBrickOnWaypoint()
    {
        Vector2 target = waypointsPrefabs[_brick.TargetWaypoint].transform.position;
        
        // Если закончил движение
        if (Vector2.Distance(target, _collider.transform.position) == 0 && !_brick.IsFinish)
        {
            _brick.IsFinish = true;
        }

        MainUtils.MoveToWaypoint(target, _collider, speed);
    }
}