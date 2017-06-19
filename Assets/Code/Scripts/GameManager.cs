using System.Collections.Generic;
using UnityEngine;



public class GameManager : Abstract
{
    // FIELDS
    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;

    private List<GameObject> players;
    private List<GameObject> cameras;



    // PROPERTIES
    public List<GameObject> Players
    {
        get { return players;  }
    }
    public List<GameObject> Cameras
    {
        get { return cameras; }
    }



    // OVERRIDES
    override protected void Start()
    {
        base.Start();
        players = new List<GameObject>();
        cameras = new List<GameObject>();
        players.Add(Instantiate(PlayerPrefab));
        cameras.Add(Instantiate(CameraPrefab));
    }




    // METHODS

}
