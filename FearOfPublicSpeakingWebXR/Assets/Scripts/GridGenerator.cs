using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[ExecuteInEditMode]
public class GridGenerator : MonoBehaviour
{
    public Transform prefab;
    public Vector2Int gridSize;
    public Vector3 offset;
    //public Vector2 positionOffset;

    public string holderName;

    private bool bCanUpdate = true;

    public Dictionary<int, List<Vector3>> GridPosDictionary { get => gridPosDictionary; private set => gridPosDictionary = value; }
    private Dictionary<int, List<Vector3>> gridPosDictionary = new Dictionary<int, List<Vector3>>();

    MeshFilter[] meshFilters;

    [SerializeField]
    Material desiredMaterial;  

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    public void GenerateGrid()
    {
        //gridPosDictionary = new Dictionary<int, List<Vector3>>();
        if (!bCanUpdate) return;

        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
            GridPosDictionary.Clear();
        }

        Transform holder = new GameObject(holderName).transform;
        holder.parent = transform;
        holder.gameObject.AddComponent<MeshFilter>();
        holder.gameObject.AddComponent<MeshRenderer>();

        for (int y = 0; y < gridSize.x; y++)
        {

            List<Vector3> tilePositions = new List<Vector3>();
            for (int x = 0; x < gridSize.y; x++)
            {
                float fRnd = Random.Range(0, 2);
                //Vector3 tilePosition = new Vector3(-gridSize.x / 2 + 0.5f + x + offset.x, 0, -gridSize.y / 2 + 0.5f + y + offset.y);
                Vector3 tilePos = new Vector3((transform.position.x) + (offset.x * x), transform.position.y + offset.z, (transform.position.z ) + (y * offset.y));
                Transform newTile = Instantiate(prefab, tilePos, Quaternion.Euler(0, 180, 0)) as Transform;
                newTile.parent = holder;                
                //newTile.name = "X:" + x + " Y:" + y;

                tilePositions.Add(tilePos);                

            }

            GridPosDictionary.Add(y,tilePositions);
        }

        CombineMeshes(holder.gameObject);
        WipeChilds(holder);

        bCanUpdate = false;
    }

    public void UpdateGrid()
    {
        bCanUpdate = true;
        GenerateGrid();
    }


    void CombineMeshes(GameObject holderGO)
    {
        meshFilters = holderGO.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combineInstances = new CombineInstance[meshFilters.Length];        
        
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combineInstances[i].mesh = meshFilters[i].sharedMesh;
            combineInstances[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            
        }

        MeshFilter meshFilter = holderGO.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.sharedMesh.CombineMeshes(combineInstances);
        
        holderGO.GetComponent<MeshRenderer>().sharedMaterial = new Material(desiredMaterial);

        holderGO.SetActive(true);
    }

    void WipeChilds(Transform holder)
    {
        foreach (Transform child in holder)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
