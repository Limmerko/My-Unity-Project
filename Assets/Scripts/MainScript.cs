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
        InitializeBrick(new Vector3(-1.5f, 0.5f, 0), BrickType.Red);
        InitializeBrick(new Vector3(-1, 0.5f, 0), BrickType.Blue);
        InitializeBrick(new Vector3(-0.5f, 0.5f, 0), BrickType.Blue);
        InitializeBrick(new Vector3(0, 0.5f, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(0.5f, 0.5f, 0), BrickType.Blue);
        InitializeBrick(new Vector3(1, 0.5f, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(1.5f, 0.5f, 0), BrickType.Blue);
        InitializeBrick(new Vector3(-1.5f, -0.5f, 0), BrickType.Blue);
        InitializeBrick(new Vector3(-1, -0.5f, 0), BrickType.Red);
        InitializeBrick(new Vector3(-0.5f, -0.5f, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(0, -0.5f, 0), BrickType.Red);
        InitializeBrick(new Vector3(0.5f, -0.5f, 0), BrickType.Red);
        InitializeBrick(new Vector3(1, -0.5f, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(1.5f, -0.5f, 0), BrickType.Blue);
    }
    
    private void Update()
    {
        ClearFinishBricks();
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
    private void ClearFinishBricks()
    {
        List<Brick> allFinishBricks = BrickUtils.AllFinishBricks();

        if (allFinishBricks.Count >= 3)
        {
            List<List<Brick>> finishBricksByType = allFinishBricks
                .GroupBy(brick => brick.Type)
                .Select(group => group.ToList())
                .ToList();

            finishBricksByType.ForEach(typeBricks =>
            {
                if (typeBricks.Count == 3)
                {
                    typeBricks.ForEach(brick =>
                    {
                        DestroyBrick(brick);
                    });
                    BrickUtils.UpdateBricksPosition();
                }
            });
        }
    }

    /**
     * Удаление одного кирпичика
     */
    private void DestroyBrick(Brick brick)
    {
        Destroy(brick.GameObject);
        Statics.AllBricks.Remove(brick);
    }
}
