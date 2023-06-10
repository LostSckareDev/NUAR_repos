using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public InputField CreateIDField;
    public InputField FindIDField;
    public InputField inputName;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        inputName.text = PlayerPrefs.GetString("name");
        PhotonNetwork.NickName = inputName.text;
    }
    
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;
        roomOptions.CleanupCacheOnLeave = false;
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

    public void SaveName()
    {
        PlayerPrefs.SetString("name", inputName.text);
        PhotonNetwork.NickName = inputName.text;
    }
    
}
