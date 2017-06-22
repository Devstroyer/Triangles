using System.Collections.Generic;
using UnityEngine;



public class TileComponent : Abstract
{
    // FIELDS
    private GridComponent parentGrid;
    
   



    // PROPERTIES
    public GridComponent ParentGrid
    {
        get { return parentGrid; }
    }


    // OVERRIDES
    protected override void Start()
    {
        base.Start();
        parentGrid = transform.parent.gameObject.GetComponent<GridComponent>();
        
    }


    // METHODS
    public TileComponent[] GetNeighbors()
    {
        return parentGrid.GetNeighborsOf(this);
    }

}