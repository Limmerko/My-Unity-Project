using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject waypointsPrefab;
    [SerializeField] private float speed = 5f;

    private BoxCollider2D _collider;
    private bool _isTouch;
    private Transform[] _waypoints;
    
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _waypoints = waypointsPrefab.GetComponentsInChildren<Transform>();
        for (int i=0; i < _waypoints.Length;  i++)
        {
            Debug.Log(i + " " + _waypoints[i].position);
        }
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            if (_collider == Physics2D.OverlapPoint(touchPosition))
            {
                _isTouch = true;
            }
        }
        
        if (_isTouch)
        {
            moveBrickOnWaypoint();
        }
    }
    
    
    private void moveBrickOnWaypoint()
    {
        // нужна общая переменная на все скрипты (static?)
        if (Vector2.Distance(_waypoints[CurrentWaypoint].transform.position, _collider.transform.position) == 0)
        {
            Debug.Log("go to " + CurrentWaypoint);
            _isTouch = false;
            CurrentWaypoint++;
            if (CurrentWaypoint >= _waypoints.Length)
            {
                // TODO Временно
                CurrentWaypoint = 0;
            }
            Debug.Log(CurrentWaypoint);
        }
        _collider.transform.position = Vector2.MoveTowards(
            _collider.transform.position,
            _waypoints[CurrentWaypoint].transform.position,
            Time.deltaTime * speed);
    }
}
