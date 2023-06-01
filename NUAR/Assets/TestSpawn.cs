using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestSpawn : MonoBehaviour
{
    public GameObject ThompsonBox;    //������ ������ ������� ��� ������
    public GameObject WinchesterBox; //������ ������ ������� ��� ������
    public GameObject SpeedBox; //������ ������ ������� ��� ������
    public GameObject HealthBox; //�������� ������ ������� ��� ������

    private void Start()
    {
        Vector2 spawnPoint = new Vector2(3, 16);
        PhotonNetwork.Instantiate("Thompson_Box", spawnPoint, Quaternion.identity);
        spawnPoint = new Vector2(3, 13);
        PhotonNetwork.Instantiate("Winchester_Box", spawnPoint, Quaternion.identity);
        spawnPoint = new Vector2(3, 10);
        PhotonNetwork.Instantiate("Speed_Box", spawnPoint, Quaternion.identity);
        spawnPoint = new Vector2(3, 7);
        PhotonNetwork.Instantiate("Health_Box", spawnPoint, Quaternion.identity);
    }

}
