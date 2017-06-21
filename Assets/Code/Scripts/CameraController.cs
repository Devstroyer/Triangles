using System.Collections.Generic;
using UnityEngine;



public class CameraController : Abstract
{
    // FIELDS
    private Camera cameraComponent;



    // PROPERTIES
    public Camera CameraComponent
    {
        get { return cameraComponent;  }
    }



    // OVERRIDES
    override protected void Start()
    {
        base.Start();
        GameManager.Cameras.Add(this);
    }





    // METHODS

}
