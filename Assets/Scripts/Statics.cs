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
        new Level("Level2", Levels.Level2, 12, 78, 8),
        new Level("Level3", Levels.Level3, 16, 450),
        new Level("Level4", Levels.Level4, 15, 90),
        new Level("Level5", Levels.Level5, 15, 130),
        new Level("Level6", Levels.Level6, 12, 72, 7),
        new Level("Level7", Levels.Level7, 6, 0, 7),
        new Level("Level8", Levels.Level8, 14, 117),
        new Level("Level9", Levels.Level9, 13, 81),
        new Level("Level10", Levels.Level10, 16, 156),
        new Level("Level11", Levels.Level11, 8, 255, 7),
        new Level("Level12", Levels.Level12, 14, 90),
        new Level("Level13", Levels.Level13, 14, 81),
        new Level("Level14", Levels.Level14, 17, 99),
        new Level("Level15", Levels.Level15, 15, 90, 8),
        new Level("Level16", Levels.Level16, 18, 276),
        new Level("Level17", Levels.Level17, 13, 288),
        new Level("Level18", Levels.Level18, 10, 36, 8),
        new Level("Level19", Levels.Level19, 18, 108),
        new Level("Level20", Levels.Level20, 10, 54, 8),
        new Level("Level21", Levels.Level21, 18, 156),
        new Level("Level22", Levels.Level22, 11, 54),
        new Level("Level23", Levels.Level23, 12, 100),
        new Level("Level24", Levels.Level24, 15, 0, 7),
        new Level("Level25", Levels.Level25, 13, 0, 7),
        new Level("Level26", Levels.Level26, 15, 0, 7),
        new Level("Level27", Levels.Level27, 8, 0, 7),
        new Level("Level28", Levels.Level28, 7, 0, 7),
        new Level("Level29", Levels.Level29, 10, 0, 7),
        new Level("Level30", Levels.Level30, 12, 0, 7),
        new Level("Level31", Levels.Level31, 15, 0, 7),
        new Level("Level32", Levels.Level32, 15, 0, 7),
        new Level("Level33", Levels.Level33, 10, 0, 7),
        new Level("Level34", Levels.Level34, 12, 0, 7),
        new Level("Level35", Levels.Level35, 15, 0, 7),
        new Level("Level36", Levels.Level36, 15, 0, 7),
        new Level("Level37", Levels.Level37, 15, 0, 7),
        new Level("Level38", Levels.Level38, 10, 0, 7),
        new Level("Level39", Levels.Level39, 15, 0, 7),
        new Level("Level40", Levels.Level40, 7, 0, 7),
        new Level("Level41", Levels.Level41, 9, 0, 7),
        new Level("Level42", Levels.Level42, 12, 0, 7),
        new Level("Level43", Levels.Level43, 16, 0, 7),
        new Level("Level44", Levels.Level44, 8, 0, 7),
        new Level("Level45", Levels.Level45, 8, 0, 7),
        new Level("Level46", Levels.Level46, 9, 0, 7),
        new Level("Level47", Levels.Level47, 16, 0, 8),
        new Level("Level48", Levels.Level48, 13, 0, 8),
        new Level("Level49", Levels.Level49, 8, 0, 7),
        new Level("Level50", Levels.Level50, 13, 0, 8),
        new Level("Level51", Levels.Level51, 7, 0, 7),
        new Level("Level52", Levels.Level52, 9, 0, 7),
        new Level("Level53", Levels.Level53, 8, 0, 7),
        new Level("Level54", Levels.Level54, 16, 0, 8),
        new Level("Level55", Levels.Level55, 18, 0, 8),
        new Level("Level56", Levels.Level56, 20, 0, 9),
        new Level("Level57", Levels.Level57, 12, 0, 7),
        new Level("Level58", Levels.Level58, 13, 0, 7),
        new Level("Level59", Levels.Level59, 15, 0, 9),
    };
}
