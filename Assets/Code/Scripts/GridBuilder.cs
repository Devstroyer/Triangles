using System.Collections.Generic;
using UnityEngine;



public class GridBuilder : Abstract
{
    // FIELDS
    public GameObject TrianglePrefab;
    public int Radius;

    private bool[][] map;
    public List<GameObject> triangles;   // made 'public' for debugging. Change to 'private' when you finish :P
    private int maxRadius = 10;


    // PROPERTIES



    // OVERRIDES
    override public void Rebuild()
    {
        base.Rebuild();
        Radius = Mathf.Clamp(Radius, 1, maxRadius);
        DestroyMap();
        DestroyTriangles();
        BuildMap(Radius);
        BuildTriangles(map);
    }



    // METHODS
    private void DestroyMap()
    {
        if (map != null)
        {
            for (int ix = 0; ix < map.Length; ix++)
                map[ix] = null;
            map = null;
        }
    }

    private void DestroyTriangles()
    {
        if (triangles != null)
        {
            foreach (GameObject iterator in triangles)
                DestroyImmediate(iterator);
            triangles = null;
        }
    }

    private void BuildMap(int gridRadius)
    {
        // How big will the array with given gridRadius?
        int horizontalLength = 0;  
        int verticalLength = 0;

        // Initialize the array (didn't test so check for silly errors)
        map = new bool[horizontalLength][];
        for (int ix = 0; ix < map.Length; ix++)
            map[ix] = new bool[verticalLength];

        // Fill out the array properly ----------------------------------------------------------------------------- <
    }

    private void BuildTriangles(bool[][] boolMap)
    {
        triangles = new List<GameObject>();

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

    }


}
