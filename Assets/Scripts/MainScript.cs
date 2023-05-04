using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private GameObject[] waypoints;

    private Brick _brick1;
    
    private void Start()
    {
        _brick1 = new Brick(Instantiate(brick, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<BoxCollider2D>());
        
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            if (_brick1.Collider == Physics2D.OverlapPoint(touchPosition))
            {
                _brick1.IsTouch = true;
            }
        }
        
        if (_brick1.IsTouch)
        {
            moveBrickOnWaypoint();
        }
    }

    private void moveBrickOnWaypoint()
    {
        if (Vector2.Distance(waypoints[0].transform.position, _brick1.Collider.transform.position) < .1f)
        {
            _brick1.IsTouch = false;
        }
        _brick1.Collider.transform.position = Vector2.MoveTowards(
            _brick1.Collider.transform.position,
            waypoints[0].transform.position,
            Time.deltaTime * 5f);
    }

    private class Brick
    {
        public BoxCollider2D Collider { get; set; }

        public bool IsTouch { get; set; }

        public Brick(BoxCollider2D boxCollider2D)
        {
            Collider = boxCollider2D;
        }
    }
}
