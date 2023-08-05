using Unity.VisualScripting;
using UnityEngine;

namespace Buttons.Hint
{
    /**
    * Кнопка "Отменить последних ход" в подсказках
    */
    public class CancelLastMoveButton : CommonButton
    {
        protected override void StartProcess()
        {
            // empty
        }

        /**
         * Отмена последнего хода и возвращение плитки на место
         */
        protected override void Process()
        {
            if (BrickUtils.IsSwipingNow() || 
                !Statics.LevelStart || 
                Statics.LastMoves.Count == 0 || 
                BrickUtils.AllTouchAndNotFinishBricks().Count > 0) return;
            
            Debug.Log("Отменить");
            
            Brick brick = Statics.LastMoves.FindLast(b => b != null && !b.GameObject.IsDestroyed() && !b.IsToDestroy);
            if (brick != null)
            {
                brick.TargetWaypoint = 0;
                brick.IsTouch = false;
                brick.IsFinish = false;
                brick.IsLastMove = true;
                brick.Layer = brick.LastMoveState.Layer;
                Statics.LastMoves.Remove(brick);
                BrickUtils.UpdateBricksPosition();
            }
        }
    }
}