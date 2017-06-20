using System.Collections.Generic;
using UnityEngine;



public class GameManager : Abstract
{
    // FIELDS
    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;
    public GameObject GridPrefab;

    private List<GameObject> players;
    private List<GameObject> cameras;
    private List<GameObject> grids;



    // PROPERTIES
    public List<GameObject> Players
    {
        get { return players;  }
    }
    public List<GameObject> Cameras
    {
        get { return cameras; }
    }
    public List<GameObject> Grids
    {
        get { return grids; }
    }

    

    // OVERRIDES
    override protected void Awake()
    {
        base.Awake();
        players = new List<GameObject>();
        cameras = new List<GameObject>();
        grids = new List<GameObject>();
        
    }




    // METHODS

}
