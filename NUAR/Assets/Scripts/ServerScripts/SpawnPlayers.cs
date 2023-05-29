using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public float minX, minY, maxX, maxY;

    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(8, 10), Random.Range(10, 15));
        PhotonNetwork.Instantiate ("Player", randomPosition, Quaternion.identity);
    }
}
