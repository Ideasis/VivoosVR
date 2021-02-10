using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

// This code is only intend to work in editor(edit Mode) to place the NPCs to the desired position.
// It will deactive itself when the game starts.

//[ExecuteInEditMode]
public class NPCGenerator : MonoBehaviour
{

    public GridGenerator grid;

    public Transform[] prefab;

    public Vector3 positionOffset;

    [Range(0,1.0f)]
    public float npcFrequency;

    public string holderName;

    private List<GameObject> npcList = new List<GameObject>();

    private bool bCanUpdate = true;

    private int[] arrayIndexes;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // This function could be better
    public void GenerateNPC()
    {

        if (grid.GridPosDictionary == null || grid.GridPosDictionary.Count < 1 || !bCanUpdate) return;

        
        int shuffledArrayIndex = 0;       

        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
            npcList.Clear();
        }

        int size = (grid.gridSize.x * grid.gridSize.y);

        Transform holder = new GameObject(holderName).transform;
        holder.parent = transform;        

        int x = 0;
        int y = 0;        

        for (int i = 0; i < size; i++)
        {            
            
            Vector3 pos = grid.GridPosDictionary[x][y];

            //TO DO: Random Sampling.......???
            float rnd = Random.Range(0.0f, 1.0f);

            if (rnd > (1.0f - npcFrequency))
            {
                pos += positionOffset;                

                GameObject npc = Instantiate(prefab[shuffledArrayIndex].gameObject, pos, Quaternion.Euler(0, 180, 0), holder);
                npc.name = "NPC " + x + "_" + y;
                npc.tag = "NPC";

                npc.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);

                //Add necessary components

                // Navmesh
                npc.AddComponent<NavMeshAgent>();
                NavMeshAgent navMeshAgent = npc.GetComponent<NavMeshAgent>();
                navMeshAgent.baseOffset = -0.26f;
                navMeshAgent.radius = 0.25f;
                navMeshAgent.height = 2;
                navMeshAgent.enabled = false;

                // Capsule Collider
                npc.AddComponent<CapsuleCollider>();
                CapsuleCollider capsuleCollider = npc.GetComponent<CapsuleCollider>();
                capsuleCollider.isTrigger = true;
                capsuleCollider.center = new Vector3(0, 0.83f, 0);
                capsuleCollider.radius = 0.26f;
                capsuleCollider.height = 1.85f;

                // NPC AI script
                npc.AddComponent<NPCBehaviour>();
                npc.GetComponent<NPCBehaviour>().Init(x,y);
                
                npcList.Add(npc);              
                
            }

            shuffledArrayIndex = (shuffledArrayIndex + 1) % prefab.Length;

            y = y + 1;
            if (y >= grid.gridSize.y)
            {
                x++;
                y = 0;
                ShuffleArray(ref prefab);
            }
            
        }
        
        bCanUpdate = false;
    }


    void ShuffleArray<T>(ref T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rnd = Random.Range(i, array.Length);

            var tmp = array[i];

            array[i] = array[rnd];
            array[rnd] = tmp;
        }
    }

    // This function is called in NPCEditor script when update NPC button is clicked.
    public void UpdateNPCs()
    {
        bCanUpdate = true;
        GenerateNPC();
    }
    

}
