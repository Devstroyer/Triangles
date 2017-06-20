using System.Collections.Generic;
using UnityEngine;



public class CameraController : Collectible
{
    // FIELDS
    private Camera cameraComponent;



    // PROPERTIES
    public Camera CameraComponent
    {
        get { return cameraComponent;  }
    }



    // OVERRIDES
    override protected void SetTargetCollection()
    {
        targetCollection = GameManager.Cameras;
    }





    // METHODS

}
