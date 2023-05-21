using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick
{
    public GameObject GameObject { get; }  // Сам объект

    public bool IsTouch { get; set; } // Было нажатие на этот кирпич

    public bool IsFinish { get; set; } // Закончил движение
    
    public bool IsClickable { get; set; } // Доступен для нажатия
    
    public int TargetWaypoint { get; set; } // Точка на которой находится кирпич
    
    public BrickType Type { get; } // Тип кирпичика
    
    public int Layer { get; set; } // Слой для отображения слайдов
    
    public float Size { get; set; }

    public Brick(GameObject gameObject, BrickType type, int layer, float size)
    {
        GameObject = gameObject;
        Type = type;
        IsTouch = false;
        IsFinish = false;
        IsClickable = true;
        Layer = layer;
        Size = size;
    }
}
