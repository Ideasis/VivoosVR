using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connector : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public Button CreateButton;
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.NickName = "ideasis1";
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        print("Connected");
        print(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();
        CreateButton.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Diconnected" + cause.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
