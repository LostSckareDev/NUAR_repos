using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BoxCollect : MonoBehaviour
{
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && view.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
