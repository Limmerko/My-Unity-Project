using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Statics
{
    public static int CurrentWaypoint = 0; // Текущый свободный waypoint.

    public static List<Brick> Bricks = new List<Brick>(); // Все кирпичики на сцене
    
    /**
     * Список кирпичиков, которые закончили движение в правильном порядке
     */
    public static List<Brick> FinishList()
    {
        return Bricks.Where(brick => brick.IsFinish).OrderBy(brick => brick.TargetWaypoint).ToList();;
    }
    
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
