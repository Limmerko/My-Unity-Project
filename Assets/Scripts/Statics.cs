using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Statics
{
    public static List<Brick> AllBricks = new(); // Все кирпичики на сцене

    public static bool IsGameOver = false;

    public static Color IsClickableColor = Color.white;
    
    public static Color IsNotClickableColor = Color.gray;
    
    public static bool LevelStart = false; // Уровень начат или нет

    public static List<Brick> LastMoves = new List<Brick>(); // Список ходов

    public static int TimeScale = 1;
    
    public static readonly List<Level> AllLevels = new() // Список всех уровней
    {
        new Level("Level1", Levels.Level1, 5, 165),
        new Level("Level18", Levels.Level18, 10, 300),
        new Level("Level20", Levels.Level20, 10, 300),
        new Level("Level1", Levels.Level1, 11, 363),
        new Level("Level22", Levels.Level22, 8, 384),
        new Level("Level13", Levels.Level13, 9, 405),
        new Level("Level6", Levels.Level6, 10, 420),
        new Level("Level23", Levels.Level23, 8, 432),
        new Level("Level9", Levels.Level9, 10, 450),
        new Level("Level12", Levels.Level12, 10, 480),
        new Level("Level4", Levels.Level4, 10, 510),
        new Level("Level2", Levels.Level2, 8, 528),
        new Level("Level5", Levels.Level5, 8, 528),
        new Level("Level15", Levels.Level15, 10, 540),
        new Level("Level14", Levels.Level14, 10, 570),
        new Level("Level6", Levels.Level6, 14, 588),
        new Level("Level13", Levels.Level13, 14, 630),
        new Level("Level23", Levels.Level23, 12, 648),
        new Level("Level21", Levels.Level21, 10, 660),
        new Level("Level9", Levels.Level9, 15, 675),
        new Level("Level8", Levels.Level8, 10, 690),
        new Level("Level10", Levels.Level10, 10, 690),
        new Level("Level19", Levels.Level19, 11, 693),
        new Level("Level17", Levels.Level17, 9, 729),
        new Level("Level4", Levels.Level4, 15, 765),
        new Level("Level12", Levels.Level12, 16, 768),
        new Level("Level22", Levels.Level22, 16, 768),
        new Level("Level5", Levels.Level5, 12, 792),
        new Level("Level11", Levels.Level11, 10, 870),
        new Level("Level15", Levels.Level15, 17, 918),
        new Level("Level14", Levels.Level14, 17, 969),
        new Level("Level2", Levels.Level2, 15, 990),
        new Level("Level19", Levels.Level19, 16, 1008),
        new Level("Level21", Levels.Level21, 16, 1056),
        new Level("Level8", Levels.Level8, 16, 1104),
        new Level("Level17", Levels.Level17, 14, 1134),
        new Level("Level16", Levels.Level16, 10, 1170),
        new Level("Level10", Levels.Level10, 18, 1242),
        new Level("Level3", Levels.Level3, 10, 1530),
        new Level("Level11", Levels.Level11, 18, 1566),
        new Level("Level7", Levels.Level7, 10, 2040),
        new Level("Level16", Levels.Level16, 18, 2106),
        new Level("Level3", Levels.Level3, 18, 2754),
    };
}
