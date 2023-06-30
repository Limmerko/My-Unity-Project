using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public List<InitialBrick> Bricks { get; }
    
    public int CountTypes { get; }

    public Level(List<InitialBrick> bricks, int countTypes)
    {
        Bricks = bricks;
        CountTypes = countTypes;
    }
}
