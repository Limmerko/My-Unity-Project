using System;
using System.Collections.Generic;
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
        
        public List<AnyType> goldens;
        
        public List<AnyType> lives;

        [Serializable]
        public class AnyType
        {
            public Sprite spriteUp;

            public Sprite spriteDown;
        }
    }
}