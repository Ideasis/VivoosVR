using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI _room_name;

    public void OnClick_CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom(_room_name.text, options, TypedLobby.Default);
        
    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(_room_name.text);

    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created",this);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Failed "+message, this);
    }
}
