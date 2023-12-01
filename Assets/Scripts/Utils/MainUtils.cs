using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Newtonsoft.Json;
using UnityEngine;

public class MainUtils
{
    /**
     * Движение до точки
     * @param точка назначения
     * @param сам объект
     * @param скорость
     */
    public static void MoveToWaypoint(Vector3 to, GameObject gameObject, float speed)
    {
        gameObject.transform.position = Vector3.MoveTowards(
            gameObject.transform.position,
            to,
            Time.deltaTime * speed);
    }

    public static void ChangeSize(Vector3 to, GameObject gameObject, float speed)
    {
        gameObject.transform.localScale = Vector3.MoveTowards(
            gameObject.transform.localScale,
            to,
            Time.deltaTime * speed);
    }

    public static void MixList<T>(IList<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public static void Vibrate()
    {
        Vibration.VibrateAndroid(1); // TODO хз как на IOS будет (есть метод VibrateIOS)
        // Vibration.VibrateIOS(ImpactFeedbackStyle.Soft); // TODO не дает установить на IPhone
    }
    
    /**
     * Расчет скорости
     */
    public static float CountSpeed(Vector3 target, Vector3 brickPosition, float moveSpeed)
    {
        float minSpeed = 3f;
        float distance = Vector3.Distance(target, brickPosition);
        float speed = distance >= 0.1f ? distance / 2 * moveSpeed : moveSpeed / 10f; // Замедление в конце
        return speed > moveSpeed ? moveSpeed : speed < minSpeed ? minSpeed : speed; // Ограничение максимальной и минимальной скорости
    }
    
    /**
    * Сохранение прогресса прохождения уровня
    */
    public static void SaveProgress()
    {
        List<SavedBrick> savedBricks = new List<SavedBrick>();
        Statics.AllBricks.ForEach(brick => savedBricks.Add(new SavedBrick(brick)));
        string savedJson = JsonConvert.SerializeObject(savedBricks);

        PlayerPrefs.SetString("LevelProgress", savedJson);
        PlayerPrefs.Save();
    }

    public static void ClearProgress()
    {
        PlayerPrefs.DeleteKey("LevelProgress");
    }
}
