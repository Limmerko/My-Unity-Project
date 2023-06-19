using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreating : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    void Start()
    {
        CreateLevel();
        // InitializeField();
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
        List<InitialBrick> bricks = Levels.Level7;
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
}
