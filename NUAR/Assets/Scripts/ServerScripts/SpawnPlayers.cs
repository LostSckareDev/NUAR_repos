using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviour
{
    public float minX, minY, maxX, maxY;
    public GameObject Player;
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-37f, 41f), Random.Range(-22f, 26f));
        PhotonNetwork.Instantiate ("Player", randomPosition, Quaternion.identity);
    }

}
    