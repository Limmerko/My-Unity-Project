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
    
        public bool IsSwipe { get; set; } // Флаг меняется ли сейчас кирпичик местами
    
        // public Brick LastMoveState { get; set; } // Информация о плитке если она является последним шагом 
    
        // public bool IsLastMove { get; set; } // Флаг является ли сейчас плитка последним ходом

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
            this.Layer = brick.Layer;
            this.Size = brick.Size;
            this.IsToDestroy = brick.IsToDestroy;
            Vector3 targetPosition = brick.GameObject.transform.position;
            this.TargetPositionX = targetPosition.x;
            this.TargetPositionY = targetPosition.y;
            this.TargetPositionZ = targetPosition.z;
            this.IsSwipe = brick.IsSwipe;
            // this.LastMoveState = brick.LastMoveState;
            // this.IsLastMove = brick.IsLastMove;
        }

        public override string ToString()
        {
            return $"{nameof(IsFinish)}: {IsFinish}, {nameof(TargetWaypoint)}: {TargetWaypoint}, {nameof(Type)}: {Type}, {nameof(Layer)}: {Layer}";
        }
    }
}