using System.Collections.Generic;
using UnityEngine;



public class CanvasComponent : Abstract
{
    // FIELDS
    public GameObject DebugLogPrefab;

    private DebugLogComponent debugLog;



    // PROPERTIES
    public DebugLogComponent DebugLog
    {
        get { return debugLog; }
    }



    // OVERRIDES
    protected override void Start()
    {
        base.Start();
        debugLog = Instantiate(DebugLogPrefab).GetComponent<DebugLogComponent>();
        debugLog.transform.SetParent(transform);
    }


    // METHODS

}
