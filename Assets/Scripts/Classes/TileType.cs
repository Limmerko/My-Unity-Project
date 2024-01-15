using System;
using Enums;
using UnityEngine;

namespace Classes
{
    [Serializable]
    public class TileType
    {
        public BrickType type;

        public Sprite spriteUp;

        public Sprite spriteDown;

        public Sprite spriteGoldenUp;
        
        public Sprite spriteGoldenDown;
    }
}