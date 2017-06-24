using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManagerComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    // Public references to in-editor Prefabs so GameManager knows what to spawn
    public GameObject GridPrefab;
    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;
    public GameObject CanvasPrefab;
    public GameObject PhaseManagerPrefab;

    // Read-only references to core gameplay components
    private List<GridComponent> grids;
    public List<GridComponent> Grids
    { get { return grids; } }

    private List<PlayerComponent> players;
    public List<PlayerComponent> Players
    { get { return players; } }

    private new CameraComponent camera;
    public CameraComponent Camera
    { get { return camera; } }

    private CanvasComponent canvas;
    public CanvasComponent Canvas
    { get { return canvas; } }

    private PhaseManagerComponent phaseManager;
    public PhaseManagerComponent PhaseManager
    { get { return phaseManager; } }



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES
    // Returns the average position of all players
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



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    override protected void Start()
    {
        base.Start();

        // Initialize component collections so later components can collect themselves (duh)
        players = new List<PlayerComponent>();
        grids = new List<GridComponent>();

        // Spawn, save references to and become the parent of some singular components
        camera = Instantiate(CameraPrefab).GetComponent<CameraComponent>();
        camera.transform.SetParent(this.transform);

        canvas = Instantiate(CanvasPrefab).GetComponent<CanvasComponent>();
        canvas.transform.SetParent(this.transform);

        phaseManager = Instantiate(PhaseManagerPrefab).GetComponent<PhaseManagerComponent>();
        phaseManager.transform.SetParent(this.transform);
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS

}
