using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject[] waypointsPrefabs; // Prefab. Список всех waypoint'ов
    [SerializeField] private float speed = 5f; // Prefab. Скорость движения
    
    private Collider2D _collider; // Коллейдер для определения нажатия
    private int _targetWaypoint; // "Целевой" waypoint

    private Brick _brick;

    void Start()
    {
        _brick = new Brick(gameObject);
        _collider = _brick.GameObject.GetComponent<Collider2D>();
        Bricks.Add(_brick);
    }

    void Update()
    {
        // Если закончил движение, то больше нельзя нажать на него 
        if (_brick.IsFinish)
        {
            return;
        }

        // Если было нажатие на экран
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // Если нажатие было на сам кирпич
            if (_collider == Physics2D.OverlapPoint(touchPosition) && !_brick.IsTouch)
            {
                _brick.IsTouch = true;
                _targetWaypoint = CurrentWaypoint;
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
        // Если закончил движение
        if (Vector2.Distance(waypointsPrefabs[_targetWaypoint].transform.position, _collider.transform.position) == 0)
        {
            _brick.IsFinish = true;
        }

        _collider.transform.position = Vector2.MoveTowards(
            _collider.transform.position,
            waypointsPrefabs[_targetWaypoint].transform.position,
            Time.deltaTime * speed);
    }
}