using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject LobbyPannel;
    public GameObject roomPannel;
    public Text roomName;

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemslist = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUptdates = 1.5f;
    float nextUptdateTime;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        roomPannel.SetActive(false);
    }

    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = 4,BroadcastPropsChangeToAll = true});

        }
    }

    public override void OnJoinedRoom()
    {
        LobbyPannel.SetActive(false);
        roomPannel.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUptdateTime)
        {
            UptdateRoomList(roomList);
            nextUptdateTime = Time.time + timeBetweenUptdates;
        }
    }

    void UptdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemslist)
        {
            Destroy(item.gameObject);
        }
        roomItemslist.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemslist.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPannel.SetActive(false);
        LobbyPannel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
