using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject Player;
    public Vector2 randomPosition;
    private GameObject currentPlayer;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {

        Vector2 randomPosition = new Vector2(Random.Range(10, 15), Random.Range(0, 1));
        currentPlayer = PhotonNetwork.Instantiate(Player.name, randomPosition, Quaternion.identity);
        SetupCamera();
    }

    private void SetupCamera()
    {
        Camera.main.transform.SetParent(currentPlayer.transform);
        Camera.main.transform.localPosition = new Vector3(0f, 5f, -10f);
        Camera.main.transform.localRotation = Quaternion.identity;
    }
}

