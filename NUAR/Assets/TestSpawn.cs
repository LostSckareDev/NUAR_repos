using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestSpawn : MonoBehaviour
{
    public GameObject ThompsonBox;    //первый префаб объекта для спавна
    public GameObject WinchesterBox; //второй префаб объекта для спавна
    public GameObject SpeedBox; //третий префаб объекта для спавна
    public GameObject HealthBox; //четвёртый префаб объекта для спавна

    private void Start()
    {
        GameObject obj;
        Vector2 spawnPoint = new Vector2(3, 16);
        obj = PhotonNetwork.Instantiate("Thompson_Box", spawnPoint, Quaternion.identity);
        spawnPoint = new Vector2(3, 13);
        obj = PhotonNetwork.Instantiate("Winchester_Box", spawnPoint, Quaternion.identity);
        spawnPoint = new Vector2(3, 10);
        obj = PhotonNetwork.Instantiate("Speed_Box", spawnPoint, Quaternion.identity);
        spawnPoint = new Vector2(3, 7);
        obj = PhotonNetwork.Instantiate("Health_Box", spawnPoint, Quaternion.identity);
    }

}
