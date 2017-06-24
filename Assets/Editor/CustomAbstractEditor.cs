using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(Abstract), true)]
public class CustomAbstractEditor : Editor
{
    // Calls Abstract's Rebuild() whenever anything changes in the GameObject's inspector
    override public void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed && ((Abstract)target).isActiveAndEnabled)
            ((Abstract)target).Rebuild();

    }

}