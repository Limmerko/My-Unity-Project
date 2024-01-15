using System.Collections.Generic;
using System.Linq;
using Classes;
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

    public static int LevelStartGoldenTiles = 0; // Уровень с которого появляются "Золотые" плитки

    public static int CountMovesGoldenState = 3;
    
    public static readonly List<Level> AllLevels = new() // Список всех уровней
    {
        new Level("Level1", Levels.Level1, 5, 12),
        new Level("Level18", Levels.Level18, 10, 36),
        new Level("Level20", Levels.Level20, 10, 54),
        new Level("Level6", Levels.Level6, 10, 72),
        new Level("Level12", Levels.Level12, 10, 54),
        new Level("Level22", Levels.Level22, 8, 54),
        new Level("Level2", Levels.Level2, 8, 78),
        new Level("Level13", Levels.Level13, 9, 81),
        new Level("Level15", Levels.Level15, 10, 90),
        new Level("Level4", Levels.Level4, 10, 90),
        new Level("Level9", Levels.Level9, 10, 81),
        new Level("Level12", Levels.Level12, 16, 90),
        new Level("Level6", Levels.Level6, 14, 96),
        new Level("Level13", Levels.Level13, 14, 108),
        new Level("Level23", Levels.Level23, 8, 100),
        new Level("Level14", Levels.Level14, 10, 99),
        new Level("Level19", Levels.Level19, 11, 108),
        new Level("Level8", Levels.Level8, 10, 117),
        new Level("Level22", Levels.Level22, 16, 135),
        new Level("Level9", Levels.Level9, 15, 135),
        new Level("Level5", Levels.Level5, 8, 130),
        new Level("Level4", Levels.Level4, 15, 150),
        new Level("Level15", Levels.Level15, 17, 150),
        new Level("Level14", Levels.Level14, 17, 165),
        new Level("Level10", Levels.Level10, 10, 156),
        new Level("Level21", Levels.Level21, 10, 156),
        new Level("Level19", Levels.Level19, 16, 180),
        new Level("Level2", Levels.Level2, 15, 195),
        new Level("Level11", Levels.Level11, 10, 255),
        new Level("Level23", Levels.Level23, 12, 200),
        new Level("Level8", Levels.Level8, 16, 195),
        new Level("Level5", Levels.Level5, 12, 260),
        new Level("Level21", Levels.Level21, 16, 260),
        new Level("Level10", Levels.Level10, 18, 312),
        new Level("Level17", Levels.Level17, 9, 288),
        new Level("Level16", Levels.Level16, 10, 276),
        new Level("Level17", Levels.Level17, 14, 384),
        new Level("Level3", Levels.Level3, 10, 450),
        new Level("Level3", Levels.Level3, 18, 900),
        new Level("Level16", Levels.Level16, 18, 552),
        new Level("Level11", Levels.Level11, 18, 510),
        new Level("Level7", Levels.Level7, 10, 960),
    };
}
