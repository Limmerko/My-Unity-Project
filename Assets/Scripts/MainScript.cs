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
        PositionUpdating();
    }

    /**
     * Очистка кирпичиков
     */
    private void ClearBrick()
    {
        List<Brick> finishBricks = Statics.FinishList();

        if (finishBricks.Count == 3)
        {
            finishBricks.ForEach(brick =>
            {
                Destroy(brick.GameObject);
                Statics.Bricks.Remove(brick);
            });
        }
    }

    /**
     * Актуализация положения кирпичиков
     */
    private void PositionUpdating()
    {
        List<Brick> finishBricks = Statics.FinishList();
        
        for (int i = 0; i < finishBricks.Count; i++)
        {
            finishBricks.ElementAt(i).TargetWaypoint = i;
        }
        
        Statics.CurrentWaypoint = Statics.Bricks.Count(brick => brick.IsTouch);
    }
}
