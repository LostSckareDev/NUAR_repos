using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    PhotonView view;

    public float speedBullet = 80f;
    public float lifetimeBullet;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    private PlayerController player;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            Destroy(gameObject);
        }
        if (hitInfo.collider == null)
        {
            transform.Translate(Vector2.up * speedBullet * Time.deltaTime);
        }
        
    }

    public void DestroyBullet()
    {
        PhotonNetwork.Destroy(gameObject);
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
