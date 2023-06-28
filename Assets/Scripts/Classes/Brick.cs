using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick
{
    public GameObject GameObject { get; }  // Сам объект

    public bool IsTouch { get; set; } // Было нажатие на этот кирпич

    public bool IsFinish { get; set; } // Закончил движение
    
    public bool IsDown { get; set; } // Состояние нажатия на этот кирпич
    
    public bool IsClickable { get; set; } // Доступен для нажатия
    
    public int TargetWaypoint { get; set; } // Точка на которой находится кирпич
    
    public BrickType Type { get; } // Тип кирпичика
    
    public int Layer { get; set; } // Слой для отображения слайдов
    
    public float Size { get; set; } // Размер
    
    public bool IsToDestroy { get; set; } // Помечен ли на уничтожение 
    
    public Vector3 TargetPosition { get; set; } // 

    public Brick(GameObject gameObject, BrickType type, int layer, float size, Vector3 targetPosition)
    {
        GameObject = gameObject;
        Type = type;
        IsTouch = false;
        IsFinish = false;
        IsClickable = false;
        Layer = layer;
        Size = size;
        IsToDestroy = false;
        TargetPosition = targetPosition;
    }
}
