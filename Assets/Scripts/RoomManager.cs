using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster(){
        base.OnConnectedToMaster();
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby(){
        base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("test",null,null);
        Debug.Log("we're connected and in the Lobby");
    }

    public override void OnJoinedRoom(){
        base.OnJoinedRoom();
        Debug.Log("we're in the room");
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
    }
}
