using System.Collections.Generic;
using UnityEngine;



public class LookAtCamera : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Update()
    {
        base.Update();

        this.transform.rotation = GameManager.Camera.transform.rotation;

        foreach(SpriteRenderer sprite in GetComponents<SpriteRenderer>())
            sprite.sortingOrder = (int)(-100 * Vector3.Distance(GameManager.Camera.transform.position, this.transform.position));
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS

}



