using System;
using Enums;
using UnityEngine;

namespace Classes
{
    public class Brick : ICloneable
    {
        public GameObject GameObject { get; }  // Сам объект

        public bool IsTouch { get; set; } // Было нажатие на этот кирпич

        public bool IsFinish { get; set; } // Закончил движение
    
        public bool IsDown { get; set; } // Состояние нажатия на этот кирпич
    
        public bool IsClickable { get; set; } // Доступен для нажатия
    
        public int TargetWaypoint { get; set; } // Точка на которой находится кирпич
    
        public BrickType Type { get; } // Тип кирпичика
    
        public int Layer { get; set; } // Слой для отображения слайдов
    
        public float Size { get; set; } // Размер
    
        public bool IsToDestroy { get; set; } // Помечен ли на уничтожение 
    
        public Vector3 TargetPosition { get; set; } // Позиция на которой должен находиться кирпичик при старте уровня
    
        public bool IsSwipe { get; set; } // Флаг меняется ли сейчас кирпичик местами
    
        public Brick LastMoveState { get; set; } // Информация о плитке если она является последним шагом 
    
        public bool IsLastMove { get; set; } // Флаг является ли сейчас плитка последним ходом
        
        public int GoldenStateMoves { get; set; } // Шагов до окончания состояния "Золотой"

        public bool IsUnknownTile { get; set; } // Флаг является ли плитка "Неизвестной"
        
        public Brick(GameObject gameObject, InitialBrick initialBrick, float size, Vector3 targetPosition)
        {
            GameObject = gameObject;
            Type = initialBrick.Type;
            IsTouch = false;
            IsFinish = false;
            IsClickable = false;
            Layer = initialBrick.Layer;
            Size = size;
            IsToDestroy = false;
            TargetPosition = targetPosition;
            IsSwipe = false;
            GoldenStateMoves = 0;
        }

        public Brick(GameObject gameObject, SavedBrick savedBrick)
        {
            this.GameObject = gameObject;
            this.IsTouch = savedBrick.IsTouch;
            this.IsFinish = savedBrick.IsFinish;
            this.IsDown = savedBrick.IsDown;
            this.IsClickable = savedBrick.IsClickable;
            this.TargetWaypoint = savedBrick.TargetWaypoint;
            this.TargetPosition = new Vector3(
                savedBrick.TargetPositionX, 
                savedBrick.TargetPositionY,
                savedBrick.TargetPositionZ);
            this.Type = Enum.Parse<BrickType>(savedBrick.Type);
            this.Layer = savedBrick.Layer;
            this.Size = savedBrick.Size;
            this.IsToDestroy = savedBrick.IsToDestroy;
            this.IsSwipe = savedBrick.IsSwipe;
            this.GoldenStateMoves = savedBrick.GoldenStateMoves;
            this.IsUnknownTile = savedBrick.IsUnknownTile;
            if (savedBrick.LastMoveState != null)
            {
                this.LastMoveState = new Brick(null, savedBrick.LastMoveState);
            }
        }

        public bool IsGolden()
        {
            return GoldenStateMoves > 0;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{nameof(IsTouch)}: {IsTouch}, {nameof(IsFinish)}: {IsFinish}, {nameof(IsDown)}: {IsDown}, {nameof(IsClickable)}: {IsClickable}, {nameof(TargetWaypoint)}: {TargetWaypoint}, {nameof(Type)}: {Type}, {nameof(Layer)}: {Layer}, {nameof(Size)}: {Size}, {nameof(IsToDestroy)}: {IsToDestroy}, {nameof(TargetPosition)}: {TargetPosition}, {nameof(IsSwipe)}: {IsSwipe}, {nameof(LastMoveState)}: {LastMoveState}, {nameof(IsLastMove)}: {IsLastMove}";
        }
    }
}
