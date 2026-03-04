using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom(
            "VRRoom",
            new RoomOptions { MaxPlayers = 4 },
            TypedLobby.Default
        );
    }

    public override void OnJoinedRoom()
{
    PhotonNetwork.Instantiate(
        "VRPlayer",
        Vector3.zero,
        Quaternion.identity
    );
}
}