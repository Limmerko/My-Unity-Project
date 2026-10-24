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

    public const int LevelStartLiveTiles = 15; // Уровень с которого появляются "Восполнение жизней" плитки

    public const int CountMovesGoldenState = 3;

    public const int MaxGoldenTiles = 3;
    
    public const int CountMovesLiveState = 3;

    public const int MaxLives = 3;
    
    public static readonly List<Level> AllLevels = new() // Список всех уровней
    {
        new Level("Level0", Levels.Level0, 2, 16, 7),
        new Level("Level11", Levels.Level11, 8, 16, 7),
        new Level("Level28", Levels.Level28, 7, 16, 7),
        new Level("Level44", Levels.Level44, 8, 16, 7),
        new Level("Level27", Levels.Level27, 8, 24, 7),
        new Level("Level1", Levels.Level1, 8, 24, 8),
        new Level("Level7", Levels.Level7, 6, 18, 7),
        new Level("Level53", Levels.Level53, 8, 24, 7),
        new Level("Level46", Levels.Level46, 9, 30, 7),
        new Level("Level51", Levels.Level51, 7, 40, 7),
        new Level("Level33", Levels.Level33, 10, 36, 7),
        new Level("Level18", Levels.Level18, 10, 36, 8),
        new Level("Level20", Levels.Level20, 10, 54, 8),
        new Level("Level34", Levels.Level34, 12, 56, 7),
        new Level("Level63", Levels.Level63, 12, 56, 8),
        new Level("Level48", Levels.Level48, 13, 56, 8),
        new Level("Level42", Levels.Level42, 12, 56, 7),
        new Level("Level29", Levels.Level29, 10, 63, 7),
        new Level("Level12", Levels.Level12, 14, 72, 7),
        new Level("Level25", Levels.Level25, 13, 84, 7),
        new Level("Level22", Levels.Level22, 11, 81, 8),
        new Level("Level38", Levels.Level38, 10, 72, 7),
        new Level("Level41", Levels.Level41, 9, 84, 7),
        new Level("Level57", Levels.Level57, 12, 84, 7),
        new Level("Level47", Levels.Level47, 16, 90, 8),
        new Level("Level43", Levels.Level43, 16, 90, 7),
        new Level("Level31", Levels.Level31, 15, 90, 7),
        new Level("Level49", Levels.Level49, 8, 90, 7),
        new Level("Level6", Levels.Level6, 12, 96, 7),
        new Level("Level52", Levels.Level52, 9, 105, 7),
        new Level("Level50", Levels.Level50, 13, 96, 8),
        new Level("Level40", Levels.Level40, 7, 96, 7),
        new Level("Level9", Levels.Level9, 13, 108, 7),
        new Level("Level13", Levels.Level13, 14, 108, 6),
        new Level("Level58", Levels.Level58, 13, 108, 7),
        new Level("Level45", Levels.Level45, 8, 108, 7),
        new Level("Level30", Levels.Level30, 12, 108, 7),
        new Level("Level55", Levels.Level55, 18, 120, 8),
        new Level("Level71", Levels.Level71, 14, 120, 8),
        new Level("Level66", Levels.Level66, 20, 144, 8),
        new Level("Level37", Levels.Level37, 15, 135, 7),
        new Level("Level24", Levels.Level24, 15, 135, 7),
        new Level("Level72", Levels.Level72, 14, 144, 8),
        new Level("Level73", Levels.Level73, 14, 144, 9),
        new Level("Level2", Levels.Level2, 12, 156, 8),
        new Level("Level15", Levels.Level15, 15, 150, 8),
        new Level("Level4", Levels.Level4, 15, 150, 9),
        new Level("Level8", Levels.Level8, 14, 156, 7),
        new Level("Level70", Levels.Level70, 14, 156, 9),
        new Level("Level54", Levels.Level54, 16, 180, 8),
        new Level("Level39", Levels.Level39, 15, 165, 7),
        new Level("Level14", Levels.Level14, 17, 165, 7),
        new Level("Level61", Levels.Level61, 11, 180, 8),
        new Level("Level64", Levels.Level64, 14, 192, 8),
        new Level("Level19", Levels.Level19, 18, 216, 8),
        new Level("Level23", Levels.Level23, 12, 200, 7),
        new Level("Level60", Levels.Level60, 15, 195, 8),
        new Level("Level26", Levels.Level26, 15, 220, 7),
        new Level("Level65", Levels.Level65, 16, 225, 8),
        new Level("Level67", Levels.Level67, 16, 240, 8),
        new Level("Level36", Levels.Level36, 15, 240, 7),
        new Level("Level32", Levels.Level32, 15, 240, 7),
        new Level("Level10", Levels.Level10, 16, 260, 7),
        new Level("Level68", Levels.Level68, 16, 260, 8),
        new Level("Level35", Levels.Level35, 15, 300, 7),
        new Level("Level56", Levels.Level56, 20, 288, 9),
        new Level("Level59", Levels.Level59, 15, 280, 9),
        new Level("Level21", Levels.Level21, 18, 312, 8),
        new Level("Level62", Levels.Level62, 16, 320, 9),
        new Level("Level17", Levels.Level17, 13, 384, 8),
        new Level("Level5", Levels.Level5, 15, 325, 7),
        new Level("Level69", Levels.Level69, 20, 324, 9),
        new Level("Level3", Levels.Level3, 16, 440, 9),
        new Level("Level16", Levels.Level16, 18, 552, 8),
        new Level("Level74", Levels.Level74, 18, 156, 9),
        new Level("Level75", Levels.Level75, 10, 24, 7),
        new Level("Level76", Levels.Level76, 8, 32, 7),
        new Level("Level77", Levels.Level77, 13, 72, 7),
        new Level("Level78", Levels.Level78, 16, 384, 8),
        new Level("Level79", Levels.Level79, 16, 160, 8),
        new Level("Level80", Levels.Level80, 9, 648, 8),
        new Level("Level81", Levels.Level81, 10, 180, 8),
        new Level("Level82", Levels.Level82, 14, 72, 9),
        new Level("Level83", Levels.Level83, 16, 0, 9),
        new Level("Level85", Levels.Level85, 15, 0, 7),
        new Level("Level86", Levels.Level86, 15, 0, 7),
        new Level("Level87", Levels.Level87, 13, 0, 7),
        new Level("Level88", Levels.Level88, 13, 0, 9),
        new Level("Level89", Levels.Level89, 12, 34, 9),
    };
    // TODO отсортировать по сложности
}
