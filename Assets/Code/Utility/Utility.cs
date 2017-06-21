using System.Collections.Generic;
using UnityEngine;



static public class Utility
{

    static public float CcwAngleBetween(Vector2 from, Vector2 to)
    {
        float angle = (Mathf.Atan2(to.y, to.x) - Mathf.Atan2(from.y, from.x)) * Mathf.Rad2Deg;
        angle += angle < 0   ?   360   :   0;
        angle %= 360;
        return angle;
    }



    // float extension
    static public bool NearlyEquals(this float a, float b, float tolerance)
    {
        float lowerBound = b * (1.0f - tolerance);
        float upperBound = b * (1.0f + tolerance);
        return (a >= lowerBound && a <= upperBound);
    }

    static public void AddMultiple<T>(this List<T> list, T obj, int count)
    {
        for (int i = 0; i < count; i++)
            list.Add(obj);
    }



}
