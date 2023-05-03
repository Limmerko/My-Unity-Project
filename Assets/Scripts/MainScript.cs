using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField] private GameObject brick;

    private BoxCollider2D _brick1;
    
    private void Start()
    {
        _brick1 = Instantiate(brick, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("Тач");
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            if (_brick1 == Physics2D.OverlapPoint(touchPosition))
            {
                Debug.Log("В яблочко");
            }
        }
    }
}
