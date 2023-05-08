using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    
    private void Start()
    {
        // Create one brick
        Instantiate(brickPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<BoxCollider2D>();
        Instantiate(brickPrefab, new Vector3(-1, 0, 0), Quaternion.identity).GetComponent<BoxCollider2D>();
        Instantiate(brickPrefab, new Vector3(-2, 0, 0), Quaternion.identity).GetComponent<BoxCollider2D>();
        Instantiate(brickPrefab, new Vector3(1, 0, 0), Quaternion.identity).GetComponent<BoxCollider2D>();
        Instantiate(brickPrefab, new Vector3(2, 0, 0), Quaternion.identity).GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        ClearBrick();
    }

    private void ClearBrick()
    {
        List<Brick> isFinishList = Statics.Bricks.Where(brick => brick.IsFinish).ToList();

        if (isFinishList.Count == 3)
        {
            Debug.Log("3 кирпичика доехали");
            isFinishList.ForEach(brick =>
            {
                Destroy(brick.GameObject);
                Statics.Bricks.Remove(brick);
            });
            
        }

        Statics.CurrentWaypoint = Statics.Bricks.Count(brick => brick.IsTouch);
        
        // TODO осталось сделать, чтобы уже приехавшие кирпичики тоже передвигались в самое начало
    }
}
