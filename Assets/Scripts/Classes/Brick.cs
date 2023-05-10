using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick
{
    public GameObject GameObject { get; set; }  // Сам объект

    public bool IsTouch { get; set; } // Было нажатие на этот кирпич

    public bool IsFinish { get; set; } // Закончил движение
    
    public int TargetWaypoint { get; set; } // Точка на которой находится кирпич

    public Brick(GameObject gameObject)
    {
        GameObject = gameObject;
        IsTouch = false;
        IsFinish = false;
    }
}
