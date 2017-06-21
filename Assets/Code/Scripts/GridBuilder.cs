using System.Collections.Generic;
using UnityEngine;



public class GridBuilder : Abstract
{
    // FIELDS
    public GameObject FieldPrefab;
    public int Radius;

    private bool[][] map;
    public List<Field> fields;
    private int maxRadius = 16;



    // PROPERTIES
    public List<Field> Fields
    {
        get { return fields;  }
    }



    // OVERRIDES
    override protected void Start()
    {
        base.Start();
        GameManager.Grids.Add(this);
    }

    override public void Rebuild()
    {
        base.Rebuild();
        Radius = Mathf.Clamp(Radius, 1, maxRadius);

        if (map != null)
            DestroyMap();
        if (fields != null)
            DestroyTriangles();
        DestroyChildren();

        BuildMap(Radius);
        BuildTriangles(map);
    }



    // METHODS
    public Field GetFieldClosestTo(Vector3 point)
    {
        float minimalDistance = float.MaxValue;
        Field closestField = null;

        foreach (Field iterator in fields)
        {
            float currentDistance = Vector3.Distance(point, iterator.transform.position);
            if (currentDistance < minimalDistance)
            {
                minimalDistance = currentDistance;
                closestField = iterator;
            }
        }

        return closestField;
    }

    private void DestroyMap()
    {
        for (int ix = 0; ix < map.Length; ix++)
            map[ix] = null;
        map = null;
    }

    private void DestroyTriangles()
    {
        foreach (Field iterator in fields)
            DestroyImmediate(iterator.gameObject);
        fields = null;
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
        fields = new List<Field>();

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
                    GameObject newField = Instantiate(FieldPrefab);
                    newField.transform.parent = gameObject.transform;
                    newField.transform.localPosition = new Vector3(ix*0.5f-moveXposition, -iy * (Mathf.Sqrt(3) / 2)+moveYposition, 0);
                    if (rotateFirst)
                    {
                        if ((iy % 2 == 0 && ix % 2 == 0) || (iy % 2 == 1 && ix % 2 == 1))
                        {
                            newField.transform.rotation = Quaternion.Euler(0, 0, -180);
                            newField.transform.localPosition = new Vector3(newField.transform.localPosition.x, newField.transform.localPosition.y + (Mathf.Sqrt(3) / 6), 0);
                        }
                    }
                    else
                    {
                        if ((iy % 2 == 0 && ix % 2 == 1) || (iy % 2 == 1 && ix % 2 == 0))
                        {
                            newField.transform.rotation = Quaternion.Euler(0, 0, -180);
                            newField.transform.localPosition = new Vector3(newField.transform.localPosition.x, newField.transform.localPosition.y + (Mathf.Sqrt(3) / 6), 0);
                        }
                    }
                    // Set localPosition
                    // Set color?

                    fields.Add(newField.GetComponent<Field>());
                }
            }
        }

    }



}
