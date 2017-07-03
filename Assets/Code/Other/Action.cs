using System.Collections.Generic;
using UnityEngine;



public class Action : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    Actions preset;
    Sprite symbol;



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES




    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Start()
    {
        base.Start();

        if(preset != Actions.None)
            symbol = GS.LoadCardSprite(preset.ToString());
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    public Action(Actions actionPreset = Actions.None)
    {
        preset = actionPreset;
    }

}