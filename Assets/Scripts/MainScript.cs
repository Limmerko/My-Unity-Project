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
        float size = (float) Screen.width / Screen.height * 1.6f;
        Debug.Log("Утановлен размер кирпичика: " + size);

        // TODO сделать ещё чтобы слой от размера зависел т.к. там расположение тоже меняется
        // Инициализация поля
        InitializeBrick(0, 0, BrickType.Red, 0, size);
        InitializeBrick(1, 0, BrickType.Green, 0, size);
        InitializeBrick(2, 0, BrickType.Yellow, 0, size);
        InitializeBrick(3, 0, BrickType.Black, 0, size);
        InitializeBrick(4, 0, BrickType.Red, 0, size);
        InitializeBrick(5, 0, BrickType.Green, 0, size);
        InitializeBrick(-1, 0, BrickType.Blue, 0, size);
        InitializeBrick(-2, 0, BrickType.Red, 0, size);
        InitializeBrick(-3, 0, BrickType.Yellow, 0, size);
        InitializeBrick(-4, 0, BrickType.White, 0, size);
        InitializeBrick(-5, 0, BrickType.Yellow, 0, size);
        /*InitializeBrick(-1, 0, BrickType.Green, 0, size);
        InitializeBrick(-2, 0, BrickType.Yellow, 0, size);
        InitializeBrick(-3, 0, BrickType.White, 0, size);
        InitializeBrick(0, 1, BrickType.White, 0, size);
        InitializeBrick(1, 1, BrickType.Yellow, 0, size);
        InitializeBrick(2, 1, BrickType.Blue, 0, size);
        InitializeBrick(3, 1, BrickType.Blue, 0, size);
        InitializeBrick(-1, 1, BrickType.Green, 0, size);
        InitializeBrick(-2, 1, BrickType.White, 0, size);
        InitializeBrick(-3, 1, BrickType.Green, 0, size);
        InitializeBrick(-1, -1, BrickType.Blue, 0, size);
        InitializeBrick(-2, -1, BrickType.Green, 0, size);
        InitializeBrick(-3, -1, BrickType.Black, 0, size);
        InitializeBrick(0, -1, BrickType.White, 0, size);
        InitializeBrick(1, -1, BrickType.Yellow, 0, size);
        InitializeBrick(2, -1, BrickType.Blue, 0, size);
        InitializeBrick(3, -1, BrickType.Black, 0, size);
        InitializeBrick(new Vector3(0, 0.5f, 0), BrickType.Yellow, 0, size);
       InitializeBrick(new Vector3(0.5f, 0.5f, 0), BrickType.Green, 0, size);
       InitializeBrick(new Vector3(1, 0.5f, 0), BrickType.Yellow, 0, size);
      InitializeBrick(new Vector3(1.5f, 0.5f, 0), BrickType.Blue, 0, size);
       InitializeBrick(new Vector3(-1.5f, -0.5f, 0), BrickType.Black, 0, size);
       InitializeBrick(new Vector3(-1, -0.5f, 0), BrickType.Yellow, 0, size);
       InitializeBrick(new Vector3(-0.5f, -0.5f, 0), BrickType.White, 0, size);
       InitializeBrick(new Vector3(0, -0.5f, 0), BrickType.Red, 0, size);
       InitializeBrick(new Vector3(0.5f, -0.5f, 0), BrickType.White, 0, size);
       InitializeBrick(new Vector3(1, -0.5f, 0), BrickType.Yellow, 0, size);
       InitializeBrick(new Vector3(1.5f, -0.5f, 0), BrickType.Blue, 0, size);
       InitializeBrick(new Vector3(-1.5f, -1, 0), BrickType.Blue, 0, size);
       InitializeBrick(new Vector3(-1, -1, 0), BrickType.Red, 0, size);
       InitializeBrick(new Vector3(-0.5f, -1, 0), BrickType.White, 0, size);
       InitializeBrick(new Vector3(0, -1, 0), BrickType.Blue, 0, size);
       InitializeBrick(new Vector3(0.5f, -1, 0), BrickType.Red, 0, size);
       InitializeBrick(new Vector3(1, -1, 0), BrickType.Yellow, 0, size);
       InitializeBrick(new Vector3(1.5f, -1, 0), BrickType.White, 0, size);
       
       InitializeBrick(new Vector3(-1.25f, -0.75f, 0), BrickType.Black, 1, size);
       InitializeBrick(new Vector3(-0.75f, -0.75f, 0), BrickType.Yellow, 1, size);
       InitializeBrick(new Vector3(-0.25f, -0.75f, 0), BrickType.Blue, 1, size);
       InitializeBrick(new Vector3(0.25f, -0.75f, 0), BrickType.Green, 1, size);
       InitializeBrick(new Vector3(0.75f, -0.75f, 0), BrickType.White, 1, size);
       InitializeBrick(new Vector3(1.25f, -0.75f, 0), BrickType.Black, 1, size);
       
       InitializeBrick(new Vector3(-1.25f, 0.25f, 0), BrickType.White, 1, size);
       InitializeBrick(new Vector3(-0.75f, 0.25f, 0), BrickType.Red, 1, size);
       InitializeBrick(new Vector3(-0.25f, 0.25f, 0), BrickType.Green, 1, size);
       InitializeBrick(new Vector3(0.25f, 0.25f, 0), BrickType.Yellow, 1, size);
       InitializeBrick(new Vector3(0.75f, 0.25f, 0), BrickType.Blue, 1, size);
       InitializeBrick(new Vector3(1.25f, 0.25f, 0), BrickType.White, 1, size);*/
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
        Vector3 vector3 = new Vector3((float) Math.Round(x * (size / 2), 2), (float) Math.Round(y * (size / 2), 2));
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
