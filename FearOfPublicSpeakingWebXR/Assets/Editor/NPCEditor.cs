using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCGenerator))]
public class NPCEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NPCGenerator npc = target as NPCGenerator;

        if (GUILayout.Button("Create NPC"))
        {
            npc.UpdateNPCs();            
        }

        EditorGUILayout.HelpBox("In order to create NPCs, click generate grid button on Grid Generator script.", MessageType.Warning);
    }
}
