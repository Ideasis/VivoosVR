using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI _room_name;
    public Button JoinButton;

    public void OnClick_CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom("qwe", options, TypedLobby.Default);
        Debug.Log(_room_name.text);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created",this);
        JoinButton.interactable = true;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Failed "+message, this);
    }
}
