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
        
        public List<Golden> goldens;

        [Serializable]
        public class Golden
        {
            public Sprite spriteUp;

            public Sprite spriteDown;
        }
    }
}