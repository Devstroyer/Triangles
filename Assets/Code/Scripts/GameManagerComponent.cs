using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManagerComponent : Abstract
{
    // ---------------------------------------------------------------- FIELDS
    // PUBLIC
    public GameObject GridPrefab;
    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;
    public GameObject CanvasPrefab;
    public GameObject PhaseManagerPrefab;

    // READ-ONLY
    private List<GridComponent> grids;
    public  List<GridComponent> Grids
    { get { return grids; } }

    private List<PlayerComponent> players;
    public  List<PlayerComponent> Players
    { get { return players; } }

    private new CameraComponent camera;
    public      CameraComponent Camera
    { get { return camera; } }

    private CanvasComponent canvas;
    public  CanvasComponent Canvas
    { get { return canvas; } }

    private PhaseManagerComponent phaseManager;
    public  PhaseManagerComponent PhaseManager
    { get { return phaseManager; } }

    // PROPERTIES
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



    // ---------------------------------------------------------------- METHODS
    // MONOBEHAVIOUR
    override protected void Start()
    {
        base.Start();

        players = new List<PlayerComponent>();
        grids = new List<GridComponent>();

        camera = Instantiate(CameraPrefab).GetComponent<CameraComponent>();
        camera.transform.SetParent(this.transform);

        canvas = Instantiate(CanvasPrefab).GetComponent<CanvasComponent>();
        canvas.transform.SetParent(this.transform);

        phaseManager = Instantiate(PhaseManagerPrefab).GetComponent<PhaseManagerComponent>();
        phaseManager.transform.SetParent(this.transform);
    }

    
}
