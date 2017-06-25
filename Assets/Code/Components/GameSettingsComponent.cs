using System.Collections.Generic;
using UnityEngine;



public class GameSettingsComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    public float[] DirectionAngles = new float[4] {0, 30, 150, 270};
    public float MaxNeighborDistance = Mathf.Sqrt(3) / 3 * 1.1f;
    public float MaxAngleHalfError = 15;
    
    public int MaxActions = 3;

    public int AbsoluteActionsLimit = 5;

    public Phases StartingGamePhase = Phases.Queue;



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES



    // -------------------------------------------------------------------------------------------------------------------------------- MONO



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS



}
