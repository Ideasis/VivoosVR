using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationDen : MonoBehaviour
{

    public Transform destination;

    NavMeshAgent navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if(navMeshAgent != null)
        {
            SetDestination();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDestination()
    {
        if(destination != null)
        {
            navMeshAgent.SetDestination(destination.position);            
        }
    }
}
