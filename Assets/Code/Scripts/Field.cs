using System.Collections.Generic;
using UnityEngine;



public class Field : Abstract
{
    // FIELDS
    private GridBuilder grid;
    private float maxNeighborDistance;
    private float[] directionAngles;



    // PROPERTIES
    public GridBuilder Grid
    {
        get { return grid; }
    }


    // OVERRIDES
    protected override void Start()
    {
        base.Start();
        grid = transform.parent.gameObject.GetComponent<GridBuilder>();
        maxNeighborDistance = Mathf.Sqrt(3) / 3 * 1.1f;
        directionAngles = new float[4];
        directionAngles[(int)Directions.Red] = 30;
        directionAngles[(int)Directions.Green] = 150;
        directionAngles[(int)Directions.Blue] = 270;
    }


    // METHODS
    public Field[] GetNeighbors()
    {
        Field[] neighbors = new Field[4];

        
        foreach (Field iterator in grid.Fields)
            if (iterator != this && Vector3.Distance(this.transform.position, iterator.transform.position) < maxNeighborDistance)   // neighbor found
            {   
                Vector2 lookAtNeighbor = iterator.transform.position - this.transform.position;   // vector between this field and found neighbor
                for (int i=1; i<directionAngles.Length; i++)
                {
                    float modifiedAngle = Utility.PositiveMod(Utility.CcwAngleBetween(Vector2.right, lookAtNeighbor) - this.transform.eulerAngles.z, 360);
                    if (Mathf.Abs(modifiedAngle - directionAngles[i]) < 15)   // if angle to the found neighbour resembles any direction
                    {
                        neighbors[i] = iterator;
                        break;
                    }
                }
            }
        

        return neighbors;
    }

}