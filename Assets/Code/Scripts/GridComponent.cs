using System.Collections.Generic;
using UnityEngine;



public class GridComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    // Public prefab, so GridComponent knows how Tiles look like
    public GameObject TilePrefab;

    // Public grid radius or in-editor construction
    public int Radius;

    private bool[][] boolMap;    // get rid of ?
    public List<TileComponent> tiles;   // Doesn't work when private because of Rebuild() :O INVESTIGATE!!!
    private int maxDirections;
    private float[] directionAngles;   // Could belong to TileComponent for ULTIMATE customizability
    private float maxNeighborDistance;
    private float maxAngleHalfError;

    private int maxRadius;



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    override public void Rebuild()
    {
        base.Rebuild();

        maxRadius = 16;
        Radius = Mathf.Clamp(Radius, 1, maxRadius);
        
        Clear();
        BuildMap();
        BuildTriangles();
    }

    override protected void Start()
    {
        base.Start();

        // Collect self
        GameManager.Grids.Add(this);

        maxDirections = 3;
        directionAngles = new float[maxDirections + 1];
        directionAngles[(int)Cards.Red] = 30;
        directionAngles[(int)Cards.Green] = 150;
        directionAngles[(int)Cards.Blue] = 270;

        maxNeighborDistance = Mathf.Sqrt(3) / 3 * 1.1f;
        maxAngleHalfError = 15;
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    // Returns a tile from this grid that lies closest to targetPoint
    public TileComponent GetTileClosestTo(Vector3 targetPoint)
    {
        float minimalDistance = float.MaxValue;
        TileComponent closestTile = null;

        foreach(TileComponent iterator in tiles)
        {
            float currentDistance = Vector3.Distance(targetPoint, iterator.transform.position);
            if(currentDistance < minimalDistance)
            {
                minimalDistance = currentDistance;
                closestTile = iterator;
            }
        }

        return closestTile;
    }

    // Returns an array of TileComponents, where [n] is targetTile's neighbor in the n-th direction
    public TileComponent[] GetNeighborsOf(TileComponent targetTile)
    {
        // Initialize a neighbors array with an extra element for None enum value
        TileComponent[] neighbors = new TileComponent[maxDirections + 1];

        // Iterate through all tiles ...
        foreach(TileComponent neighborTile in tiles)
        
            // ... to find those that are within maxNeighborDistance from targetTile ...
            if(neighborTile != targetTile && Vector3.Distance(targetTile.transform.position, neighborTile.transform.position) < maxNeighborDistance)
                        
                // ... then iterate through all directions ...
                for(int i = 1; i <= maxDirections; i++)
                
                    // ... to find whether the neighbor is in any of the predefined directions (relative to targetTile)
                    if(targetTile.GetRelativeAngleTo(neighborTile).GetDistanceTo(directionAngles[i]) < maxAngleHalfError)
                    {
                        neighbors[i] = neighborTile;
                        break;
                    }
                
        return neighbors;
    }
    
    // Clears the grid to a blank state
    private void Clear()
    {
        // Destroy boolMap
        if(boolMap != null)
            for(int ix = 0; ix < boolMap.Length; ix++)
                boolMap[ix] = null;
        boolMap = null;

        // Destroy tiles
        if(tiles != null)
            foreach(TileComponent iterator in tiles)
                DestroyImmediate(iterator.gameObject);
        tiles = null;

        // Destroy remaining children
        foreach(Transform child in transform)
            DestroyImmediate(child.gameObject);
    }


    private void BuildMap()
    {
        // How big will the array with given radius?
        int horizontalLength = (2 * Radius) - 1;
        int verticalLength = 2 * (int)Mathf.Ceil(Radius / 2f);

        // Initialize the array (didn't test so check for silly errors)
        boolMap = new bool[horizontalLength][];
        for(int ix = 0; ix < boolMap.Length; ix++)
            boolMap[ix] = new bool[verticalLength];

        // Fill out the array properly ----------------------------------------------------------------------------- <
        for(int iy = 0; iy < boolMap[0].Length / 2; iy++)
        {
            for(int ix = iy; ix < boolMap.Length - iy; ix++)
            {
                boolMap[ix][(boolMap[0].Length / 2) - 1 - iy] = true;
                boolMap[ix][(boolMap[0].Length / 2) + iy] = true;
            }
        }
        if(Radius % 2 == 1)
        {
            for(int ix = (boolMap[0].Length / 2); ix < boolMap.Length; ix += 2)
            {
                boolMap[ix][0] = false;
                boolMap[ix][boolMap[0].Length - 1] = false;
            }
        }
    }

    private void BuildTriangles()
    {
        tiles = new List<TileComponent>();

        bool rotateFirst = false;
        if(((boolMap[0].Length / 2) - 1) % 2 == 0)
        {
            if((boolMap.Length - 1) / 2 % 2 == 1)
                rotateFirst = true;
        }
        else
        {
            if((boolMap.Length - 1) / 2 % 2 == 0)
                rotateFirst = true;
        }

        float moveXposition = boolMap.Length / 2 * 0.5f;
        float moveYposition = (((boolMap[0].Length) / 2)) * (Mathf.Sqrt(3) / 2) - (Mathf.Sqrt(3) / 3);

        for(int ix = 0; ix < boolMap.Length; ix++)
        {
            for(int iy = 0; iy < boolMap[0].Length; iy++)
            {
                if(boolMap[ix][iy])
                {
                    GameObject newTileComponent = Instantiate(TilePrefab);
                    newTileComponent.transform.SetParent(gameObject.transform, false);
                    newTileComponent.transform.localPosition = new Vector3(ix * 0.5f - moveXposition, -iy * (Mathf.Sqrt(3) / 2) + moveYposition, 0);
                    if(rotateFirst)
                    {
                        if((iy % 2 == 0 && ix % 2 == 0) || (iy % 2 == 1 && ix % 2 == 1))
                        {
                            newTileComponent.transform.rotation = Quaternion.Euler(0, 0, -180);
                            newTileComponent.transform.localPosition = new Vector3(newTileComponent.transform.localPosition.x, newTileComponent.transform.localPosition.y + (Mathf.Sqrt(3) / 6), 0);
                        }
                    }
                    else
                    {
                        if((iy % 2 == 0 && ix % 2 == 1) || (iy % 2 == 1 && ix % 2 == 0))
                        {
                            newTileComponent.transform.rotation = Quaternion.Euler(0, 0, -180);
                            newTileComponent.transform.localPosition = new Vector3(newTileComponent.transform.localPosition.x, newTileComponent.transform.localPosition.y + (Mathf.Sqrt(3) / 6), 0);
                        }
                    }

                    tiles.Add(newTileComponent.GetComponent<TileComponent>());
                }
            }
        }
    }



}
