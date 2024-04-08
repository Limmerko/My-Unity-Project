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

    public const int MaxFinishTiles = 7; // Максимальное кол-во финишировавших плиток

    public const int LevelStartGoldenTiles = 5; // Уровень с которого появляются "Золотые" плитки
    
    public const int LevelStartUnknownTiles = 10; // Уровень с которого появляются "Неизвестные" плитки

    public const int CountMovesGoldenState = 3;

    public const int MaxGoldenTiles = 3;
    
    public const int CountMovesLiveState = 3;

    public const int MaxLives = 3;
    
    public static readonly List<Level> AllLevels = new() // Список всех уровней
    {
        new Level("Level1", Levels.Level1, 6, 12, 8),
        new Level("Level18", Levels.Level18, 10, 36, 8),
        new Level("Level20", Levels.Level20, 10, 54, 8),
        new Level("Level6", Levels.Level6, 12, 72, 7),
        new Level("Level22", Levels.Level22, 11, 54),
        new Level("Level2", Levels.Level2, 12, 78, 8),
        new Level("Level13", Levels.Level13, 14, 81),
        new Level("Level15", Levels.Level15, 15, 90, 8),
        new Level("Level4", Levels.Level4, 15, 90),
        new Level("Level9", Levels.Level9, 13, 81),
        new Level("Level12", Levels.Level12, 14, 90),
        new Level("Level23", Levels.Level23, 12, 100),
        new Level("Level14", Levels.Level14, 17, 99),
        new Level("Level19", Levels.Level19, 18, 108),
        new Level("Level8", Levels.Level8, 14, 117),
        new Level("Level5", Levels.Level5, 15, 130),
        new Level("Level10", Levels.Level10, 16, 156),
        new Level("Level21", Levels.Level21, 18, 156),
        new Level("Level11", Levels.Level11, 8, 255, 7),
        new Level("Level17", Levels.Level17, 13, 288),
        new Level("Level16", Levels.Level16, 18, 276),
        new Level("Level3", Levels.Level3, 16, 450),
        new Level("Level7", Levels.Level7, 6, 0, 7),
        new Level("Level24", Levels.Level24, 15, 0, 7),
        new Level("Level25", Levels.Level25, 13, 0, 7),
    };
}
