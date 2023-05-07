using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick
{
    public BoxCollider2D Collider { get; set; }  // Коллайдер для опредения области

    public bool IsTouch { get; set; } // Было нажатие на этот кирпич

    public bool IsFinish { get; set; } // Закончил движение

    public Brick(BoxCollider2D collider)
    {
        Collider = collider;
        IsTouch = false;
        IsFinish = false;
    }
}
