using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator anim;

    int overlappedCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            anim.SetBool("bCanOpenDoors", true);
            overlappedCnt++;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "NPC")
        {
            overlappedCnt--;
            other.GetComponent<NPCBehaviour>().DisableNPC();
            if (overlappedCnt == 0)
            {
                anim.SetBool("bCanOpenDoors", false);
            }
        }
        
    }
}
