using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviour
{
    PhotonView view;

    public float speedBullet = 55f; 
    public float distance;
    public LayerMask whatIsSolid;
    private PlayerController player;
    bool hit = false;

    private void Start()
    {
        view = GetComponent<PhotonView>();
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
            //Destroy(gameObject);
            if (view.IsMine)
            PhotonNetwork.Destroy(gameObject);
            //if (hitInfo.transform.gameObject == "Player (Clone)")
            //.ChangeHealth(7);
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
