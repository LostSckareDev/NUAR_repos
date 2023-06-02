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
        while (true)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-36f, 40f), Random.Range(-21f, 25f));

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 1f);

            bool canSpawn = true;

            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("WallCollider") || col.CompareTag("Player"))
                    canSpawn = false;
            }

            if (canSpawn)
            {
                PhotonNetwork.Instantiate ("Player", spawnPosition, Quaternion.identity);
                break;
            }
        }
    }

}
    