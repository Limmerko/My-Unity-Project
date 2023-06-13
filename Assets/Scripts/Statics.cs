using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Statics
{
    public static List<Brick> AllBricks = new(); // Все кирпичики на сцене

    public static bool IsGameOver = false;

    public static readonly List<List<InitialBrick>> AllLevels = new() // Список всех уровней
    {
        Levels.Level1,
        Levels.Level2,
        Levels.Level3
    };
}
