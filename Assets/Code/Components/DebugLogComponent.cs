using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DebugLogComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    private string debugText;
    public string DebugText
    { get { return debugText; } }



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Update()
    {
        base.Update();
        DisplayDebugText();
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    public void AddLine(string text)
    { debugText += text + '\n'; }

    private void DisplayDebugText()
    {
        GetComponent<Text>().text = debugText;
        debugText = "";
    }



}
