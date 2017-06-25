using System.Collections.Generic;
using UnityEngine;



public class CameraComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    public GameObject BoomPrefab = null;

    private BoomComponent boom;
    public BoomComponent Boom
    { get { return boom; } }


    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Start()
    {
        base.Start();
        InitializeBoom();
    }

    protected override void Update()
    {
        base.Update();
        boom.LerpPositionTowards(GameManager.PlayersMidpoint, 0.05f);
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    private void InitializeBoom()
    {
        boom = Instantiate(BoomPrefab).GetComponent<BoomComponent>();
        boom.transform.SetParent(transform.parent);
        transform.SetParent(boom.transform);
    }
}
