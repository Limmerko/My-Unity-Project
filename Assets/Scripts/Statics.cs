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
        new Level("Level0", Levels.Level0, 2, 3, 6),
        new Level("Level28", Levels.Level28, 7, 11, 7),
        new Level("Level7", Levels.Level7, 6, 11, 6),
        new Level("Level27", Levels.Level27, 8, 13, 7),
        new Level("Level44", Levels.Level44, 8, 12, 7),
        new Level("Level11", Levels.Level11, 8, 12, 6),
        new Level("Level1", Levels.Level1, 8, 14, 7),
        new Level("Level53", Levels.Level53, 9, 14, 7),
        new Level("Level76", Levels.Level76, 8, 14, 7),
        new Level("Level51", Levels.Level51, 7, 14, 6),
        new Level("Level46", Levels.Level46, 9, 14, 6),
        new Level("Level33", Levels.Level33, 10, 15, 7),
        new Level("Level18", Levels.Level18, 10, 15, 8),
        new Level("Level34", Levels.Level34, 12, 18, 7),
        new Level("Level20", Levels.Level20, 10, 16, 7),
        new Level("Level75", Levels.Level75, 10, 15, 7),
        new Level("Level63", Levels.Level63, 12, 18, 8),
        new Level("Level42", Levels.Level42, 12, 18, 7),
        new Level("Level25", Levels.Level25, 13, 19, 7),
        new Level("Level48", Levels.Level48, 13, 19, 8),
        new Level("Level29", Levels.Level29, 10, 18, 7),
        new Level("Level41", Levels.Level41, 11, 19, 7),
        new Level("Level57", Levels.Level57, 12, 19, 7),
        new Level("Level38", Levels.Level38, 12, 21, 7),
        new Level("Level50", Levels.Level50, 13, 20, 7),
        new Level("Level52", Levels.Level52, 9, 20, 6),
        new Level("Level6", Levels.Level6, 12, 21, 7),
        new Level("Level12", Levels.Level12, 15, 22, 6),
        new Level("Level40", Levels.Level40, 7, 22, 7),
        new Level("Level49", Levels.Level49, 8, 22, 6),
        new Level("Level31", Levels.Level31, 15, 22, 7),
        new Level("Level9", Levels.Level9, 14, 22, 7),
        new Level("Level30", Levels.Level30, 12, 22, 7),
        new Level("Level43", Levels.Level43, 16, 23, 7),
        new Level("Level47", Levels.Level47, 16, 23, 8),
        new Level("Level22", Levels.Level22, 13, 23, 8),
        new Level("Level13", Levels.Level13, 15, 23, 6),
        new Level("Level58", Levels.Level58, 15, 23, 7),
        new Level("Level73", Levels.Level73, 14, 23, 9),
        new Level("Level24", Levels.Level24, 15, 23, 7),
        new Level("Level37", Levels.Level37, 15, 23, 7),
        new Level("Level93", Levels.Level93, 16, 23, 7),
        new Level("Level45", Levels.Level45, 8, 24, 7),
        new Level("Level86", Levels.Level86, 16, 24, 7),
        new Level("Level81", Levels.Level81, 10, 24, 7),
        new Level("Level71", Levels.Level71, 14, 24, 8),
        new Level("Level79", Levels.Level79, 16, 25, 8),
        new Level("Level55", Levels.Level55, 18, 26, 8),
        new Level("Level39", Levels.Level39, 15, 27, 7),
        new Level("Level15", Levels.Level15, 18, 27, 7),
        new Level("Level4", Levels.Level4, 16, 26, 9),
        new Level("Level14", Levels.Level14, 17, 27, 7),
        new Level("Level23", Levels.Level23, 12, 27, 7),
        new Level("Level66", Levels.Level66, 20, 28, 8),
        new Level("Level95", Levels.Level95, 13, 27, 6),
        new Level("Level82", Levels.Level82, 14, 27, 8),
        new Level("Level54", Levels.Level54, 16, 28, 8),
        new Level("Level61", Levels.Level61, 11, 28, 8),
        new Level("Level2", Levels.Level2, 12, 29, 7),
        new Level("Level72", Levels.Level72, 18, 29, 8),
        new Level("Level26", Levels.Level26, 15, 28, 7),
        new Level("Level64", Levels.Level64, 15, 29, 8),
        new Level("Level67", Levels.Level67, 17, 29, 8),
        new Level("Level84", Levels.Level84, 14, 29, 8),
        new Level("Level77", Levels.Level77, 13, 29, 7),
        new Level("Level36", Levels.Level36, 15, 29, 7),
        new Level("Level70", Levels.Level70, 16, 30, 9),
        new Level("Level19", Levels.Level19, 18, 30, 8),
        new Level("Level90", Levels.Level90, 12, 30, 9),
        new Level("Level87", Levels.Level87, 13, 30, 7),
        new Level("Level32", Levels.Level32, 15, 30, 7),
        new Level("Level8", Levels.Level8, 16, 31, 7),
        new Level("Level60", Levels.Level60, 15, 31, 8),
        new Level("Level68", Levels.Level68, 17, 32, 8),
        new Level("Level10", Levels.Level10, 16, 32, 7),
        new Level("Level98", Levels.Level98, 13, 31, 8),
        new Level("Level21", Levels.Level21, 18, 32, 8),
        new Level("Level5", Levels.Level5, 15, 32, 7),
        new Level("Level59", Levels.Level59, 16, 33, 9),
        new Level("Level85", Levels.Level85, 15, 32, 7),
        new Level("Level83", Levels.Level83, 16, 32, 9),
        new Level("Level65", Levels.Level65, 16, 34, 8),
        new Level("Level74", Levels.Level74, 21, 34, 7),
        new Level("Level56", Levels.Level56, 20, 36, 9),
        new Level("Level99", Levels.Level99, 15, 35, 7),
        new Level("Level35", Levels.Level35, 15, 35, 7),
        new Level("Level62", Levels.Level62, 16, 37, 9),
        new Level("Level17", Levels.Level17, 13, 37, 8),
        new Level("Level88", Levels.Level88, 13, 39, 6),
        new Level("Level80", Levels.Level80, 9, 39, 7),
        new Level("Level89", Levels.Level89, 10, 38, 7),
        new Level("Level91", Levels.Level91, 12, 40, 9),
        new Level("Level69", Levels.Level69, 25, 41, 9),
        new Level("Level3", Levels.Level3, 16, 47, 9),
        new Level("Level97", Levels.Level97, 12, 44, 8),
        new Level("Level78", Levels.Level78, 18, 42, 8),
        new Level("Level16", Levels.Level16, 18, 49, 8),
        new Level("Level92", Levels.Level92, 17, 55, 9),
        new Level("Level96", Levels.Level96, 19, 59, 8)
    };
    // TODO отсортировать по сложности
}
