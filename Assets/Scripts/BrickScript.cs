using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject waypointsPrefab; // Prefab. Список всех waypoint'ов
    [SerializeField] private float speed = 5f; // Prefab. Скорость движения

    private BoxCollider2D _collider; // Коллайдер для опредения области
    private Transform[] _waypoints; // Список всех waypoint'ов
    private bool _isTouch; // Было нажатие на этот кирпич
    private bool _isFinish; // Закончил движение
    private int _targetWaypoint; // "Целевой" waypoint
    
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _waypoints = waypointsPrefab.GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        // Если закончил движение, то больше нельзя нажать на него 
        if (_isFinish)
        {
            return;
        }
        
        // Если было нажатие на экран
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // Если нажатие было на сам кирпич
            if (_collider == Physics2D.OverlapPoint(touchPosition) && !_isTouch)
            {
                _isTouch = true;
                _targetWaypoint = CurrentWaypoint;
                CurrentWaypoint++;
                if (CurrentWaypoint >= _waypoints.Length)
                {
                    // TODO Временно. Потом будет проигрыш
                    CurrentWaypoint = 1;
                }
            }
        }
        
        if (_isTouch)
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
        if (Vector2.Distance(_waypoints[_targetWaypoint].transform.position, _collider.transform.position) == 0)
        {
            _isFinish = true;
        }
        _collider.transform.position = Vector2.MoveTowards(
            _collider.transform.position,
            _waypoints[_targetWaypoint].transform.position,
            Time.deltaTime * speed);
    }
}
