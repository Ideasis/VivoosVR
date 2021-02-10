using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Dictionary<int, List<Vector3>> gridPosDic;

    static List<GameObject> NPCs = new List<GameObject>();


    public static void AddToList(GameObject item)
    {
        NPCs.Add(item);
    }

    public static List<GameObject> GetList()
    {
        return NPCs;
    }

    public static void ClearList()
    {
        NPCs.Clear();
    }

    public static void LOL()
    {
        Debug.Log(NPCs.Count);
    }
}
