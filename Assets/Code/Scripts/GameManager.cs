using System.Collections.Generic;
using UnityEngine;



public class GameManager : Abstract
{
    // FIELDS
    private List<PlayerController> players;
    private List<CameraController> cameras;
    private List<GridBuilder> grids;
    private Vector3 playersMidpoint;



    // PROPERTIES
    public List<PlayerController> Players
    {
        get { return players; }
    }
    public List<CameraController> Cameras
    {
        get { return cameras; }
    }
    public List<GridBuilder> Grids
    {
        get { return grids; }
    }
    public Vector3 PlayersMidpoint
    {
        get { return playersMidpoint; }
    }




    // OVERRIDES
    override protected void Start()
    {
        base.Start();
        players = new List<PlayerController>();
        cameras = new List<CameraController>();
        grids = new List<GridBuilder>();
    }

    protected override void Update()
    {
        base.Update();
        playersMidpoint = GetPlayersMidpoint();



    }




    // METHODS
    private Vector3 GetPlayersMidpoint()
    {
        Vector3 midpoint = Vector3.zero;
        int count = 0;
        foreach(PlayerController iterator in players)
        {
            midpoint += iterator.transform.position;
            count++;
        }

    

        return midpoint / count;
    }

}
