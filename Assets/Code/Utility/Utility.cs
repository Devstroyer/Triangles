using System.Collections.Generic;
using UnityEngine;



static public class Utility
{

    static public float PositiveMod(float dividend, float divisor)
    {
        return (dividend % divisor + divisor) % divisor;
    }

    static public float CcwAngleBetween(Vector2 from, Vector2 to)
    {
        float angle = (Mathf.Atan2(to.y, to.x) - Mathf.Atan2(from.y, from.x)) * Mathf.Rad2Deg;
        angle += angle < 0   ?   360   :   0;
        angle %= 360;
        return angle;
    }

    static public void AddMultiple<T>(this List<T> list, T obj, int count)
    {
        for (int i = 0; i < count; i++)
            list.Add(obj);
    }

    static public void LerpTowards(this Vector3 start, Vector3 target, float speed)
    {
        start = Vector3.Lerp(start, target, speed);
    }


}
