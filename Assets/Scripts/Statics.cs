using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Statics
{
    public static List<Brick> AllBricks = new(); // Все кирпичики на сцене

    public static bool IsGameOver = false;

    public static Color IsClickableColor = Color.white;
    
    public static Color IsNotClickableColor = Color.gray;

    public static readonly List<List<InitialBrick>> AllLevels = new() // Список всех уровней
    {
        Levels.Level1,
        Levels.Level2,
        Levels.Level3,
        Levels.Level4,
        Levels.Level5,
        Levels.Level6,
        Levels.Level7,
        Levels.Level8,
        Levels.Level9,
        Levels.Level10,
        Levels.Level11,
        Levels.Level12,
        Levels.Level13,
        Levels.Level14,
        Levels.Level15,
        Levels.Level16,
        Levels.Level17,
        Levels.Level18,
        Levels.Level19
    };
}
