using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
using System.Linq;
using System.Runtime.InteropServices;

public class MainScript : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab; // Плитка
    [SerializeField] private GameObject finishPlacePrefab; // Верхняя и нижняя часть места для приземления 
    [SerializeField] private GameObject waypointPrefab; // Одна из точек приземления

    private float _brickSize; 
    private float _sizeFinishBrick; 
    private Random _random = new Random();
        
    private void Start()
    {
        Vibration.Init();
        
        List<InitialBrick> level = Statics.AllLevels[_random.Next(Statics.AllLevels.Count)];
        float maxX = level.Aggregate((max, next) => next.X > max.X ? next : max).X;
        float minX = level.Aggregate((min, next) => next.X < min.X ? next : min).X;
        int countBrickWidth = (int) (maxX - minX) + 1;
        Debug.Log("Ширина: " + countBrickWidth);
        
        _brickSize = BrickUtils.BrickSize(countBrickWidth);
        _sizeFinishBrick = BrickUtils.BrickSize(8);
        
        InitializeFinishPlace();
        InitializedBricks(level);
        
        BrickUtils.UpdateBricksState();
    }
    
    private void Update()
    {
        CheckFinishBricks();
    }

    private void InitializedBricks(List<InitialBrick> level)
    {
        List<InitialBrick> bricks = level;

        MainUtils.MixList(bricks); // Перемешивание плиток

        List<BrickType> types = new List<BrickType>();
        
        if (bricks.Count % 3 != 0)
        {
            throw new ArgumentException("ОШИБКА!!! Кол-во плиток в уровне не кратно 3. " + bricks.Count);
        }
        
        for (int i = 2; i < bricks.Count; i += 3) // Случайное выставление типов
        {
            if (types.Count == 0)
            {
                types = Enum.GetValues(typeof(BrickType)).Cast<BrickType>().ToList();
            }
            BrickType type = types[_random.Next(types.Count)];
            types.Remove(type);

            bricks[i - 2].Type = type;
            bricks[i - 1].Type = type;
            bricks[i].Type = type;
        }
        
        // Сортировка по слоям и расположению
        bricks = bricks.OrderBy(b => b.Layer)
            .ThenByDescending(b => b.Y)
            .ThenBy(b => b.X)
            .ToList();

        for (int i = 0; i < bricks.Count; i++)
        {
            InitializeBrick(bricks[i], i / -10000f);
        }
    }

    /**
     * Инициализация кирпичика
     */
    private void InitializeBrick(InitialBrick initialBrick, float z)
    {
        float sizeAdd = _brickSize / 2;
        float xPos = initialBrick.X * sizeAdd;
        float yPos = initialBrick.Y * sizeAdd;
        Vector3 vector3 = new Vector3(xPos, yPos, z); // Z нужен для корректного отображаения спрайтов, иначе они будут накладываться друг на друга
        GameObject brickGameObject = Instantiate(brickPrefab, vector3, Quaternion.identity);
        Brick brick = new Brick(brickGameObject, initialBrick.Type, initialBrick.Layer, _brickSize);
        brickGameObject.transform.localScale = new Vector3(brick.Size, brick.Size, 1);
        brickGameObject.GetComponent<BrickScript>().SetBrick(brick, _sizeFinishBrick);
    }

    /**
     * Инициализация места для приземления
     */
    private void InitializeFinishPlace()
    {
        finishPlacePrefab.transform.localScale = new Vector3(_sizeFinishBrick, _sizeFinishBrick, 1);
        float y = waypointPrefab.transform.position.y + _sizeFinishBrick / 18f;
        finishPlacePrefab.transform.position = new Vector3(0, y, 0);
        Instantiate(finishPlacePrefab, finishPlacePrefab.transform.position, Quaternion.identity);
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
                .Where(bricks => bricks.Count() >= 3)
                .Select(group => group.ToList())
                .ToList();

            if (finishBricksByType.Count > 0)
            {
                finishBricksByType.ForEach(typeBricks => { StartCoroutine(DestroyBricks(typeBricks.Take(3).ToList())); });
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
        bricks.ForEach(brick => brick.IsToDestroy = true);
        yield return new WaitForSeconds(0.1f);
        float timeAnim = 0f;
        bricks.ForEach(brick =>
        {
            if (!brick.GameObject.IsDestroyed())
            {
                BrickScript brickScript = brick.GameObject.GetComponent<BrickScript>();
                if (timeAnim == 0f)
                {
                    timeAnim = brickScript.LengthClearAnim();
                }
                brickScript.PlayClearAnim();
            }
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
