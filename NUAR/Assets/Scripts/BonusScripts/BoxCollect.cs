using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BoxCollect : MonoBehaviour
{
    PlayerController property;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            property = other.GetComponent<PlayerController>();
            if (property.IsSpeed != 1 && property.IsWinchester != 1 && property.IsThompson != 1 && property.view.IsMine)
                Destroy(gameObject);
        }
    }
}
