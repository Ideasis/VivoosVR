using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public RoomInfo _roomInfo {get; private set;}
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
        text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }

    public void OnClick_Button()
    {
        PhotonNetwork.JoinRoom(_roomInfo.Name);
        
    }
    
}
