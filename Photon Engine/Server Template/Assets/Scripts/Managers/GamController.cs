using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamController : MonoBehaviourPunCallbacks
{

    GameObject player1;
    GameObject player2;
    public GameObject spawnPoint;
    public float targetTime = 5.0f;
    private GameMasterPhoton gameMaster;  

    
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
                player2 = PhotonNetwork.Instantiate("Server",
                     spawnPoint.transform.position,
                     spawnPoint.transform.rotation, 0);
            }
            else // 5
            {
                
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            //CheckCam();
            targetTime = 5f;
        }
    }

    private void CheckCam()
    {
        Camera[] cameralist = GameObject.FindObjectsOfType<Camera>();

        foreach (Camera camera in cameralist)
        {
            
            if (camera.gameObject.name != "Camera")
            {
                camera.gameObject.SetActive(false);
            }
            else
            {
                if(!camera.enabled)
                {
                    camera.enabled = true;
                }
            }
        }
    }
}
