using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviour
{
    PhotonView view;

    public float speedBullet = 50f; 
    public float distance;
    public LayerMask whatIsSolid;
    private PlayerController player;
    private PlayerController killer;
    private bool hit = false;
    public int ownerNumber;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        ownerNumber = view.ViewID/1000 + 1;
    }

    private void Update()
    {
        /*RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            Destroy(gameObject);
        }
        if (hitInfo.collider == null)
        {
            transform.Translate(Vector2.up * speedBullet * Time.deltaTime);
        }*/
        if (hit)
            return;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider == null)
        {
            transform.Translate(Vector2.up * speedBullet * Time.deltaTime);
        }
        else
        {
            hit = true;

            if (view.IsMine)
                PhotonNetwork.Destroy(gameObject);

            if (hitInfo.transform.gameObject.name == "Player(Clone)")
            {
                player = hitInfo.transform.gameObject.GetComponent<PlayerController>();
                if (player.ChangeHealth(15) == true)
                {
                    killer = PhotonView.Find(ownerNumber).GetComponent<PlayerController>();
                    killer.kills++;
                }
                player.ChangeHealth(15);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if(other.CompareTag("WallCollider"))
        {
            Destroy(gameObject);
        }
        if(other.CompareTag("Player") && view.IsMine)
        {
            player = other.gameObject.GetComponent<PlayerController>();
            DestroyBullet();
            player.ChangeHealth(5);
        }*/
    }
}
