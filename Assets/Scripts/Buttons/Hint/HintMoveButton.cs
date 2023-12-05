using System.Collections.Generic;
using System.Linq;
using Classes;
using Enums;
using UnityEngine;
using Utils;

namespace Buttons.Hint
{
    /**
    * Кнопка "Подсказка хода" в подсказках
    */
    public class HintMoveButton : CommonHintButton
    {
        protected override void StartProcess()
        {
            PrefCount = HintCountType.CountHintMove.ToString();
            base.StartProcess();
        }
    
        protected override void Process()
        {
            if (BrickUtils.IsSwipingNow() || 
                !Statics.LevelStart || 
                BrickUtils.AllTouchAndNotFinishBricks().Count > 0 ||
                PlayerPrefs.GetInt(PrefCount) == 0) return;
            
            Debug.Log("Подсказка");
            
            List<Brick> finishBricks = BrickUtils.AllFinishBricks();
            List<List<Brick>> needBricks = new() { BrickUtils.AllClickableBricks(), 
                BrickUtils.AllNotTouchBricks().OrderByDescending(brick => brick.Layer).ToList() }; 

            List<List<Brick>> finishBricksByType = finishBricks
                .GroupBy(brick => brick.Type)
                .Select(group => group.ToList())
                .ToList();
            
            foreach (var availableBricks in needBricks)
            {
                // Когда есть финишированные плитки
                if (finishBricksByType.Count != 0)
                {
                    if (finishBricks.Count <= 6)
                    {
                        // Когда есть 2 плитки одного типа в финише и есть 1 или больше доступных 
                        List<List<Brick>> twoFinish = finishBricksByType.Where(bricks => bricks.Count() == 2).ToList();
                        if (twoFinish.Count > 0)
                        {
                            foreach (var bricksInFinish in twoFinish)
                            {
                                Brick brick = availableBricks
                                    .Find(brick => brick.Type.Equals(bricksInFinish[0].Type));
                                if (brick != null)
                                {
                                    MoveBrick(brick);
                                    goto Exit;
                                }
                            }
                        }
                    }

                    if (finishBricks.Count <= 5)
                    {
                        // Когда есть 1 плитка одного типа в финише и есть 2 или больше доступных 
                        List<List<Brick>> oneFinish = finishBricksByType.Where(bricks => bricks.Count() == 1).ToList();
                        if (oneFinish.Count > 0)
                        {
                            foreach (var brickInFinish in oneFinish)
                            {
                                List<Brick> bricks = availableBricks
                                    .Where(brick => brick.Type.Equals(brickInFinish[0].Type))
                                    .ToList();
                                if (bricks.Count >= 2)
                                {
                                    foreach (var brick in bricks.Take(2))
                                    {
                                        MoveBrick(brick);
                                    }

                                    goto Exit;
                                }
                            }
                        }
                    }
                }

                if (finishBricks.Count <= 4)
                {
                    // Когда для финишированных плиток нет доступных ищется 3 любого типа среди доступных
                    List<Brick> clickableBricksByType = availableBricks
                        .GroupBy(brick => brick.Type)
                        .Select(group => group.ToList())
                        .ToList()
                        .Find(bricks => bricks.Count >= 3);

                    if (clickableBricksByType != null && clickableBricksByType.Count > 0)
                    {
                        foreach (var brick in clickableBricksByType.Take(3))
                        {
                            MoveBrick(brick);
                        }

                        goto Exit;
                    }
                }
            }

            Debug.Log("Нет доступных ходов"); // TODO может что-то выводить если нет доступных ходов?
            return; 
            
            Exit:
                CheckCount();
                return;
        }

        private void MoveBrick(Brick brick)
        {
            // Изменение положения
            brick.TargetWaypoint = BrickUtils.FindCurrentWaypoint(brick.Type);
            brick.IsTouch = true;
            brick.IsDown = false;
            brick.Layer = 100;
            BrickUtils.UpdateBricksPosition();

            // Изменение состояния
            BrickUtils.ChangeClickable(brick, true);
            BrickUtils.UpdateBricksState();
            
        }
    }
}
