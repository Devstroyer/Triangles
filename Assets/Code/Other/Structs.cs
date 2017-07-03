using System.Collections.Generic;
using UnityEngine;



public struct Order
{
    public Actions A;
    public Directions B;

    public Order(Actions a, Directions b)
    {
        A = a;
        B = b;
    }

    public override string ToString()
    {
        return A.ToString() + " + " + B.ToString();
    }
}