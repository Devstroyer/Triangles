using System.Collections.Generic;
using UnityEngine;



static public class Utility
{
    // -------------------------------------------------------------------------------------------------------------------------------- GetRelativeAngle (Abstract)
    // Method
    static public float GetRelativeAngleBetween(Abstract a, Abstract b)
    {
        float angle = Utility.CcwAngleBetween(Vector2.right, b.transform.position - a.transform.position);
        angle -= Utility.PosMod(a.transform.eulerAngles.z, 360);

        return Utility.PosMod(angle, 360);
    }

    // Abstract Extension
    static public float GetRelativeAngleTo(this Abstract a, Abstract b)
    {
        return Utility.GetRelativeAngleBetween(a, b);
    }



    // -------------------------------------------------------------------------------------------------------------------------------- GetDistance (float)
    // Method
    static public float GetDistanceBetween(float a, float b)
    {
        return Mathf.Abs(a - b);
    }

    // float Extentsion
    static public float GetDistanceTo(this float a, float b)
    {
        return GetDistanceBetween(a, b);
    }

    // -------------------------------------------------------------------------------------------------------------------------------- GetNearestMultiple (float)
    // Method
    static public float GetNearestMultipleOf(float a, float multiple)
    {
        return Mathf.Round(a/multiple) * multiple;
    }

    // float Extentsion
    static public float GetNearestMultiple(this float a, float multiple)
    {
        return GetNearestMultipleOf(a, multiple);
    }



    // -------------------------------------------------------------------------------------------------------------------------------- Various
    static public float PosMod(float dividend, float divisor)
    {
        return (dividend % divisor + divisor) % divisor;
    }

    static public float CcwAngleBetween(Vector2 from, Vector2 to)
    {
        float angle = (Mathf.Atan2(to.y, to.x) - Mathf.Atan2(from.y, from.x)) * Mathf.Rad2Deg;
        return PosMod(angle, 360);
    }

    static public void LerpTowards(this Vector3 start, Vector3 target, float speed)
    {
        start = Vector3.Lerp(start, target, speed);
    }

    static public void AddMultiple<T>(this List<T> list, T obj, int count)
    {
        for(int i = 0; i < count; i++)
            list.Add(obj);
    }


}
