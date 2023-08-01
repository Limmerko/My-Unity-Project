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
        return Statics.AllBricks.Where(brick => brick.IsFinish && !brick.IsToDestroy)
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
            Brick brick = finishBricks[i];
            brick.TargetWaypoint = i;
            brick.GameObject.GetComponent<SpriteRenderer>().sortingOrder = brick.Layer;
            Transform transform = brick.GameObject.transform;
            Vector3 position = transform.position;
            transform.position = new Vector3(position.x, position.y, i / -10000f);
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
            float brickRadius = brick.Size / 2.1f;
            bool isClickable = true;
            Vector3 brickPos = brick.GameObject.transform.position;
           
                
            foreach (var it in bricks.Where(it => it.Layer > brick.Layer))
            {
                Vector3 itPos = it.GameObject.transform.position;
                if (Math.Abs(itPos.x - brickPos.x) - brickRadius < 0 &&
                    Math.Abs(itPos.y - brickPos.y) - brickRadius < 0)
                {
                    isClickable = false;
                    break;
                }
            }

            ChangeClickable(brick, isClickable);
        });
    }

    /**
     * Изменение возможности нажатия на кирпичик
     */
    public static void ChangeClickable(Brick brick, bool isClickable)
    {
        SpriteRenderer sprite = brick.GameObject.GetComponent<SpriteRenderer>();
        if (isClickable)
        {
            sprite.color = Statics.IsClickableColor;
            brick.IsClickable = true;
        }
        else
        {
            sprite.color = Statics.IsNotClickableColor;
            brick.IsClickable = false;
        }
    }

    /**
     * Проверяет происходит ли сейчас перемешивание
     */
    public static bool IsSwipingNow()
    {
        return AllNotTouchBricks().Count(brick => brick.IsSwipe) != 0;
    }

    public static float BrickSize(int countBrickWidth)
    {
        // 18 получено опытным путем. При нем во всех разрешениях и соотношениях остается по бокам ровно по половине плитки
        return (float) Screen.width / Screen.height * 18f / countBrickWidth;
    }
}
