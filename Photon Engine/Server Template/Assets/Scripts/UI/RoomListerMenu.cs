using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListerMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private RoomItem _roomListerPrefab;
    private List<RoomItem> _listing = new List<RoomItem>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Callback");
        foreach (RoomInfo info in roomList)
        {
            Debug.Log("Callback" + info.Name);
            if(info.RemovedFromList)
            {
                int index = _listing.FindIndex(x => x._roomInfo.Name == info.Name);
                if(index != -1)
                {
                    Destroy(_listing[index].gameObject);
                    _listing.RemoveAt(index);
                }
                
            }
            else
            {
                RoomItem room = Instantiate(_roomListerPrefab, content);
                if (room != null)
                {
                    room.SetRoomInfo(info);
                    _listing.Add(room);
                }
            }
            
        }
    }
}
