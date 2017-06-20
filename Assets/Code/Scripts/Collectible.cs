using System.Collections.Generic;
using UnityEngine;



public abstract class Collectible : Abstract
{
    // FIELDS
    protected List<GameObject> targetCollection;
    public int index;



    // PROPERTIES
    public int Index
    {
        get { return index; }
    }


    // OVERRIDES
    override protected void Start()
    {
        base.Start();
        SetTargetCollection();
        targetCollection.Add(gameObject);
        index = targetCollection.IndexOf(gameObject);
    }



    // METHODS
    abstract protected void SetTargetCollection();
}
