using System.Collections.Generic;
using UnityEngine;



public class CanvasComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    public GameObject DebugLogPrefab = null;

    private DebugLogComponent debugLog;
    public DebugLogComponent DebugLog
    { get { return debugLog; } }



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Start()
    {
        base.Start();
        InitializeDebugLog();
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    private void InitializeDebugLog()
    {
        debugLog = Instantiate(DebugLogPrefab).GetComponent<DebugLogComponent>();
        debugLog.transform.SetParent(transform);
    }


}
