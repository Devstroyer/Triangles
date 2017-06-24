using System.Collections.Generic;
using UnityEngine;



public class CameraComponent : Abstract
{
    // FIELDS



    // PROPERTIES


        
    // OVERRIDES
    protected override void Update()
    {
        base.Update();
        LerpPositionTowards(GameManager.PlayersMidpoint, 0.05f);
    }





    // METHODS

}
