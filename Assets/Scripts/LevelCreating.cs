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
        
        foreach (var brick in bricks)
        {
            float layerAdd = brick.GetComponent<SpriteRenderer>().sortingOrder % 2 == 0 ? 0 : 0.5f;
            float xPos = brick.transform.position.x - layerAdd;
            float yPos = brick.transform.position.y - layerAdd;
            
            result += "new InitialBrick(" + xPos + ", " + (yPos + 1) + ", " + brick.GetComponent<SpriteRenderer>().sortingOrder + "),\n";
        }
        
        Debug.Log(result);
    }
}
