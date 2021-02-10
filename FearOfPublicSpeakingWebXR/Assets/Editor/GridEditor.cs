using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (GridGenerator))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridGenerator grid = target as GridGenerator;

        if (GUILayout.Button("Generate Grid"))
        {
            grid.UpdateGrid();
        }
        

    }
}
