using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBrick
{
    public float X { get; set; }
    
    public float Y { get; set; }
    
    public int Layer { get; set; }
    
    public BrickType Type { get; set; }

    public InitialBrick(float x, float y, int layer)
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
