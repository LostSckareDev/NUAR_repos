using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    private Vector3 playerVector;
    public int speed;
    public PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();

    }
    void Update()
    {
        if (view.IsMine)
        {
            /*playerVector.x = Player.position.x;
            playerVector.y = Player.position.y;
            playerVector.z = -10;
            transform.position = Vector3.Lerp(transform.position, playerVector, speed * Time.deltaTime);*/
            Vector3 playerVector = new Vector3(Player.position.x, Player.position.y - 20f, Player.position.z - 10);
            transform.position = playerVector;
        }
    }
}
