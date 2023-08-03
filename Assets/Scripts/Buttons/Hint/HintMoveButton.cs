using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Buttons.Hint
{
    /**
    * Кнопка "Подсказка хода" в подсказках
    */
    public class HintMoveButton : CommonButton
    {
        protected override void StartProcess()
        {
            // empty
        }
    
        protected override void Process()
        {
            if (BrickUtils.IsSwipingNow() || !Statics.LevelStart) return;
            
            Debug.Log("Подсказка");
            
            // Сначала взять финишировавшие плитки
            // среди них найти сначала 2 с одним типом и найти такую же среди доступных кирпичиков
            // если не нашлось и есть 2 свободных места, то найти 1 финишировавший и найти 2 таких же в доступных
            // если не нашлось и есть 3 свободных места, то найти 3 доступных плитки 
            // если места нет то искать среди недоступных
            
            List<Brick> finishBricks = BrickUtils.AllFinishBricks();
            List<Brick> clickableBricks = BrickUtils.AllClickableBricks();

            List<List<Brick>> finishBricksByType = finishBricks
                .GroupBy(brick => brick.Type)
                .Where(bricks => bricks.Count() >= 2)
                .Select(group => group.ToList())
                .ToList();
            if (finishBricksByType.Any())
            {
                Brick brick = clickableBricks.First(brick => brick.Type.Equals(finishBricksByType[0][0].Type));
                if (brick != null)
                {
                    Debug.Log("НАШЕЛ " + brick.Type + "  " + brick.GameObject.transform.position);
                    MoveBrick(brick);
                }
            }
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
            BrickUtils.UpdateBricksState();
        }
    }
}
