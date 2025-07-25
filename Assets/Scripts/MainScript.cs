using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Enums;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Random = System.Random;

public class MainScript : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab; // Плитка
    [SerializeField] private GameObject finishPlacePrefab; // Верхняя и нижняя часть места для приземления 
    [SerializeField] private GameObject waypointPrefab; // Одна из точек приземления
    [SerializeField] private GameObject coinPrefab; // Монета
    [SerializeField] private GameObject coinsPlace; // Место расположения всех монет
    [SerializeField] private TextMeshProUGUI coinsText; // Текст с кол-вом монет
    [SerializeField] private GameObject liveHeartPrefab; // Сердце
    [SerializeField] private GameObject liveHeartPlace; // Место расположения сердца
    [SerializeField] private TextMeshProUGUI livesText; // Текст с кол-вом жизней
    [SerializeField] private GameObject backgroundPanel; // Фон на всех панелях
    [SerializeField] private GameObject losePanel; // Внутрення панель окончания игры
    [SerializeField] private GameObject nextLevelPanel; // Панель перехода на следующий уровень
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject background; // Фон 
    [SerializeField] private List<Sprite> allBackgroundSprites; // Все возможное картинки для фонаПри
    [SerializeField] private AudioSource soundCollectThreeTiles; // Звук движения до финиша
    [SerializeField] private AudioSource soundVictory; // Звук прохождения уровня
    [SerializeField] private AudioSource soundGameOver; // Звук проигрыша
    [SerializeField] private AudioSource soundMoveBricks; // Звук движения плиток до места  
    [SerializeField] private AudioSource musicSound; // Фоновая музыка (звук)
    [SerializeField] private GameObject resumeAfterLoseByLiveButton; // Кнопка "Возобновить" за жизни
    [SerializeField] private GameObject resumeAfterLoseByAdButton; // Кнопка "Возобновить" за рекламу
    
    private Animation _backgroundPanelAnim; // Анимация фона паузы
    private Animation _losePanelAnim; // Анимация панели окончания игры
    private Animation _nextLevelPanelAnim; // Анимация панели перехода на следующий уровень
    private GameObject _music; // Сам объект для мызука

    private Level _level;
    private float _brickSize; 
    private float _sizeFinishBrick;
    private Random _random = new Random();
    private List<SpriteRenderer> _backIcons = new(); // Икноки на фоне
    private List<BrickType> _limitedTypes = new();
    private bool _isStartSavedlevel = false;
    private int _finishedLayerOnStart = 0;
    private const String CoinsPref = "Coins";
    private const String LivesPref = "Lives";

    private void Awake()
    {
        MainUtils.VibrationInit();
        Statics.TimeScale = 1;
        Statics.AllBricks = new List<Brick>();
        Statics.LastMoves = new List<Brick>();
        backgroundPanel.SetActive(false);
        losePanel.SetActive(false);
        nextLevelPanel.SetActive(false);
        Statics.IsGameOver = false;
        Statics.LevelStart = false;
        coinsText.text = PlayerPrefs.GetInt(CoinsPref).ToString();
        livesText.text = PlayerPrefs.GetInt(LivesPref).ToString();

        _backgroundPanelAnim = backgroundPanel.GetComponent<Animation>();
        _losePanelAnim = losePanel.GetComponent<Animation>();
        _nextLevelPanelAnim = nextLevelPanel.GetComponent<Animation>();
        _backIcons = background.GetComponentsInChildren<SpriteRenderer>()
            .Where(icon => "Background".Equals(icon.tag))
            .ToList();
        _music = GameObject.FindWithTag("Music");
    }

    private void Start()    
    {
        PlayMusicOnAwake();
        DefineLevel();
        InitializeFinishPlace();
        InitializedTypes();
        InitializedBricks();
        InitializeBackGround();

        StartLevel();
    }
    
    private void Update()
    {
        if (Statics.LevelStart)
        {
            CheckFinishBricks();
            CheckNextLevel();
            CheckBrickState();
        }
        
        if (BrickUtils.IsSwipingNow() && !soundMoveBricks.isPlaying)
        {
            MainUtils.PlaySound(soundMoveBricks);
        }

        CheckPlayMusic();
    }

    private void FixedUpdate()
    {
        if (!Statics.LevelStart && Statics.TimeScale == 1)
        {
            MoveAllWaypointsOnTargetPosition();
        }
    }

    /**
     * Определение уровня
     */
    private void DefineLevel()
    {
        int levelPref = PlayerPrefs.GetInt("Level", 0);
        _level = Statics.AllLevels[levelPref];
        // _level = Statics.AllLevels[64];
        PlayerPrefs.SetInt("Level", 72);
        PlayerPrefs.Save();
        
        Debug.Log("Выбран уровень " + _level.Name);
        Debug.Log("Ширина: " + _level.Width);
        Debug.Log("Всего уровней : " + Statics.AllLevels.Count);
        
        _brickSize = BrickUtils.BrickSize(_level.Width);
        _sizeFinishBrick = BrickUtils.BrickSize(9);
    }

    private void InitializedTypes()
    {
        _limitedTypes = Enum.GetValues(typeof(BrickType)).Cast<BrickType>()
            .OrderBy(x => _random.Next())
            .Take(_level.CountTypes)
            .ToList();
        Debug.Log("Кол-во типов : " + _limitedTypes.Count);
    }
    
    /**
     * Инициализация плиток
     */
    private void InitializedBricks()
    {
        if (PlayerPrefs.HasKey("LevelProgress"))
        {
            Debug.Log("Загрузка сохраненого уровня");
            _isStartSavedlevel = true;
            string savedJson = PlayerPrefs.GetString("LevelProgress");
            List<SavedBrick> savedBricks = JsonConvert.DeserializeObject<List<SavedBrick>>(savedJson);
            // Сортировка по слоям и расположению
            savedBricks = savedBricks
                .OrderBy(b => b.TargetWaypoint)
                .ToList();

            for (int i = 0; i < savedBricks.Count; i++)
            {
                InitializeBrick(savedBricks[i]);
            }
            Statics.LevelStart = true;
        }
        else
        {
            Debug.Log("Загрузка нового уровня");
            PlayerPrefs.SetInt("MaxFinishTiles", Statics.MaxFinishTiles);
            PlayerPrefs.Save();
            List<InitialBrick> bricks = GetBricksNewLevel();
            for (int i = 0; i < bricks.Count; i++)
            {
                InitializeBrick(bricks[i], i / -10000f);
            }

            SetGoldenState();
            SetUnknownState();
            SetLiveState();
        }
    }

    /**
     * Получение плиток для старта нового уровня
     */
    private List<InitialBrick> GetBricksNewLevel()
    {
        List<InitialBrick> bricks = _level.Bricks;
        Debug.Log("Кол-во кирпичиков : " + bricks.Count());

        MainUtils.MixList(bricks); // Перемешивание плиток
        
        List<BrickType> types = new List<BrickType>(_limitedTypes);

        if (bricks.Count % 3 != 0)
        {
            throw new ArgumentException("ОШИБКА!!! Кол-во плиток в уровне не кратно 3. " + bricks.Count);
        }
        
        for (int i = 2; i < bricks.Count; i += 3) // Случайное выставление типов
        {
            if (types.Count == 0)
            {
                types = new List<BrickType>(_limitedTypes);
            }
            BrickType type = types[_random.Next(types.Count)];
            types.Remove(type);
            
            bricks[i - 2].Type = type;
            bricks[i - 1].Type = type;
            bricks[i].Type = type;
        }
        
        // Сортировка по слоям и расположению
        bricks = bricks.OrderBy(b => b.Layer)
            .ThenByDescending(b => b.Y)
            .ThenBy(b => b.X)
            .ToList();
        
        return bricks;
    }

    /**
     * Определение "Золотых" кирпичиков
     */
    private void SetGoldenState()
    {
        if (PlayerPrefs.GetInt("Level") < Statics.LevelStartGoldenTiles)
        {
            return;
        }
        
        List<BrickType> goldenTypes = new List<BrickType>();
        List<Brick> bricks = new List<Brick>(Statics.AllBricks);
        MainUtils.MixList(bricks);
        bricks.ForEach(brick => brick.GoldenStateMoves = 0);
        
        foreach (var brick in bricks)
        {
            if (goldenTypes.Count == Statics.MaxGoldenTiles || goldenTypes.Contains(brick.Type))
            {
                return;
            }
            
            int chance = 20 + goldenTypes.Count * 10; // Шанс уменьшается с кол-вом золотых плиток
            bool isGoldenTile = _random.Next(chance) == 0;
            if (isGoldenTile)
            {
                brick.GoldenStateMoves = Statics.CountMovesGoldenState;
                goldenTypes.Add(brick.Type);
            }
        }
    }

    /**
     * Определение "Неизвестных" кирпичиков
     */
    private void SetUnknownState()
    {
        if (PlayerPrefs.GetInt("Level") < Statics.LevelStartUnknownTiles)
        {
            return;
        }
        
        List<BrickType> unknownTypes = new List<BrickType>();
        List<Brick> bricks = new List<Brick>(Statics.AllBricks);
        MainUtils.MixList(bricks);
        bricks.ForEach(brick => brick.IsUnknownTile = false);
        
        int maxUnknown = (int) Math.Round(bricks.Count / 100f * 5); // 5% от общего кол-ва 

        foreach (var brick in bricks)
        {
            if (unknownTypes.Count == maxUnknown)
                return;

            if (brick.IsGolden())
                continue;

            brick.IsUnknownTile = true;
            unknownTypes.Add(brick.Type);
        }
    }
    
    /**
     * Определение "Восполнение жизней" кирпичиков
     */
    private void SetLiveState()
    {
        if (PlayerPrefs.GetInt(LivesPref) >= Statics.MaxLives)
        {
            return;
        }
        
        List<Brick> bricks = new List<Brick>(Statics.AllBricks);
        MainUtils.MixList(bricks);
        bricks.ForEach(brick => brick.LiveStateMoves = 0);
        
        foreach (var brick in bricks)
        {
            if (brick.IsGolden() || brick.IsUnknownTile)
                continue;

            int chance = 50; 
            bool isLiveTile = _random.Next(chance) == 0;
            if (isLiveTile)
            {
                brick.LiveStateMoves = Statics.CountMovesLiveState;
                return;
            }
        }
    }
    
    /**
     * Инициализация кирпичика
     */
    private void InitializeBrick(InitialBrick initialBrick, float z)
    {
        float sizeAdd = _brickSize / 2;
        float xPos = initialBrick.X * sizeAdd;
        float yPos = initialBrick.Y * sizeAdd;
        Vector3 vector3 = new Vector3(xPos, yPos, z); // Z нужен для корректного отображаения спрайтов, иначе они будут накладываться друг на друга
        GameObject brickGameObject = Instantiate(brickPrefab, vector3, Quaternion.identity);
        Brick brick = new Brick(brickGameObject, initialBrick, _brickSize, vector3);
        brickGameObject.transform.localScale = new Vector3(brick.Size, brick.Size, 1);
        brickGameObject.GetComponent<BrickScript>().SetBrick(brick, _sizeFinishBrick);
        brickGameObject.SetActive(false);
    }

    /**
     * Инициализация кирпичика
     */
    private void InitializeBrick(SavedBrick savedBrick)
    {
        Vector3 vector3 = new Vector3(savedBrick.TargetPositionX, savedBrick.TargetPositionY, savedBrick.TargetPositionZ);
        GameObject brickGameObject = Instantiate(brickPrefab, vector3, Quaternion.identity);
        Brick brick = new Brick(brickGameObject, savedBrick);
        brickGameObject.transform.localScale = new Vector3(brick.Size, brick.Size, 1);
        brickGameObject.transform.position =
            new Vector3(savedBrick.PositionX, savedBrick.PositionY, savedBrick.PositionZ);
        brickGameObject.GetComponent<BrickScript>().SetBrick(brick, _sizeFinishBrick);
        brickGameObject.SetActive(false);
        if (savedBrick.LastMoveState != null)
        {
            Statics.LastMoves.Add(brick);
        }
    }

    /**
     * Инициализация места для приземления
     */
    private void InitializeFinishPlace()
    {
        float y = waypointPrefab.transform.position.y;
        GameObject finishPlace = Instantiate(finishPlacePrefab, finishPlacePrefab.transform.position, Quaternion.identity);
        finishPlace.transform.localScale = new Vector3(_sizeFinishBrick, _sizeFinishBrick, 1);
        finishPlace.transform.SetParent(canvas.transform);
        finishPlace.GetComponent<RectTransform>().anchoredPosition = new Vector3(-7, y);
    }

    /**
     * Инициализация монеты
     */
    private IEnumerator InitializeCoins(Brick brick, int countCoins)
    {
        Vector3 initPosition = brick.GameObject.transform.position;
        
        for (int i = 0; i < countCoins; i++)
        {
            GameObject coinGameObject = Instantiate(coinPrefab, initPosition, Quaternion.identity);
            coinGameObject.transform.localScale = new Vector3(0.01f, 0.01f, 3f);
            coinGameObject.GetComponent<CoinScript>().SetCoin(coinsPlace.transform.position, coinsText);
            coinGameObject.GetComponent<SpriteRenderer>().sortingOrder = 10000; // Чтобы было выше Canvas
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator InitializeLiveHeart(Brick brick)
    {
        Vector3 initPosition = brick.GameObject.transform.position;
        GameObject heartGameObject = Instantiate(liveHeartPrefab, initPosition, Quaternion.identity);
        heartGameObject.transform.localScale = new Vector3(0.01f, 0.01f, 3f);
        heartGameObject.GetComponent<LiveHeartScript>().SetLiveHeart(liveHeartPlace.transform.position, livesText);
        heartGameObject.GetComponent<SpriteRenderer>().sortingOrder = 10000; // Чтобы было выше Canvas
        yield return new WaitForSeconds(0.05f);
    }

    /**
     * Инициализация динамического фона
     */
    private void InitializeBackGround()
    {
        // TODO сейчас на фоне только те картинки, что есть на самом уровне
        List<BrickType> types = new List<BrickType>(_limitedTypes);

        _backIcons.ForEach(icon =>
        {
            if (types.Count == 0)
            {
                types = new List<BrickType>(_limitedTypes);
            }
            BrickType type = types[_random.Next(types.Count)];
            Sprite spriteByType = allBackgroundSprites.First(sprite => sprite.name.Equals(type.ToString()));

            icon.sprite = spriteByType;
            types.Remove(type);
        });
    }

    /**
     * Проверка очистки кирпичиков
     */
    private void CheckFinishBricks()
    {
        List<Brick> allFinishBricks = BrickUtils.AllFinishBricks();
        
        if (allFinishBricks.Count >= 3)
        {
            List<List<Brick>> finishBricksByType = allFinishBricks
                .GroupBy(brick => brick.Type)
                .Where(bricks => bricks.Count() >= 3)
                .Select(group => group.ToList())
                .ToList();

            if (finishBricksByType.Count > 0)
            {
                MainUtils.PlaySound(soundCollectThreeTiles);
                finishBricksByType.ForEach(typeBricks => { StartCoroutine(DestroyBricks(typeBricks.Take(3).ToList())); });
                ReduceGoldenStateMoves();
                ReduceLiveStateMoves();
            }
            else
            {
                if (allFinishBricks.Count == PlayerPrefs.GetInt("MaxFinishTiles"))
                {
                    GameOver();
                }
            }
        }
    }

    /**
     * Переход на следующий уровень
     */
    private void CheckNextLevel()
    {
        if (Statics.AllBricks.Count == 0 && !nextLevelPanel.activeSelf)
        {
            int nextlevel = PlayerPrefs.GetInt("Level");
            if (nextlevel >= Statics.AllLevels.Count) // TODO убрать потом
            {
                PlayerPrefs.SetInt("Level", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            }
            backgroundPanel.SetActive(true);
            nextLevelPanel.SetActive(true);
            _backgroundPanelAnim.Play("BackgroundPanelUprise");
            _nextLevelPanelAnim.Play("PanelUprise");
            MainUtils.PlaySound(soundVictory);

            MainUtils.ClearProgress();
        }
    }

    /**
     * Удаление кирпичиков
     */
    private IEnumerator DestroyBricks(List<Brick> bricks)
    {
        bricks.ForEach(brick => brick.IsToDestroy = true);
        bool isAnyGolden = bricks.Any(b => b.IsGolden());
        bool isAnyLive = bricks.Any(b => b.IsLive());
        int countCoins = isAnyGolden ? 10 : 1;
        int countLives = isAnyLive ? 1 : 0;
        PlayerPrefs.SetInt(CoinsPref, PlayerPrefs.GetInt(CoinsPref) + countCoins);
        PlayerPrefs.SetInt(LivesPref, PlayerPrefs.GetInt(LivesPref) + countLives);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(0.1f);
        float timeAnim = 0f;
        bricks.ForEach(brick =>
        {
            if (!brick.GameObject.IsDestroyed())
            {
                BrickScript brickScript = brick.GameObject.GetComponent<BrickScript>();
                if (timeAnim == 0f)
                {
                    timeAnim = brickScript.LengthClearAnim();
                }
                brickScript.PlayClearAnim();
            }
        });
        
        StartCoroutine(InitializeCoins(bricks[1], countCoins));
        if (countLives > 0)
        {
            StartCoroutine(InitializeLiveHeart(bricks[1]));
        }
        
        yield return new WaitForSeconds(timeAnim);
        bricks.ForEach(brick =>
        {
            Destroy(brick.GameObject);
            Statics.AllBricks.Remove(brick);
        });
        BrickUtils.UpdateBricksPosition();
    }

    /**
     * Уменьшение кол-ва ходов "Золотого" состояния кирпичиков
     */
    private void ReduceGoldenStateMoves()
    {
        BrickUtils.AllGoldenTiles().ForEach(brick =>
        {
            brick.GoldenStateMoves--;
        });
    }
    
    /**
     * Уменьшение кол-ва ходов "Восполнение жизней" состояния кирпичиков
     */
    private void ReduceLiveStateMoves()
    {
        BrickUtils.AllLiveTiles().ForEach(brick =>
        {
            brick.LiveStateMoves--;
        });
    }
    
    /**
     *  Конец игры
     */
    private void GameOver()
    {
        if (!losePanel.activeSelf)
        {
            Debug.Log("Конец игры");
            Statics.IsGameOver = true;
            Statics.TimeScale = 1;
            backgroundPanel.SetActive(true);
            losePanel.SetActive(true);
            _backgroundPanelAnim.Play("BackgroundPanelUprise");
            _losePanelAnim.Play("PanelUprise");
            MainUtils.PlaySound(soundGameOver);
            MainUtils.ClearProgress();

            int lives = PlayerPrefs.GetInt(LivesPref);
            resumeAfterLoseByLiveButton.SetActive(lives > 0);
            resumeAfterLoseByAdButton.SetActive(lives <= 0);
        }
    }

    /**
     * Подготовка всех кирпичиков к старту уровня
     */
    private void StartLevel()
    {
        if (_isStartSavedlevel)
        {
            Statics.AllBricks.ForEach(brick =>
            {
                brick.GameObject.SetActive(true);
            });
            BrickUtils.UpdateBricksPosition();
            BrickUtils.UpdateBricksState();
            return;
        }
        
        var groupAllBricks = Statics.AllBricks.GroupBy(brick => brick.Layer)
            .OrderBy(bricksByLayer => bricksByLayer.Key)
            .Select(group => group.ToList())
            .ToList();
        
        // Случайное определение места появления слоев 
        bool isXEven = _random.Next(2) == 1; // (слева или справа)
        bool isYEven = _random.Next(2) == 1; // (сверху или снизу)
        groupAllBricks.ForEach(bricks =>
        {
            bricks.ForEach(brick =>
            {
                var newPosition = new Vector3();
                if (brick.Layer % 2 == 0)
                {
                    newPosition = new Vector3(brick.TargetPosition.x + (isXEven ? 10 : -10), 
                        brick.TargetPosition.y,
                        brick.TargetPosition.z);
                }
                else
                {
                    newPosition = new Vector3(brick.TargetPosition.x, 
                        brick.TargetPosition.y + (isYEven ? 10 : -10),
                        brick.TargetPosition.z);
                }
                
                brick.GameObject.transform.position = newPosition;
                brick.GameObject.GetComponent<SpriteRenderer>().color = Statics.IsNotClickableColor;
                brick.GameObject.SetActive(true);
            });
            // Смена места появления
            if (bricks[0].Layer % 2 == 0)
            {
                isXEven = !isXEven;
                isYEven = !isYEven;
            }
        });
    }

    /**
     * Движение кирпичиков на место при старте уровня
     */
    private void MoveAllWaypointsOnTargetPosition()
    {
        int currentLayer = 0; // Слой, который уже приехал на место
        
        var groupAllBricks = Statics.AllBricks.GroupBy(brick => brick.Layer)
            .OrderBy(bricksByLayer => bricksByLayer.Key)
            .Select(group => group.ToList())
            .ToList();
        
        groupAllBricks.ForEach(bricks =>
        {
            bricks.ForEach(brick =>
            {
                if (brick.Layer <= currentLayer)
                {
                    MainUtils.MoveToWaypoint(brick.TargetPosition, brick.GameObject, 18f);
                }
            });
            
            // Если слой почти приехал на место
            if (bricks.Count(brick => !brick.GameObject.transform.position.Equals(brick.TargetPosition)) <= (bricks.Count / 100 * 30))
            {
                currentLayer++;
                if (_finishedLayerOnStart < currentLayer)
                {
                    _finishedLayerOnStart = currentLayer;
                    if (!soundMoveBricks.isPlaying)
                    {
                        MainUtils.PlaySound(soundMoveBricks);
                    }
                }
            }
        });

        if (Statics.AllBricks.Count(brick => !brick.GameObject.transform.position.Equals(brick.TargetPosition)) == 0)
        {
            Debug.Log("Уровень начат !!!");
            Statics.LevelStart = true;
            BrickUtils.UpdateBricksState();
        }
    }

    private void CheckBrickState()
    {
        if (!Statics.IsGameOver && !BrickUtils.IsSwipingNow() && Statics.TimeScale == 1)
        {
            BrickUtils.UpdateBricksState();
        };
    }

    private void PlayMusicOnAwake()
    {
        if (_music == null)
        {
            musicSound.gameObject.tag = "Music";
            musicSound.Play();
            DontDestroyOnLoad(musicSound);
            _music = musicSound.gameObject;
            Debug.Log("_music == null");
        }
        else
        {
            musicSound = _music.GetComponent<AudioSource>();
        }
    }
    
    private void CheckPlayMusic()
    {
        if (_music != null)
        {
            if (!MainUtils.SettingIsOn(SettingsType.MusicSettings) && musicSound.isPlaying)
            {
                musicSound.Pause();
            }

            if (MainUtils.SettingIsOn(SettingsType.MusicSettings) && !musicSound.isPlaying)
            {
                musicSound.Play();
            }
        }
    }
}
