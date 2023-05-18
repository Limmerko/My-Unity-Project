using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;

    private void Start()
    {
        // Инициализация поля
        InitializeBrick(new Vector3(0, 0, 0), BrickType.Red, 0);
        InitializeBrick(new Vector3(0.5f, 0, 0), BrickType.Green, 0);
        InitializeBrick(new Vector3(1, 0, 0), BrickType.Yellow, 0);
        InitializeBrick(new Vector3(1.5f, 0, 0), BrickType.Red, 0);
        InitializeBrick(new Vector3(-0.5f, 0, 0), BrickType.Green, 0);
        InitializeBrick(new Vector3(-1, 0, 0), BrickType.Yellow, 0);
        InitializeBrick(new Vector3(-1.5f, 0, 0), BrickType.Red, 0);
        InitializeBrick(new Vector3(-1.5f, 0.5f, 0), BrickType.White, 0);
        InitializeBrick(new Vector3(-1f, 0.5f, 0), BrickType.Green, 0);
        InitializeBrick(new Vector3(-0.5f, 0.5f, 0), BrickType.Black, 0);
        InitializeBrick(new Vector3(0, 0.5f, 0), BrickType.Yellow, 0);
        InitializeBrick(new Vector3(0.5f, 0.5f, 0), BrickType.Green, 0);
        InitializeBrick(new Vector3(1, 0.5f, 0), BrickType.Yellow, 0);
        InitializeBrick(new Vector3(1.5f, 0.5f, 0), BrickType.Blue, 0);
        InitializeBrick(new Vector3(-1.5f, -0.5f, 0), BrickType.Black, 0);
        InitializeBrick(new Vector3(-1, -0.5f, 0), BrickType.Red, 0);
        InitializeBrick(new Vector3(-0.5f, -0.5f, 0), BrickType.White, 0);
        InitializeBrick(new Vector3(0, -0.5f, 0), BrickType.Red, 0);
        InitializeBrick(new Vector3(0.5f, -0.5f, 0), BrickType.White, 0);
        InitializeBrick(new Vector3(1, -0.5f, 0), BrickType.Yellow, 0);
        InitializeBrick(new Vector3(1.5f, -0.5f, 0), BrickType.Blue, 0);
        InitializeBrick(new Vector3(-1.5f, -1, 0), BrickType.Blue, 0);
        InitializeBrick(new Vector3(-1, -1, 0), BrickType.Red, 0);
        InitializeBrick(new Vector3(-0.5f, -1, 0), BrickType.White, 0);
        InitializeBrick(new Vector3(0, -1, 0), BrickType.Blue, 0);
        InitializeBrick(new Vector3(0.5f, -1, 0), BrickType.Red, 0);
        InitializeBrick(new Vector3(1, -1, 0), BrickType.Yellow, 0);
        InitializeBrick(new Vector3(1.5f, -1, 0), BrickType.White, 0);
        InitializeBrick(new Vector3(0.25f, -0.75f, 0), BrickType.Green, 1);
        BrickUtils.UpdateBricksState();
    }
    
    private void Update()
    {
        CheckFinishBricks();
    }

    /**
     * Инициализация кирпичика
     */
    private void InitializeBrick(Vector3 vector3, BrickType type, int layer)
    {
        GameObject brickGameObject = Instantiate(brickPrefab, vector3, Quaternion.identity);
        Brick brick = new Brick(brickGameObject, type, layer);
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
        yield return new WaitForSeconds(0.2f);
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
