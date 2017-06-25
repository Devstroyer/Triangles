using System.Collections.Generic;
using UnityEngine;



public struct Action
{
    public Cards A;
    public Cards B;

    public Action(Cards a, Cards b)
    {
        A = a;
        B = b;
    }

    public override string ToString()
    {
        return A.ToString() + " + " + B.ToString();
    }
}