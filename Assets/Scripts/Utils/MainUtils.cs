using System.Collections;
using System.Collections.Generic;
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
}
