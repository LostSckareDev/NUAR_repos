using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour, IPunObservable
{
    internal PhotonView view;

    public float speedPlayer = 7f;
    private Rigidbody2D Rigidbody;
    private Vector2 moveInput;
    private Vector2 moveVelosity;
    private Joystick joystick;
    private Animator anim;
    private bool facingRight = true;

    public int health;
    private TextMeshProUGUI healthText;
    private const float healthFull = 100f;

    public int IsThompson = 0;
    public int IsWinchester = 0;
    public int IsSpeed = 0;

    public Transform thompsonShotPoint;
    public Transform pistolShotPoint;
    internal float bonusTimeStart = 10f;
    bool timerRunning = true;
    public Pistol pistol;

    private int deaths = 0;
    public int kills = 0;

    public Text textName;

    public ParticleSystem blood;
    
    void Start()
    {
        view = GetComponent<PhotonView>();

        blood.Stop();

        healthText = GameObject.Find("TextHealth").GetComponent<TextMeshProUGUI>();
        Rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        joystick = GameObject.FindGameObjectWithTag("JoystickWalk").GetComponent<Joystick>();

        textName.text = view.Owner.NickName;
        
        if(view.Owner.IsLocal)
            Camera.main.GetComponent<CameraFollow>().player = gameObject.transform;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int) stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (view.IsMine)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            moveVelosity = moveInput.normalized * speedPlayer * Time.deltaTime;
            transform.position += (Vector3)moveVelosity;

            //deathsText.text = deaths.ToString(); ���� ������
            //killsText.text = kills.ToString(); ���� ��������

            healthText.text = health.ToString();

            if (moveInput.x == 0)
            {
                anim.SetBool("IsWalk", false);
            }
            else
            {
                anim.SetBool("IsWalk", true);
            }
        }
            if (!facingRight && moveInput.x < 0)
            {
                Flip();
            }
            else if (facingRight && moveInput.x > 0)
            {
                Flip();
            }

           
            if (IsSpeed == 1)
            {
                if (timerRunning == true)
                {
                    bonusTimeStart -= Time.deltaTime;
                }
                if (bonusTimeStart < 0)
                {
                    IsSpeed = 0;
                    bonusTimeStart = 10f;
                    speedPlayer = speedPlayer - 5f;
                }
            }
        
    }
    
    void FixedUpdate()
    {
        Rigidbody.MovePosition(Rigidbody.position + moveVelosity * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        view.RPC("SyncNicknameRotation", RpcTarget.All);
        
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        pistol.Flip();
    }

    [PunRPC]
    private void SyncNicknameRotation()
    {
        textName.transform.Rotate(0f, 180f, 0f);
    }

    [PunRPC]
    private void BloodSplash()
    {
        blood.Play();
    }

    public bool ChangeHealth(int healthValue)
    {
        if (view.IsMine)
        {
            health -= healthValue;
            view.RPC("BloodSplash", RpcTarget.All);
        }
        if (health <= 0)
        {
            deaths++;
            while (true)
            {
                Vector2 spawnPoint = new Vector2(Random.Range(-37f, 41f), Random.Range(-22f, 26f));

                Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint, 1f);

                bool canSpawn = true;

                foreach (Collider2D col in colliders)
                {
                    if (col.CompareTag("WallCollider") || col.CompareTag("Player"))
                        canSpawn = false;
                }

                if (canSpawn)
                {
                    transform.position = spawnPoint;
                    IsSpeed = 0;
                    IsThompson = 0;
                    IsWinchester = 0;
                    speedPlayer = 7f;
                    health = 100;
                    break;
                }
            }
            return true;
        }
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        {

            if (view.IsMine)
            {
                /*if (other.CompareTag("Bullet"))
                {
                    ChangeHealth(15);
                    //blood.Play();
                    //BloodSplash();
                    //BloodSplash();
                    view.RPC("BloodSplash", RpcTarget.All);
                }*/
                if (other.CompareTag("ThompsonBox") && IsSpeed != 1 && IsWinchester != 1 && IsThompson != 1)
                {
                        IsThompson++;
                }

                else if (other.CompareTag("WinchesterBox") && IsSpeed != 1 && IsWinchester != 1 && IsThompson != 1)
                {
                        IsWinchester++;
                }

                else if (other.CompareTag("SpeedBox") && IsSpeed != 1 && IsWinchester != 1 && IsThompson != 1)
                {
                    {
                        IsSpeed++;
                        speedPlayer = speedPlayer + 5f;
                    }
                }

                else if (other.CompareTag("HealthBox"))
                {
                    {
                        if ((health + 40) < healthFull)
                        {
                            health += 40;
                        }
                        else
                        {
                            while (health < healthFull)
                            {
                                health++;
                            }
                        }
                        Debug.Log("Health: " + health);
                    }
                }
            }
        }
    }
}
