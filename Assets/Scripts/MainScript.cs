using System;
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
        int countBrickWidth = 7;
        float size = (float) Screen.width / Screen.height * 15f / countBrickWidth;
        Debug.Log("Установлен размер кирпичика: " + size);
        
        // Инициализация поля
        InitializeBrick(0, 0, BrickType.Red, 0, size);
        InitializeBrick(1, 0, BrickType.Green, 0, size);
        InitializeBrick(2, 0, BrickType.Yellow, 0, size);
        InitializeBrick(3, 0, BrickType.Black, 0, size);
        InitializeBrick(-1, 0, BrickType.Blue, 0, size);
        InitializeBrick(-2, 0, BrickType.Red, 0, size);
        InitializeBrick(-3, 0, BrickType.Yellow, 0, size);

        InitializeBrick(0, 1, BrickType.Green, 0, size);
        InitializeBrick(1, 1, BrickType.White, 0, size);
        InitializeBrick(2, 1, BrickType.Red, 0, size);
        InitializeBrick(3, 1, BrickType.Blue, 0, size);
        InitializeBrick(-1, 1, BrickType.White, 0, size);
        InitializeBrick(-2, 1, BrickType.Yellow, 0, size);
        InitializeBrick(-3, 1, BrickType.Red, 0, size);

        InitializeBrick(0, -1, BrickType.Yellow, 0, size);
        InitializeBrick(1, -1, BrickType.Red, 0, size);
        InitializeBrick(2, -1, BrickType.Blue, 0, size);
        InitializeBrick(3, -1, BrickType.Green, 0, size);
        InitializeBrick(-1, -1, BrickType.Red, 0, size);
        InitializeBrick(-2, -1, BrickType.Blue, 0, size);
        InitializeBrick(-3, -1, BrickType.Black, 0, size);

        InitializeBrick(0, 0, BrickType.Blue, 1, size);
        InitializeBrick(1, 0, BrickType.White, 1, size);
        InitializeBrick(2, 0, BrickType.Yellow, 1, size);
        InitializeBrick(-1, 0, BrickType.Red, 1, size);
        InitializeBrick(-2, 0, BrickType.Black, 1, size);
        InitializeBrick(-3, 0, BrickType.Green, 1, size);

        InitializeBrick(0, -1, BrickType.Yellow, 1, size);
        InitializeBrick(1, -1, BrickType.Black, 1, size);
        InitializeBrick(2, -1, BrickType.Blue, 1, size);
        InitializeBrick(-1, -1, BrickType.Yellow, 1, size);
        InitializeBrick(-2, -1, BrickType.Green, 1, size);
        InitializeBrick(-3, -1, BrickType.Red, 1, size);

        BrickUtils.UpdateBricksState();
    }
    
    private void Update()
    {
        CheckFinishBricks();
    }

    /**
     * Инициализация кирпичика
     */
    private void InitializeBrick(int x, int y, BrickType type, int layer, float size)
    {
        float sizeAdd = size / 2;
        float layerAdd = layer % 2 == 0 ? 0 : sizeAdd / 2;
        float xPos = x * sizeAdd + layerAdd;
        float yPos = y * sizeAdd + layerAdd;
        Vector3 vector3 = new Vector3(xPos, yPos, 0);
        GameObject brickGameObject = Instantiate(brickPrefab, vector3, Quaternion.identity);
        Brick brick = new Brick(brickGameObject, type, layer, size);
        brickGameObject.transform.localScale = new Vector3(brick.Size, brick.Size, 1);
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
