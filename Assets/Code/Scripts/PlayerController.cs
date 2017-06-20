using System.Collections.Generic;
using UnityEngine;



public class PlayerController : Collectible
{
    // FIELDS



    // PROPERTIES



    // OVERRIDES
    override protected void SetTargetCollection()
    {
        targetCollection = GameManager.Players;
    }



    // METHODS

}
