using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreating : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] bricks =GameObject.FindGameObjectsWithTag("BrickEmpty");
        String result = null;
        
        if (bricks.Length % 3 != 0)
        {
            throw new ArgumentException("ОШИБКА!!! Кол-во плиток в уровне не кратно 3. (ЛОХ) " + bricks.Length);
        }

        
        foreach (var brick in bricks)
        {
            float layerAdd = brick.GetComponent<SpriteRenderer>().sortingOrder % 2 == 0 ? 0 : 0.5f;
            float xPos = brick.transform.position.x - layerAdd;
            float yPos = brick.transform.position.y - layerAdd;
            
            //  (yPos + 1) чтобы сам уровень был чуть выше центра
            result += "new InitialBrick(" + xPos + ", " + (yPos + 1) + ", " + brick.GetComponent<SpriteRenderer>().sortingOrder + "),\n";
        }
        
        Debug.Log(result);
    }
}
