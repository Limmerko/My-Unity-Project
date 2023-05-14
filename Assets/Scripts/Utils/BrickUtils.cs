using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BrickUtils
{
    /**
     * Список кирпичиков, которые закончили движение в правильном порядке
     */
    public static List<Brick> AllFinishBricks()
    {
        return Statics.AllBricks.Where(brick => brick.IsFinish)
            .OrderBy(brick => brick.TargetWaypoint)
            .ToList();;
    }
    
    public static List<Brick> TouchBricks()
    {
        return  Statics.AllBricks.Where(brick => brick.IsTouch)
            .OrderBy(brick => brick.TargetWaypoint)
            .ToList();
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
}
