using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DebugLogComponent : Abstract
{
    // FIELDS
    private string debugText;




    // PROPERTIES
    public string DebugText
    {
        get { return debugText; }
    }



    // OVERRIDES
    protected override void Update()
    {
        base.Update();
        GetComponent<Text>().text = debugText;
        debugText = "";
    }



    // METHODS
    public void AddLine(string text)
    {
        debugText += text + '\n';
    }


}
