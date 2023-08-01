using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Statics
{
    public static List<Brick> AllBricks = new(); // Все кирпичики на сцене

    public static bool IsGameOver = false;

    public static Color IsClickableColor = Color.white;
    
    public static Color IsNotClickableColor = Color.gray;
    
    public static readonly List<Level> AllLevels = new() // Список всех уровней
    {
        new Level(Levels.Level1, 5),
        new Level(Levels.Level1, 11),
        new Level(Levels.Level2, 8),
        new Level(Levels.Level2, 15), 
        new Level(Levels.Level3, 10),
        new Level(Levels.Level3, 18),
        new Level(Levels.Level4, 10),
        new Level(Levels.Level4, 15),
        new Level(Levels.Level5, 8),
        new Level(Levels.Level5, 12),
        new Level(Levels.Level6, 10),
        new Level(Levels.Level6, 14),
        new Level(Levels.Level7, 10),
        new Level(Levels.Level8, 10),
        new Level(Levels.Level8, 16),
        new Level(Levels.Level9, 10),
        new Level(Levels.Level9, 15),
        new Level(Levels.Level10, 10),
        new Level(Levels.Level10, 18),
        new Level(Levels.Level11, 10),
        new Level(Levels.Level11, 18),
        new Level(Levels.Level12, 10),
        new Level(Levels.Level12, 16),
        new Level(Levels.Level13, 9),
        new Level(Levels.Level13, 14),
        new Level(Levels.Level14, 10),
        new Level(Levels.Level14, 17),
        new Level(Levels.Level15, 10),
        new Level(Levels.Level15, 17),
        new Level(Levels.Level16, 10),
        new Level(Levels.Level16, 18),
        new Level(Levels.Level17, 9),
        new Level(Levels.Level17, 14),
        new Level(Levels.Level18, 10),
        new Level(Levels.Level19, 11),
        new Level(Levels.Level19, 16),
        new Level(Levels.Level20, 10),
        new Level(Levels.Level21, 10),
        new Level(Levels.Level21, 16),
        new Level(Levels.Level22, 8),
        new Level(Levels.Level22, 16),
        new Level(Levels.Level23, 8),
        new Level(Levels.Level23, 12),
    };
}
