using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{

    [HideInInspector]
    public int rowID;
    [HideInInspector]
    public int colID;

    NavMeshAgent navMeshAgent;

    Animator anim;    

    Vector3 destination;
    
    Transform lookTarget;

    bool bIsBored = false;
    bool bIsWalking = false;
    bool bCanApplause = false;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        GameMaster.rndWalkEvent += StartWalking;
        GameMaster.boredEvent += StartBored;
        GameMaster.applauseEvent += ToggleNPCApplause;

        navMeshAgent.updateRotation = false;        

        RandomSitAnimation();

        GetComponent<LODGroup>().RecalculateBounds();
    }   
   
    public void Init(int rowId, int colId)
    {
        rowID = rowId;
        colID = colId;
    }

    private void LateUpdate()
    {
        if (bIsWalking && navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
        }

        if (lookTarget == null)
        {
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCamera != null)
            lookTarget = mainCamera.transform;
        }
    }

    void StartWalking(int val, Vector3 dest)
    {
        if(val == rowID && !bIsBored && !bIsWalking)
        {  
            const int chanceToWalk = 50;

            int rnd = Random.Range(0, 100);
            if(rnd > chanceToWalk)
            {
                destination = dest;               
                StartCoroutine(MoveToDestination());
            }
        }
    }

    IEnumerator MoveToDestination()
    {
        float rndDelay = Random.Range(0, 3f);

        yield return new WaitForSeconds(rndDelay);
        
        SetDestination();
    }

    void SetDestination()
    {   
        if(destination != null)
        {
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(destination);
            

            bIsWalking = true;
            anim.SetBool("bCanWalk", bIsWalking);
        }
    }

    Vector3 FindClosestPointToNavmesh()
    {
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(transform.position, out navMeshHit, 1, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }

        return Vector3.zero;
    }

    void StartBored(int row, int col)
    {
        if (row == rowID && col == colID && !bIsBored && !bIsWalking)
        {
            bIsBored = true;
            anim.SetBool("bIsBored", bIsBored);
        }
    }

    void ToggleNPCApplause(bool inVal)
    {

        if (inVal)
        {
            StartCoroutine(RandomApplauseStart());
        }
        else
        {
            bCanApplause = !bCanApplause;

            anim.SetBool("bCanClap", bCanApplause);
        }        
    }

    IEnumerator RandomApplauseStart()
    {
        float rndWait = Random.Range(0.0f,1.0f);

        yield return new WaitForSeconds(rndWait);

        bCanApplause = !bCanApplause;
        anim.SetBool("bCanClap", bCanApplause);

        RandomSitAnimation();
    }

    // This function is for setting the head/any bone rotation to desired rotation.
    // For this function to called by the engine : the rig needs to be humanoid and IK Pass have to be enable to in animator
    // This function runs like update function.
    private void OnAnimatorIK(int layerIndex)
    {
        if (lookTarget != null && !bIsWalking && !bIsBored)
        {
            anim.SetLookAtWeight(1.0f, 0.0f, 1.0f, 0.0f, 0.5f);
            anim.SetLookAtPosition(lookTarget.position);
            
        }
        else
        {
            anim.SetLookAtWeight(0.0f);            
        }        
    }

    public void DisableNPC()
    {
        GameMaster.rndWalkEvent -= StartWalking;
        GameMaster.boredEvent -= StartBored;
        GameMaster.applauseEvent -= ToggleNPCApplause;
        this.enabled = false;
        navMeshAgent.enabled = false;
        gameObject.SetActive(false);
    }


    void RandomSitAnimation()
    {
        int rnd = Random.Range(0, 2); // Random number between 0-1

        anim.SetFloat("BlendSit", rnd);
    }
}
