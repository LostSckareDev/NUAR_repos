using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public InputField CreateIDField;
    public InputField FindIDField;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.CreateRoom(CreateIDField.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(FindIDField.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
