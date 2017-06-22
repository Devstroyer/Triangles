using System.Collections.Generic;
using UnityEngine;



public class GridComponent : Abstract
{
    // FIELDS
    public GameObject TilePrefab;
    public List<TileComponent> tiles;

    public int Radius;

    private bool[][] map;
    
    private int maxRadius = 16;

    private float maxNeighborDistance;
    private float[] directionAngles;



    // PROPERTIES
    public List<TileComponent> TileComponents
    {
        get { return tiles;  }
    }



    // OVERRIDES
    override protected void Start()
    {
        base.Start();
        GameManager.Grids.Add(this);
        InitializeSettings();

    }

    override public void Rebuild()
    {
        base.Rebuild();
        Radius = Mathf.Clamp(Radius, 1, maxRadius);

        if (map != null)
            DestroyMap();
        if (tiles != null)
            DestroyTriangles();
        DestroyChildren();

        BuildMap(Radius);
        BuildTriangles(map);
    }



    // METHODS
    public void InitializeSettings()
    {
        maxNeighborDistance = Mathf.Sqrt(3) / 3 * 1.1f;
        directionAngles = new float[4];
        directionAngles[(int)Directions.Red] = 30;
        directionAngles[(int)Directions.Green] = 150;
        directionAngles[(int)Directions.Blue] = 270;
    }

    public TileComponent GetTileClosestTo(Vector3 point)
    {
        float minimalDistance = float.MaxValue;
        TileComponent closestTile = null;

        foreach (TileComponent iterator in tiles)
        {
            float currentDistance = Vector3.Distance(point, iterator.transform.position);
            if (currentDistance < minimalDistance)
            {
                minimalDistance = currentDistance;
                closestTile = iterator;
            }
        }

        return closestTile;
    }

    public TileComponent[] GetNeighborsOf(TileComponent tile)
    {
        TileComponent[] neighbors = new TileComponent[4];

        
        foreach (TileComponent iterator in tiles)
            if (iterator != tile && Vector3.Distance(tile.transform.position, iterator.transform.position) < maxNeighborDistance)   // neighbor found
            {   
                Vector2 lookAtNeighbor = iterator.transform.position - tile.transform.position;   // vector between this field and found neighbor
                for (int i=1; i<directionAngles.Length; i++)
                {
                    float modifiedAngle = Utility.PositiveMod(Utility.CcwAngleBetween(Vector2.right, lookAtNeighbor) - tile.transform.eulerAngles.z, 360);
                    if (Mathf.Abs(modifiedAngle - directionAngles[i]) < 15)   // if angle to the found neighbour resembles any direction
                    {
                        neighbors[i] = iterator;
                        break;
                    }
                }
            }
        

        return neighbors;
    }
    private void DestroyMap()
    {
        for (int ix = 0; ix < map.Length; ix++)
            map[ix] = null;
        map = null;
    }

    private void DestroyTriangles()
    {
        foreach (TileComponent iterator in tiles)
            DestroyImmediate(iterator.gameObject);
        tiles = null;
    }

    private void DestroyChildren()
    {
        foreach (Transform child in transform)
            DestroyImmediate(child.gameObject);
    }

    private void BuildMap(int gridRadius)
    {
        // How big will the array with given gridRadius?
        int horizontalLength = (2 * gridRadius) - 1;
        int verticalLength = 2 * (int) Mathf.Ceil(gridRadius / 2f);

        // Initialize the array (didn't test so check for silly errors)
        map = new bool[horizontalLength][];
        for (int ix = 0; ix < map.Length; ix++)
            map[ix] = new bool[verticalLength];

        // Fill out the array properly ----------------------------------------------------------------------------- <
        for (int iy = 0; iy < map[0].Length/2; iy++)
        {
            for( int ix = iy; ix < map.Length-iy; ix++)
            {
                map[ix][(map[0].Length / 2) - 1 - iy] = true;
                map[ix][(map[0].Length / 2) + iy] = true;
            }
        }
        if (gridRadius % 2 == 1)
        {
            for (int ix = (map[0].Length/2);ix<map.Length; ix+=2)
            {

                map[ix][0] = false;
                map[ix][map[0].Length - 1] = false;
            }
        }
    }

    private void BuildTriangles(bool[][] boolMap)
    {
        tiles = new List<TileComponent>();

        bool rotateFirst = false;
        if (((map[0].Length / 2) - 1) % 2 == 0)
        {
            if ((map.Length - 1) / 2 % 2 == 1)
                rotateFirst = true;
        }
        else
        {
            if ((map.Length - 1) / 2 % 2 == 0)
                rotateFirst = true;
        }
        float moveXposition= map.Length / 2 *0.5f;
        float moveYposition = (((map[0].Length)/ 2)) * (Mathf.Sqrt(3) / 2) - (Mathf.Sqrt(3) / 3);
        for (int ix = 0; ix < map.Length; ix++)
        {
            for(int iy =0; iy < map[0].Length; iy++)
            {
                if (map[ix][iy])
                {
                    GameObject newTileComponent = Instantiate(TilePrefab);
                    newTileComponent.transform.parent = gameObject.transform;
                    newTileComponent.transform.localPosition = new Vector3(ix*0.5f-moveXposition, -iy * (Mathf.Sqrt(3) / 2)+moveYposition, 0);
                    if (rotateFirst)
                    {
                        if ((iy % 2 == 0 && ix % 2 == 0) || (iy % 2 == 1 && ix % 2 == 1))
                        {
                            newTileComponent.transform.rotation = Quaternion.Euler(0, 0, -180);
                            newTileComponent.transform.localPosition = new Vector3(newTileComponent.transform.localPosition.x, newTileComponent.transform.localPosition.y + (Mathf.Sqrt(3) / 6), 0);
                        }
                    }
                    else
                    {
                        if ((iy % 2 == 0 && ix % 2 == 1) || (iy % 2 == 1 && ix % 2 == 0))
                        {
                            newTileComponent.transform.rotation = Quaternion.Euler(0, 0, -180);
                            newTileComponent.transform.localPosition = new Vector3(newTileComponent.transform.localPosition.x, newTileComponent.transform.localPosition.y + (Mathf.Sqrt(3) / 6), 0);
                        }
                    }
                    // Set localPosition
                    // Set color?

                    tiles.Add(newTileComponent.GetComponent<TileComponent>());
                }
            }
        }

    }



}
