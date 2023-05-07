using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject waypointsPrefab; // Prefab. Список всех waypoint'ов
    [SerializeField] private float speed = 5f; // Prefab. Скорость движения

    private Transform[] _waypoints; // Список всех waypoint'ов
    private int _targetWaypoint; // "Целевой" waypoint

    private Brick _brick;
    
    void Start()
    {
        _brick = new Brick(GetComponent<BoxCollider2D>());
        Bricks.Add(_brick);
        _waypoints = waypointsPrefab.GetComponentsInChildren<Transform>();
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
            if (_brick.Collider == Physics2D.OverlapPoint(touchPosition) && !_brick.IsTouch)
            {
                _brick.IsTouch = true;
                _targetWaypoint = CurrentWaypoint;
                CurrentWaypoint++;
                if (CurrentWaypoint >= _waypoints.Length)
                {
                    // TODO Временно. Потом будет проигрыш
                    CurrentWaypoint = 1;
                }
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
        if (Vector2.Distance(_waypoints[_targetWaypoint].transform.position, _brick.Collider.transform.position) == 0)
        {
            _brick.IsFinish = true;
        }
        _brick.Collider.transform.position = Vector2.MoveTowards(
            _brick.Collider.transform.position,
            _waypoints[_targetWaypoint].transform.position,
            Time.deltaTime * speed);
    }
}
