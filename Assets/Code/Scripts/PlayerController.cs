using System.Collections.Generic;
using UnityEngine;



public class PlayerController : Collectible
{
    // FIELDS
    public GameObject activeGrid;
    public GameObject activeField;


    // PROPERTIES



    // OVERRIDES
    override protected void SetTargetCollection()
    {
        targetCollection = GameManager.Players;
    }

    protected override void Start()
    {
        base.Start();
    }



    // METHODS

}
