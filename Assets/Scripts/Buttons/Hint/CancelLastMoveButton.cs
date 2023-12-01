using Classes;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Buttons.Hint
{
    /**
    * Кнопка "Отменить последних ход" в подсказках
    */
    public class CancelLastMoveButton : CommonHintButton
    {
        protected override void StartProcess()
        {
            PrefCount = "CountCancelLastMove";
            base.StartProcess();
        }

        /**
         * Отмена последнего хода и возвращение плитки на место
         */
        protected override void Process()
        {
            if (BrickUtils.IsSwipingNow() || 
                !Statics.LevelStart || 
                Statics.LastMoves.Count == 0 || 
                BrickUtils.AllTouchAndNotFinishBricks().Count > 0 ||
                BrickUtils.AllFinishBricks().Count == 0 ||
                PlayerPrefs.GetInt(PrefCount) == 0) return;
            
            Debug.Log("Отменить");

            CancelLastMove(); // TODO может что-то выводить если нет последних ходов?
            
            CheckCount();
        }

        public void CancelLastMove()
        {
            Brick brick = Statics.LastMoves.FindLast(b => b != null && !b.GameObject.IsDestroyed() && !b.IsToDestroy);
            if (brick != null)
            {
                brick.TargetWaypoint = 0;
                brick.IsTouch = false;
                brick.IsFinish = false;
                brick.IsLastMove = true;
                brick.Layer = brick.LastMoveState.Layer;
                brick.Size = brick.LastMoveState.Size;
                Statics.LastMoves.Remove(brick);
                BrickUtils.UpdateBricksPosition();
            }
        }
    }
}