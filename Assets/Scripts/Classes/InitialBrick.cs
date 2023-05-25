using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBrick
{
    public int X { get; set; }
    
    public int Y { get; set; }
    
    public int Layer { get; set; }

    public InitialBrick(int x, int y, int layer)
    {
        X = x;
        Y = y;
        Layer = layer;
    }

    public override string ToString()
    {
        return "X " + X + " Y " + Y + " Layer " + Layer;
    }
}
