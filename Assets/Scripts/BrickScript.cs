using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 5f;

    private BoxCollider2D _collider;
    private bool _isTouch;
    
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
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
        if (Vector2.Distance(waypoints[0].transform.position, _collider.transform.position) == 0)
        {
            _isTouch = false;
        }
        _collider.transform.position = Vector2.MoveTowards(
            _collider.transform.position,
            waypoints[0].transform.position,
            Time.deltaTime * speed);
    }
}
