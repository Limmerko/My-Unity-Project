using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Statics
{
    public static List<Brick> AllBricks = new List<Brick>(); // Все кирпичики на сцене
    
    /**
     * Список кирпичиков, которые закончили движение в правильном порядке
     */
    public static List<Brick> FinishBricks()
    {
        return AllBricks.Where(brick => brick.IsFinish).OrderBy(brick => brick.TargetWaypoint).ToList();;
    }
    
    /**
     * Список кирпичиков, которые закончили движение в правильном порядке определенного типа
     */
    public static List<Brick> FinishBricks(BrickType type)
    {
        return AllBricks.Where(brick => brick.IsFinish && brick.Type.Equals(type)).OrderBy(brick => brick.TargetWaypoint).ToList();;
    }
    
    public static List<Brick> TouchBricks()
    {
        return AllBricks.Where(brick => brick.IsTouch).OrderBy(brick => brick.TargetWaypoint).ToList();;
    }

    /**
     * Определение правильной цели для движение кирпичика 
     */
    public static int FindCurrentWaypoint(BrickType type)
    {
        int result = 0;
        List<Brick> touchBricks = TouchBricks();
        // Если уже есть нажатый кирпичик такого типа
        if (touchBricks.Count(brick => brick.Type.Equals(type)) > 0)
        {
            result = touchBricks.Last(brick => type.Equals(brick.Type)).TargetWaypoint + 1;
            // Движение остальных кирпичиков вправо
            touchBricks.Where(brick => brick.TargetWaypoint >= result).ToList().ForEach(brick => brick.TargetWaypoint += 1);
        }
        else
        {
            result = touchBricks.Count;
        }

        return result;
    }
    
    /**
     * Актуализация положения кирпичиков
     */
    public static void UpdateBricksPosition()
    {
        List<Brick> finishBricks = TouchBricks();
        
        for (int i = 0; i < finishBricks.Count; i++)
        {
            finishBricks.ElementAt(i).TargetWaypoint = i;
        }
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
