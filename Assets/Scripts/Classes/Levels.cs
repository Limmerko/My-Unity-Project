using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Levels
{
    public static List<InitialBrick> Level1 = new List<InitialBrick>()
    {
        new InitialBrick(0, 0, 0),
        new InitialBrick(1, 0, 0),
        new InitialBrick(2, 0, 0),
        new InitialBrick(3, 0,0),
        new InitialBrick(-1, 0, 0),
        new InitialBrick(-2, 0, 0),
        new InitialBrick(-3, 0, 0),

        new InitialBrick(0, 1, 0),
        new InitialBrick(1, 1, 0),
        new InitialBrick(2, 1, 0),
        new InitialBrick(3, 1, 0),
        new InitialBrick(-1, 1, 0),
        new InitialBrick(-2, 1, 0),
        new InitialBrick(-3, 1, 0),

        new InitialBrick(0, -1, 0),
        new InitialBrick(1, -1, 0),
        new InitialBrick(2, -1, 0),
        new InitialBrick(3, -1, 0),
        new InitialBrick(-1, -1, 0),
        new InitialBrick(-2, -1, 0),
        new InitialBrick(-3, -1, 0),

        new InitialBrick(0, 0, 1),
        new InitialBrick(1, 0, 1),
        new InitialBrick(2, 0, 1),
        new InitialBrick(-1, 0, 1),
        new InitialBrick(-2, 0, 1),
        new InitialBrick(-3, 0, 1),

        new InitialBrick(0, -1, 1),
        new InitialBrick(1, -1, 1),
        new InitialBrick(2, -1, 1),
        new InitialBrick(-1, -1, 1),
        new InitialBrick(-2, -1, 1),
        new InitialBrick(-3, -1, 1)
    };
    
    public static List<InitialBrick> Level2 = new List<InitialBrick>()
    {
        new InitialBrick(-2, -1, 1),
        new InitialBrick(-1, -1, 1),
        new InitialBrick(1, -1, 0),
        new InitialBrick(0, -1, 0),
        new InitialBrick(-1, -1, 0),
        new InitialBrick(2, -1, 0),
        new InitialBrick(2, 0, 0),
        new InitialBrick(0, 0, 1),
        new InitialBrick(-1, 0, 0),
        new InitialBrick(0, 0, 0),
        new InitialBrick(1, 0, 1),
        new InitialBrick(1, 0, 0),
    };
}
