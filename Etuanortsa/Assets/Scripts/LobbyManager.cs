using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject LobbyPannel;
    public GameObject roomPannel;
    public Text roomName;
    public GameObject ChangeColor;

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemslist = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUptdates = 1.5f;
    float nextUptdateTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        roomPannel.SetActive(false);
    }

    public void OnClickBack()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("menu");
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
        UpdatePlayerList();
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

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem =  Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }
            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        bool valid = true;
        List<int> val = new List<int>();
        int i = 0;
        Player[] pl = PhotonNetwork.PlayerList;
        while (i < pl.Length && valid)
        {
            if(val.Contains((int)pl[i].CustomProperties["playerAvatar"]))
            {
                valid = false;
            }
            else 
            {
                val.Add((int)pl[i].CustomProperties["playerAvatar"]);
            }
            i++;
        }

        if(valid) 
        {
            Destroy(GameObject.FindGameObjectWithTag("GameMusic"));
            PhotonNetwork.LoadLevel("level");
        }
        else 
        {
            StartCoroutine(Change());
        }
    }

    IEnumerator Change()
    {
        ChangeColor.SetActive(true);
        yield return new WaitForSeconds(5f);
        ChangeColor.SetActive(false);
    }
}
