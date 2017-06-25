using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManagerComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    // Public references to in-editor Prefabs so GameManager knows what to spawn
    public GameObject GameSettingsPrefab = null;
    public GameObject GridPrefab = null;
    public GameObject PlayerPrefab = null;
    public GameObject CameraPrefab = null;
    public GameObject CanvasPrefab = null;
    public GameObject PhaseManagerPrefab = null;

    // Read-only references to core gameplay components
    private GameSettingsComponent gameSettings;
    public GameSettingsComponent GameSettings
    { get { return gameSettings; } }

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
            foreach(PlayerComponent iterator in players.FindAll(o => o.isActiveAndEnabled))
            {
                midpoint += iterator.transform.position;
                count++;
            }

            return midpoint / Mathf.Max(1, count);
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
        gameSettings = Instantiate(GameSettingsPrefab).GetComponent<GameSettingsComponent>();
        gameSettings.transform.SetParent(this.transform);

        phaseManager = Instantiate(PhaseManagerPrefab).GetComponent<PhaseManagerComponent>();
        phaseManager.transform.SetParent(this.transform);

        canvas = Instantiate(CanvasPrefab).GetComponent<CanvasComponent>();
        canvas.transform.SetParent(this.transform);

        camera = Instantiate(CameraPrefab).GetComponent<CameraComponent>();
        camera.transform.SetParent(this.transform);
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS

}
