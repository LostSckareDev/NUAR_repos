using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public InputField CreateIDField;
    public InputField FindIDField;
<<<<<<< HEAD
    public InputField NickNameInput;
    public GameObject Player;

    public void CreateRoom()
    {

=======

    public void CreateRoom()
    {
>>>>>>> db8bbfaa93b8fdb9ec480fa8e1612137b8589388
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.CreateRoom(CreateIDField.text, roomOptions);
    }

    public void JoinRoom()
    {
<<<<<<< HEAD

=======
>>>>>>> db8bbfaa93b8fdb9ec480fa8e1612137b8589388
        PhotonNetwork.JoinRoom(FindIDField.text);
    }

    public override void OnJoinedRoom()
    {
<<<<<<< HEAD

=======
>>>>>>> db8bbfaa93b8fdb9ec480fa8e1612137b8589388
        PhotonNetwork.LoadLevel("Game");
    }
}
