using System;
using System.Collections.Generic;
using UnityEngine;



public class GridController : Collectible
{
    // FIELDS
    public GameObject TrianglePrefab;
    public int Radius;

    private bool[][] map;
    private List<GameObject> triangles;
    private int maxRadius = 16;



    // PROPERTIES



    // OVERRIDES
    override protected void SetTargetCollection()
    {
        targetCollection = GameManager.Grids;
    }

    override public void Rebuild()
    {
        base.Rebuild();
        Radius = Mathf.Clamp(Radius, 1, maxRadius);

        if (map != null)
            DestroyMap();
        if (triangles != null)
            DestroyTriangles();

        BuildMap(Radius);
        BuildTriangles(map);
    }



    // METHODS
    private void DestroyMap()
    {
            for (int ix = 0; ix < map.Length; ix++)
                map[ix] = null;
            map = null;
    }

    private void DestroyTriangles()
    {
            foreach (GameObject iterator in triangles)
                DestroyImmediate(iterator);
            triangles = null;
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
        triangles = new List<GameObject>();
        /*
        // Iterate through boolMap and spawn triangles ------------------------------------------------------------- <
        for(int i=0; i<Radius; i++)   // create number of triangles equal to Radius. For early debugging, you can change it now to proper iteration :)
        {
            GameObject newTriangle = Instantiate(TrianglePrefab);
            newTriangle.transform.parent = gameObject.transform;
            // Set localPosition
            // Set color?
            // Flip vertically?
            triangles.Add(newTriangle);
        }
        */
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
                    GameObject newTriangle = Instantiate(TrianglePrefab);
                    newTriangle.transform.parent = gameObject.transform;
                    newTriangle.transform.localPosition = new Vector3(ix*0.5f-moveXposition, -iy * (Mathf.Sqrt(3) / 2)+moveYposition, 0);
                    if (rotateFirst)
                    {
                        if ((iy % 2 == 0 && ix % 2 == 0) || (iy % 2 == 1 && ix % 2 == 1))
                        {
                            newTriangle.transform.rotation = Quaternion.Euler(0, 0, -180);
                            newTriangle.transform.localPosition = new Vector3(newTriangle.transform.localPosition.x, newTriangle.transform.localPosition.y + (Mathf.Sqrt(3) / 6), 0);
                        }
                    }
                    else
                    {
                        if ((iy % 2 == 0 && ix % 2 == 1) || (iy % 2 == 1 && ix % 2 == 0))
                        {
                            newTriangle.transform.rotation = Quaternion.Euler(0, 0, -180);
                            newTriangle.transform.localPosition = new Vector3(newTriangle.transform.localPosition.x, newTriangle.transform.localPosition.y + (Mathf.Sqrt(3) / 6), 0);
                        }
                    }
                    // Set localPosition
                    // Set color?

                    triangles.Add(newTriangle);
                }
            }
        }

    }



}
