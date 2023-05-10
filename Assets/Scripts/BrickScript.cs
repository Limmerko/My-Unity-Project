using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject[] waypointsPrefabs; // Prefab. Список всех waypoint'ов
    [SerializeField] private float speed = 5f; // Prefab. Скорость движения
    
    private Collider2D _collider; // Коллейдер для определения нажатия

    private Brick _brick;

    void Start()
    {
        _brick = new Brick(gameObject);
        _collider = _brick.GameObject.GetComponent<Collider2D>();
        Bricks.Add(_brick);
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
                _brick.IsTouch = true;
                _brick.TargetWaypoint = CurrentWaypoint;
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
        if (Vector2.Distance(target, _collider.transform.position) == 0)
        {
            _brick.IsFinish = true;
        }

        MoveToWaypoint(target, _collider, speed);
    }
}