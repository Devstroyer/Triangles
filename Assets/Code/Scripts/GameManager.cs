using System.Collections.Generic;
using UnityEngine;



public class GameManager : Abstract
{
    // FIELDS
    public GameObject PlayerPrefab, CameraPrefab, GridPrefab;

    private List<PlayerController> players;
    private List<CameraController> cameras;
    private List<GridBuilder> grids;



    // PROPERTIES
    public List<PlayerController> Players
    {
        get { return players;  }
    }
    public List<CameraController> Cameras
    {
        get { return cameras; }
    }
    public List<GridBuilder> Grids
    {
        get { return grids; }
    }

    

    // OVERRIDES
    override protected void Start()
    {
        base.Start();
        players = new List<PlayerController>();
        cameras = new List<CameraController>();
        grids = new List<GridBuilder>();
        
    }




    // METHODS

}
