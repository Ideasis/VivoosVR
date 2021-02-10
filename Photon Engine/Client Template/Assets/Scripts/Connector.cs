using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Connector : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public Button JoinButton;

    private void Awake()
    {
        XRSettings.enabled = false;
    }

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.NickName = "ideasis2";
        PhotonNetwork.ConnectUsingSettings();
        
    }
    

    public override void OnConnectedToMaster()
    {
        print("Connected");
        print(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();
        JoinButton.interactable = true;


    }

    public override void OnJoinedLobby()
    {
       
        base.OnJoinedLobby();

        StartCoroutine(Join_Room());
        print("Lobby");
    }
    
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Diconnected" + cause.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Join_Room()
    {
        yield return new WaitForSeconds(2f);
        PhotonNetwork.JoinRoom("qwe");
        print("Room");
    }

}
