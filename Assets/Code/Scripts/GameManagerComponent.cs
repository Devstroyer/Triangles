using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManagerComponent : Abstract
{
    // FIELDS
    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;
    public GameObject GridPrefab;
    public GameObject CanvasPrefab;

    private List<PlayerComponent> players;
    private List<CameraComponent> cameras;
    private List<GridComponent> grids;
    private CanvasComponent canvas;

    private Vector3 playersMidpoint;






    // PROPERTIES
    public List<PlayerComponent> Players
    {
        get { return players; }
    }
    public List<CameraComponent> Cameras
    {
        get { return cameras; }
    }
    public List<GridComponent> Grids
    {
        get { return grids; }
    }
    public CanvasComponent Canvas
    {
        get { return canvas; }
    }

    public Vector3 PlayersMidpoint
    {
        get
        {
            Vector3 midpoint = Vector3.zero;
            int count = 0;
            foreach(PlayerComponent iterator in players)
            {
                midpoint += iterator.transform.position;
                count++;
            }

            return midpoint / count; ;
        }
    }




    // OVERRIDES
    override protected void Start()
    {
        base.Start();
        players = new List<PlayerComponent>();
        cameras = new List<CameraComponent>();
        grids = new List<GridComponent>();

        canvas = Instantiate(CanvasPrefab).GetComponent<CanvasComponent>();
        

    }



    // METHODS


}
