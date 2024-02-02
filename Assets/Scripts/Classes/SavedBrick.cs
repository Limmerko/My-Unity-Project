using System;
using Enums;
using UnityEngine;

namespace Classes
{
    [Serializable]
    public class SavedBrick
    {
        public bool IsTouch { get; set; } // Было нажатие на этот кирпич

        public bool IsFinish { get; set; } // Закончил движение
    
        public bool IsDown { get; set; } // Состояние нажатия на этот кирпич
    
        public bool IsClickable { get; set; } // Доступен для нажатия
    
        public int TargetWaypoint { get; set; } // Точка на которой находится кирпич
    
        public string Type { get; set; } // Тип кирпичика
    
        public int Layer { get; set; } // Слой для отображения слайдов
    
        public float Size { get; set; } // Размер
    
        public bool IsToDestroy { get; set; } // Помечен ли на уничтожение 
    
        public float TargetPositionX { get; set; } // Позиция на которой должен находиться кирпичик при старте уровня (X)
        public float TargetPositionY { get; set; } // Позиция на которой должен находиться кирпичик при старте уровня (Y)
        public float TargetPositionZ { get; set; } // Позиция на которой должен находиться кирпичик при старте уровня (Z)
        
        public float PositionX { get; set; } // Позиция на находится кирпичик (X)
        public float PositionY { get; set; } // Позиция на находится кирпичик (Y)
        public float PositionZ { get; set; } // Позиция на находится кирпичик (Z)
        
        public bool IsSwipe { get; set; } // Флаг меняется ли сейчас кирпичик местами
        
        public int GoldenStateMoves { get; set; } // Шагов до окончания состояния "Золотой"
        
        public bool IsUnknownTile { get; set; } // Флаг является ли плитка "Неизвестной"

        public SavedBrick LastMoveState { get; set; } // Информация о плитке если она является последним шагом 
        
        public SavedBrick()
        {
            
        }

        public SavedBrick(Brick brick)
        {
            this.IsTouch = brick.IsTouch;
            this.IsFinish = brick.IsFinish;
            this.IsDown = brick.IsDown;
            this.IsClickable = brick.IsClickable;
            this.TargetWaypoint = brick.TargetWaypoint;
            this.Type = brick.Type.ToString();
            this.Size = brick.Size;
            this.Layer = brick.Layer;
            this.IsToDestroy = brick.IsToDestroy;
            this.IsSwipe = brick.IsSwipe;
            this.GoldenStateMoves = brick.GoldenStateMoves;
            this.IsUnknownTile = brick.IsUnknownTile;
            
            Vector3 targetPosition = brick.TargetPosition;
            this.TargetPositionX = targetPosition.x;
            this.TargetPositionY = targetPosition.y;
            this.TargetPositionZ = targetPosition.z;

            if (brick.LastMoveState != null)
            {
                this.LastMoveState = new SavedBrick(brick.LastMoveState);
            }
            if (brick.IsFinish)
            {
                this.Size = brick.GameObject.transform.localScale.x;
            }
            if (brick.GameObject != null) {
                Vector3 transformPosition = brick.GameObject.transform.position;
                this.PositionX = transformPosition.x;
                this.PositionY = transformPosition.y;
                this.PositionZ = transformPosition.z;
            }
        }

        public override string ToString()
        {
            return $"{nameof(IsFinish)}: {IsFinish}, {nameof(TargetWaypoint)}: {TargetWaypoint}, {nameof(Type)}: {Type}, {nameof(Layer)}: {Layer}";
        }
    }
}