using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(Abstract), true)]
public class CustomAbstractEditor : Editor
{
    // FIELDS
    private Abstract abstractTarget;



    // METHODS
    private void OnEnable()
    {
        abstractTarget = ((Abstract)target);
    }

    override public void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed && abstractTarget.isActiveAndEnabled)
            abstractTarget.Rebuild();

    }

}