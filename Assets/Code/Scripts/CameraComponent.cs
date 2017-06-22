using System.Collections.Generic;
using UnityEngine;



public class CameraComponent : Abstract
{
    // FIELDS



    // PROPERTIES


        
    // OVERRIDES
    override protected void Start()
    {
        base.Start();
        GameManager.Cameras.Add(this);
    }

    protected override void Update()
    {
        base.Update();
        this.transform.position = Vector3.Lerp(this.transform.position, GameManager.PlayersMidpoint, 0.05f);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -100);
    }





    // METHODS

}
