using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Canvas canvas;
    
    private void Start()
    {
        // Инициализация поля
        InitializeBrick(new Vector3(0, 0, 0), BrickType.Red);
        InitializeBrick(new Vector3(0.5f, 0, 0), BrickType.Green);
        InitializeBrick(new Vector3(1, 0, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(1.5f, 0, 0), BrickType.Red);
        InitializeBrick(new Vector3(-0.5f, 0, 0), BrickType.Green);
        InitializeBrick(new Vector3(-1, 0, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(-1.5f, 0, 0), BrickType.Red);
        InitializeBrick(new Vector3(-1.5f, 0.5f, 0), BrickType.White);
        InitializeBrick(new Vector3(-1f, 0.5f, 0), BrickType.Green);
        InitializeBrick(new Vector3(-0.5f, 0.5f, 0), BrickType.Black);
        InitializeBrick(new Vector3(0, 0.5f, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(0.5f, 0.5f, 0), BrickType.Green);
        InitializeBrick(new Vector3(1, 0.5f, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(1.5f, 0.5f, 0), BrickType.Blue);
        InitializeBrick(new Vector3(-1.5f, -0.5f, 0), BrickType.Black);
        InitializeBrick(new Vector3(-1, -0.5f, 0), BrickType.Red);
        InitializeBrick(new Vector3(-0.5f, -0.5f, 0), BrickType.White);
        InitializeBrick(new Vector3(0, -0.5f, 0), BrickType.Red);
        InitializeBrick(new Vector3(0.5f, -0.5f, 0), BrickType.White);
        InitializeBrick(new Vector3(1, -0.5f, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(1.5f, -0.5f, 0), BrickType.Blue);
        InitializeBrick(new Vector3(-1.5f, -1, 0), BrickType.Blue);
        InitializeBrick(new Vector3(-1, -1, 0), BrickType.Red);
        InitializeBrick(new Vector3(-0.5f, -1, 0), BrickType.White);
        InitializeBrick(new Vector3(0, -1, 0), BrickType.Blue);
        InitializeBrick(new Vector3(0.5f, -1, 0), BrickType.Red);
        InitializeBrick(new Vector3(1, -1, 0), BrickType.Yellow);
        InitializeBrick(new Vector3(1.5f, -1, 0), BrickType.White);
    }
    
    private void Update()
    {
        CheckFinishBricks();
    }

    /**
     * Инициализация кирпичика
     */
    private void InitializeBrick(Vector3 vector3, BrickType type)
    {
        GameObject brickGameObject = Instantiate(brickPrefab, vector3, Quaternion.identity, canvas.transform);
        Brick brick = new Brick(brickGameObject, type);
        brickGameObject.GetComponent<BrickScript>().SetBrick(brick);
    }
    
    /**
     * Проверка очистки кирпичиков
     */
    private void CheckFinishBricks()
    {
        List<Brick> allFinishBricks = BrickUtils.AllFinishBricks();
        
        if (allFinishBricks.Count >= 3)
        {
            List<List<Brick>> finishBricksByType = allFinishBricks
                .GroupBy(brick => brick.Type)
                .Where(bricks => bricks.Count() == 3)
                .Select(group => group.ToList())
                .ToList();

            if (finishBricksByType.Count > 0)
            {
                finishBricksByType.ForEach(typeBricks => { StartCoroutine(DestroyBricks(typeBricks)); });
            }
            else
            {
                if (allFinishBricks.Count == 7)
                {
                    GameOver();
                }
            }
        }
    }

    /**
     * Удаление кирпичиков
     */
    private IEnumerator DestroyBricks(List<Brick> bricks)
    {
        yield return new WaitForSeconds(0.1f);
        bricks.ForEach(brick =>
        {
            Destroy(brick.GameObject);
            Statics.AllBricks.Remove(brick);
        });
        BrickUtils.UpdateBricksPosition();
    }

    /**
     *  Конец игры
     */
    private void GameOver()
    {
        Debug.Log("Конец игры");
        Statics.AllBricks = new List<Brick>();
        StartCoroutine(RestartLevel());
        Statics.IsGameOver = true;
    }
    
    /**
     * Перезапуск сцены
     */
    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Statics.IsGameOver = false;
    }
}
