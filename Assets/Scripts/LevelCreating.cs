using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelCreating : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    void Start()
    {
        // CreateLevel();
        // InitializeField();
        // CheckAllLevelsForDouble();
        SortLevelsByComplexity();
    }

    /**
     * Выводит в консоль созданный уровень.
     * Весь уровень берет со сцены.
     */
    private void CreateLevel()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("BrickEmpty");
        String result = null;
        
        if (bricks.Length % 3 != 0)
        {
            throw new ArgumentException("ОШИБКА!!! Кол-во плиток в уровне не кратно 3. " + bricks.Length);
        }
        
        foreach (var brick in bricks)
        {
            float x = brick.transform.position.x;
            float y = brick.transform.position.y + 1; // +1 чтобы сам уровень был чуть выше центра
            string xPos = x.ToString().Replace(",", ".") + "f";
            string yPos = y.ToString().Replace(",", ".") + "f";
            
            result += "new InitialBrick(" + xPos + ", " +  yPos + ", " + brick.GetComponent<SpriteRenderer>().sortingOrder + "),\n";
        }
        
        Debug.Log(result);
    }

    /**
     * Инициализирует уровень на сцене.
     */
    private void InitializeField()
    {
        List<InitialBrick> bricks = Levels.Level23;
        String result = null;
        
        foreach (var brick in bricks)
        {
            string xPos = brick.X.ToString().Replace(",", ".") + "f";
            string yPos = brick.Y.ToString().Replace(",", ".") + "f";
            
            result += "new InitialBrick(" + xPos + ", " + yPos + ", " + brick.Layer + "),\n";
            Vector3 vector3 = new Vector3(brick.X, brick.Y, 0);
            GameObject brickGameObject = Instantiate(prefab, vector3, Quaternion.identity);
            
            brickGameObject.GetComponent<SpriteRenderer>().sortingOrder = brick.Layer;
        }
        
        Debug.Log(result);
    }

    /**
     * Проверка уровня на дубликаты
     */
    private void CheckAllLevelsForDouble()
    {
        int i = 0;
        Statics.AllLevels.ForEach(level =>
        {
            i++;
            List<InitialBrick> bricks = level.Bricks.GroupBy(brick => new
                {
                    brick.X,
                    brick.Y,
                    brick.Layer
                })
                .Where(g => g.Count() > 1)
                .Select(g => new InitialBrick(g.Key.X, g.Key.Y, g.Key.Layer))
                .ToList();
            if (bricks.Count() > 0)
            {
                Debug.Log("В уровне " + i + " есть дубликаты: ");
                Debug.Log(bricks[0].X + " " + bricks[0].Y + " " + bricks[0].Layer);
                
            }
        });
    }

    /**
     * Сортировка уровней по сложности
     */
    private void SortLevelsByComplexity()
    {
        List<Level> levels = Statics.AllLevels;
        foreach (var level in levels)
        {
            int complexity = 0;
            complexity += level.Bricks.Count * level.CountTypes;
            level.Complexity = complexity;
        }
        
        levels = levels.OrderBy(l => l.Complexity).ToList();
        
        String result = null;
        
        foreach (var level in levels)
        {
            result += "new Level(" + "\"" + level.Name + "\", "+ "Levels." + level.Name + ", " + level.CountTypes + ", " + level.Complexity + "),\n";
        }
        Debug.Log(result);
    }
}
