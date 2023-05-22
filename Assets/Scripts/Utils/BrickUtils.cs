using System;
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
            float brickRadius = brick.Size / 2;
            bool isClickable = true;
            Vector3 brickPos = brick.GameObject.transform.position;
            foreach (var it in bricks.Where(it => it.Layer > brick.Layer))
            {
                Vector3 itPos = it.GameObject.transform.position;
                if (Math.Abs(itPos.x - brickPos.x) - brickRadius < 0.01f &&
                    Math.Abs(itPos.y - brickPos.y) - brickRadius < 0.01f)
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

    public static float BrickSize(int countBrickWidth)
    {
        return (float) Screen.width / Screen.height * 15f / countBrickWidth;
    } 
}
