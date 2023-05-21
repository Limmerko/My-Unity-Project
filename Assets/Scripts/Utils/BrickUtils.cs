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
    
    public static List<Brick> AllTouchBricks()
    {
        return  Statics.AllBricks.Where(brick => brick.IsTouch)
            .OrderBy(brick => brick.TargetWaypoint)
            .ToList();
    }
    
    public static List<Brick> AllNotTouchBricks()
    {
        return  Statics.AllBricks.Where(brick => !brick.IsTouch)
            .ToList();
    }

    /**
     * Определение правильной цели для движение кирпичика 
     */
    public static int FindCurrentWaypoint(BrickType type)
    {
        int result = 0;
        List<Brick> touchBricks = AllTouchBricks();
        foreach (var touchBrick in touchBricks)
        {
            Debug.Log(touchBrick.TargetWaypoint + " " + touchBrick.Type);
        }
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
        Debug.Log(result);
        return result;
    }
    
    /**
     * Актуализация положения кирпичиков
     */
    public static void UpdateBricksPosition()
    {
        List<Brick> finishBricks = AllTouchBricks();
        
        for (int i = 0; i < finishBricks.Count; i++)
        {
            finishBricks.ElementAt(i).TargetWaypoint = i;
        }
    }

    /**
     * Актуализация состояника кирпичика
     */
    public static void UpdateBricksState()
    {
        List<Brick> bricks = AllNotTouchBricks();
        
        bricks.ForEach(brick =>
        {
            brick.GameObject.GetComponent<SpriteRenderer>().sortingOrder = brick.Layer;
        });

        bricks = bricks.OrderBy(brick => brick.Layer).ToList();

        bricks.ForEach(brick =>
        {
            bool isClickable = true;
            Vector3 brickPos = brick.GameObject.transform.position;
            foreach (var it in bricks.Where(it => it.Layer > brick.Layer))
            {
                Vector3 itPos = it.GameObject.transform.position;
                if ((itPos.x - brickPos.x == 0.25f || itPos.x - brickPos.x == -0.25f) &&
                    (itPos.y - brickPos.y == 0.25f || itPos.y - brickPos.y == -0.25f))
                {
                    isClickable = false;
                    break;
                }
            }

            if (isClickable)
            {
                brick.GameObject.GetComponent<SpriteRenderer>().color = Color.white;
                brick.IsClickable = true;
            }
            else
            {
                brick.GameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                brick.IsClickable = false;
            }
        });
    }
}
