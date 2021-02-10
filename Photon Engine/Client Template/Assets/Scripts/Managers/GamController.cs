using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GamController : MonoBehaviour
{
    public Text test;
    float targetTime = 1;
    GameObject player1;
    GameObject player2;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
        if (!PhotonNetwork.IsConnected) // 1
        {
            
            return;
        }

        if (PlayerManager.LocalPlayerInstance == null)
        {
            
            if (PhotonNetwork.IsMasterClient) // 2
            {
                
            }
            else // 5
            {
                Debug.Log("Instantiating Player 1");
                // 3
                player1 = PhotonNetwork.Instantiate("PlayerGO",
                    spawnPoint.transform.position,
                    spawnPoint.transform.rotation, 0);
                XRSettings.enabled = true;
                
            }
        }
    }

    void Update()
    {
        
    }

    
}
