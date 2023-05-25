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

    private float _brickSize; 
        
    private void Start()
    {
        int countBrickWidth = 7;
        _brickSize = BrickUtils.BrickSize(countBrickWidth);

        InitializedBricks();
        BrickUtils.UpdateBricksState();
    }
    
    private void Update()
    {
        CheckFinishBricks();
    }

    private void InitializedBricks()
    {
        List<InitialBrick> bricks = new List<InitialBrick>();
        
        bricks.Add(new InitialBrick(0, 0, 0));
        bricks.Add(new InitialBrick(1, 0, 0));
        bricks.Add(new InitialBrick(2, 0, 0));
        bricks.Add(new InitialBrick(3, 0,0));
        bricks.Add(new InitialBrick(-1, 0, 0));
        bricks.Add(new InitialBrick(-2, 0, 0));
        bricks.Add(new InitialBrick(-3, 0, 0));

        bricks.Add(new InitialBrick(0, 1, 0));
        bricks.Add(new InitialBrick(1, 1, 0));
        bricks.Add(new InitialBrick(2, 1, 0));
        bricks.Add(new InitialBrick(3, 1, 0));
        bricks.Add(new InitialBrick(-1, 1, 0));
        bricks.Add(new InitialBrick(-2, 1, 0));
        bricks.Add(new InitialBrick(-3, 1, 0));

        bricks.Add(new InitialBrick(0, -1, 0));
        bricks.Add(new InitialBrick(1, -1, 0));
        bricks.Add(new InitialBrick(2, -1, 0));
        bricks.Add(new InitialBrick(3, -1, 0));
        bricks.Add(new InitialBrick(-1, -1, 0));
        bricks.Add(new InitialBrick(-2, -1, 0));
        bricks.Add(new InitialBrick(-3, -1, 0));

        bricks.Add(new InitialBrick(0, 0, 1));
        bricks.Add(new InitialBrick(1, 0, 1));
        bricks.Add(new InitialBrick(2, 0, 1));
        bricks.Add(new InitialBrick(-1, 0, 1));
        bricks.Add(new InitialBrick(-2, 0, 1));
        bricks.Add(new InitialBrick(-3, 0, 1));

        bricks.Add(new InitialBrick(0, -1, 1));
        bricks.Add(new InitialBrick(1, -1, 1));
        bricks.Add(new InitialBrick(2, -1, 1));
        bricks.Add(new InitialBrick(-1, -1, 1));
        bricks.Add(new InitialBrick(-2, -1, 1));
        bricks.Add(new InitialBrick(-3, -1, 1));
        
        MainUtils.MixList(bricks);

        List<BrickType> types = new List<BrickType>();

        System.Random random = new System.Random();
        
        for (int i = 2; i < bricks.Count; i += 3)
        {
            if (types.Count == 0)
            {
                types = Enum.GetValues(typeof(BrickType)).Cast<BrickType>().ToList();
            }
            BrickType type = types[random.Next(types.Count)];
            types.Remove(type);
            
            InitializeBrick(bricks[i - 2], type);
            InitializeBrick(bricks[i - 1], type);
            InitializeBrick(bricks[i], type);
        }
    }

    /**
     * Инициализация кирпичика
     */
    private void InitializeBrick(InitialBrick initialBrick, BrickType type)
    {
        float sizeAdd = _brickSize / 2;
        float layerAdd = initialBrick.Layer % 2 == 0 ? 0 : sizeAdd / 2;
        float xPos = initialBrick.X * sizeAdd + layerAdd;
        float yPos = initialBrick.Y * sizeAdd + layerAdd;
        Vector3 vector3 = new Vector3(xPos, yPos, 0);
        GameObject brickGameObject = Instantiate(brickPrefab, vector3, Quaternion.identity);
        Brick brick = new Brick(brickGameObject, type, initialBrick.Layer, _brickSize);
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
        float timeAnim = 0f;
        bricks.ForEach(brick =>
        {
            BrickScript brickScript = brick.GameObject.GetComponent<BrickScript>();
            timeAnim = brickScript.LengthClearAnim();
            brickScript.PlayClearAnim();
        });
        
        yield return new WaitForSeconds(timeAnim);
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
