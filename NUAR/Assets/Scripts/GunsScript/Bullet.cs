using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedBullet = 80f;
    public float lifetimeBullet;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    public PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        Invoke("DestroyBullet", lifetimeBullet);
    }

    private void Update()
    {
        if (view.IsMine)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
            if (hitInfo.collider != null)
            {
                Destroy(gameObject);
            }
            transform.Translate(Vector2.up * speedBullet * Time.deltaTime);
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("WallCollider"))
        {
            Destroy(gameObject);
        }
    }
}
