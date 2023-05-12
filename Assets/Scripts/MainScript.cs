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
        // Инициализация поля
        InitializeBrick(new Vector3(-1.5f, 0, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(-1, 0, 0), BrickType.Red);
        InitializeBrick(new Vector3(-0.5f, 0, 0), BrickType.Blue);
        InitializeBrick(new Vector3(0, 0, 0), BrickType.Red);
        InitializeBrick(new Vector3(0.5f, 0, 0), BrickType.Blue);
        InitializeBrick(new Vector3(1, 0, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(1.5f, 0, 0), BrickType.Red);
    }
    
    private void Update()
    {
        // ClearBrick();
    }

    /**
     * Инициализация кирпичика
     */
    private void InitializeBrick(Vector3 vector3, BrickType type)
    {
        GameObject brickGameObject = Instantiate(brickPrefab, vector3, Quaternion.identity);
        Brick brick = new Brick(brickGameObject, type);
        brickGameObject.GetComponent<BrickScript>().SetBrick(brick);
    }
    
    /**
     * Очистка кирпичиков
     */
    private void ClearBrick()
    {
        // TODO сделать удаление по типам
        List<Brick> finishBricks = Statics.FinishBricks();

        if (finishBricks.Count == 3)
        {
            finishBricks.ForEach(brick =>
            {
                Destroy(brick.GameObject);
                Statics.AllBricks.Remove(brick);
            });
        }
    }
}
