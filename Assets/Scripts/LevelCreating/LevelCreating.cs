using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

namespace LevelCreating
{
    public class LevelCreating : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

        private String _method = null;
    
        private List<Level> _levels = new List<Level>();
        private int _level = 0;
        private int _lastLevel = -1;
    
        void Start()
        { 
            LogLevelFromScene();
            CheckAllLevelsForDouble();
            // InitializeField(null);
            // CheckAllLevelsForDouble();
            // _levels = SortLevelsByComplexity();
        }

        private void Update()
        {
            if ("SortLevelsByComplexity".Equals(_method))
            {
                if (_level != _lastLevel)
                {
                    StartCoroutine(InitLevel(_levels[_level]));
                }
            }
        }

        private IEnumerator InitLevel(Level level)
        {
            _lastLevel = _level;
            yield return new WaitForSeconds(3f);
            InitializeField(level);
            _level++;
        }

        /**
     * Выводит в консоль созданный уровень.
     * Весь уровень берет со сцены.
     */
        public void LogLevelFromScene()
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("BrickEmpty");
            LogLevel(bricks);
        }

        /**
     * Инициализирует уровень на сцене.
     */
        private void InitializeField(Level level)
        {
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("BrickEmpty")) {
                Destroy(obj);
            }

            List<InitialBrick> bricks = level == null ? Levels.Level1 : level.Bricks;
        
            if (bricks.Count % 3 != 0)
            {
                Debug.Log("ОШИБКА!!! Кол-во плиток в уровне не кратно 3. " + bricks.Count);
            }
        
            String result = null;
            foreach (var brick in bricks)
            {
                string xPos = brick.X.ToString().Replace(",", ".") + "f";
                string yPos = brick.Y.ToString().Replace(",", ".") + "f";
            
                result += "new InitialBrick(" + xPos + ", " + yPos + ", " + brick.Layer + "),\n";
                Vector3 vector3 = new Vector3(brick.X, brick.Y, 0);
                GameObject brickGameObject = Instantiate(prefab, vector3, Quaternion.identity);
            
                brickGameObject.GetComponent<SpriteRenderer>().sortingOrder = brick.Layer;
            }
        
            Debug.Log(result);
        }

        /**
     * Проверка уровня на дубликаты
     */
        private void CheckAllLevelsForDouble()
        {
            int i = 0;
            Statics.AllLevels.ForEach(level =>
            {
                i++;
                List<InitialBrick> bricks = level.Bricks.GroupBy(brick => new
                    {
                        brick.X,
                        brick.Y,
                        brick.Layer
                    })
                    .Where(g => g.Count() > 1)
                    .Select(g => new InitialBrick(g.Key.X, g.Key.Y, g.Key.Layer))
                    .ToList();
                if (bricks.Count() > 0)
                {
                    Debug.Log("В уровне " + level.Name + " есть дубликаты: ");
                    Debug.Log(bricks[0].X + " " + bricks[0].Y + " " + bricks[0].Layer);
                }
            });
        }

        /**
         * Сортировка уровней по сложности
         */
        private List<Level> SortLevelsByComplexity()
        {
            _method = "SortLevelsByComplexity";
        
            List<Level> levels = Statics.AllLevels;
            // Установка сложность каждого уровня
            foreach (var level in levels)
            {
                int layers = level.Bricks.GroupBy(l => l.Layer).Count();
                int complexity = (level.Bricks.Count / 5) * (level.CountTypes / 3) * layers;
                level.Complexity = complexity;
            }
        
            // Сортировка по сложности
            levels = levels.OrderBy(l => l.Complexity).ToList();

            // Корректировка кривой сложности
            for (int i = 0; i < levels.Count - 5; i += 5)
            {
                (levels[i + 3], levels[i + 5]) = (levels[i + 5], levels[i + 3]);
                (levels[i + 4], levels[i + 6]) = (levels[i + 4], levels[i + 6]);
            }
        
            String result = null;
            String difficultyCurve = null;
        
            // Вывод результата
            foreach (var level in levels)
            {
                result += "new Level(" + "\"" + level.Name + "\", "+ "Levels." + level.Name + ", " + level.CountTypes + ", " + level.Complexity + "),\n";
                difficultyCurve += level.Complexity + "\n ";
            }
            Debug.Log(result);
        
            Debug.Log("Кривая сложности " + difficultyCurve);
        
            return levels;
        }

        private void LogLevel(GameObject[] bricks)
        {
            Debug.Log("Всего: " + bricks.Length);
            if (bricks.Length % 3 != 0)
            {
                throw new ArgumentException("ОШИБКА!!! Кол-во плиток в уровне не кратно 3. " + bricks.Length);
            }
        
            String result = null;
            foreach (var brick in bricks)
            {
                float x = brick.transform.position.x;
                float y = brick.transform.position.y;
                string xPos = x.ToString().Replace(",", ".") + "f";
                string yPos = y.ToString().Replace(",", ".") + "f";
            
                result += "new InitialBrick(" + xPos + ", " +  yPos + ", " + brick.GetComponent<SpriteRenderer>().sortingOrder + "),\n";
            }
        
            Debug.Log(result);
        }
    }
}
