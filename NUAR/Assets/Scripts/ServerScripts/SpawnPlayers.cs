using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    public float minX, minY, maxX, maxY;

    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(0, 10), Random.Range(1, 15));
        PhotonNetwork.Instantiate (player.name, randomPosition, Quaternion.identity);
    }


}
