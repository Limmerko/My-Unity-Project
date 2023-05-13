using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUtils
{
    /**
     * Движение до точки
     * @param точка назначения
     * @param сам объект
     * @param скорость
     */
    public static void MoveToWaypoint(Vector2 to, Collider2D collider, float speed)
    {
        collider.transform.position = Vector2.MoveTowards(
            collider.transform.position,
            to,
            Time.deltaTime * speed);
    }
}
