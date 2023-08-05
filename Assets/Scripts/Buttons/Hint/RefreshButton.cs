using System.Collections.Generic;
using UnityEngine;

namespace Buttons.Hint
{
    /**
    * Кнопка "Перемешать" в подсказках
    */
    public class RefreshButton : CommonButton
    {
        protected override void StartProcess()
        {
            // empty
        }
        
        /**
         * Перемешивание всех ненажатых кирпичиков на поле
         */
        protected override void Process()
        {
            if (BrickUtils.IsSwipingNow() || !Statics.LevelStart) return;
            
            Debug.Log("Перемешать");
            
            List<Brick> notTouchBrick = BrickUtils.AllNotTouchBricks();
            notTouchBrick.ForEach(brick => BrickUtils.ChangeClickable(brick, false));
            
            MainUtils.MixList(notTouchBrick);
            for (int i = 0; i < notTouchBrick.Count - (notTouchBrick.Count % 2 == 0 ? 0 : 1); i += 2)
            {
                SwipeBricks(notTouchBrick[i], notTouchBrick[i + 1]);
            }
        }

        private void SwipeBricks(Brick first, Brick second)
        {
            SpriteRenderer spriteFirst = first.GameObject.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteSecond = second.GameObject.GetComponent<SpriteRenderer>();
            
            first.IsSwipe = true;
            second.IsSwipe = true;
            (first.TargetPosition, second.TargetPosition) = (second.TargetPosition, first.TargetPosition);
            (first.Layer, second.Layer) = (second.Layer, first.Layer);
            
            spriteFirst.color = Statics.IsNotClickableColor;
            first.IsClickable = false;
            
            spriteSecond.color = Statics.IsNotClickableColor;
            second.IsClickable = false;
        }
        
    }
}