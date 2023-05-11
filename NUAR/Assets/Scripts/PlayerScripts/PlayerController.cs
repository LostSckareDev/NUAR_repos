using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    PhotonView view;

    public float speedPlayer;
    private Rigidbody2D Rigidbody;
    private Vector2 moveInput;
    private Vector2 moveVelosity;
    public Joystick joystick;
    private Animator anim;
    private bool facingRight = true;
    public int health;
    public TextMeshProUGUI healthText;
    private const float healthFull = 100f;
    public static int IsThompson = 0;
    public static int IsWinchester = 0;
    private int IsSpeed = 0;
    public Transform thompsonShotPoint;
    public Transform pistolShotPoint;
    private float bonusTimeStart = 10f;
    bool timerRunning = true;
    private float bonusTimeStartSpeed = 10f;
    public Pistol pistol;
    
    void Start()
    {
        view = GetComponent<PhotonView>();
        healthText = GameObject.Find("TextHealth").GetComponent<TextMeshProUGUI>();
        Rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        Camera.main.GetComponent<CameraFollow>().player = gameObject.transform;
    }

    void Update()
    {
        if (view.IsMine)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            moveVelosity = moveInput.normalized * speedPlayer * Time.deltaTime;
            transform.position += (Vector3)moveVelosity;
        }

        healthText.text = health.ToString();

        if(moveInput.x == 0)
        {
            anim.SetBool("IsWalk", false);
        }
        else
        {
            anim.SetBool("IsWalk", true);
        }

        if(!facingRight && moveInput.x < 0)
        {
            Flip();
        }
        else if(facingRight && moveInput.x > 0)
        {
            Flip();
        }

        if(IsThompson == 1)
        {
            if(timerRunning == true)
            {
                bonusTimeStart -= Time.deltaTime;
            }
            if(bonusTimeStart < 0)
            {
                IsThompson = 0;
                bonusTimeStart = 10f;
            }
        }

        if(IsWinchester == 1)
        {
            if(timerRunning == true)
            {
                bonusTimeStart -= Time.deltaTime;
            }
            if(bonusTimeStart < 0)
            {
                IsWinchester = 0;
                bonusTimeStart = 10f;
            }
        }

        if(IsSpeed == 1)
        {
            if(timerRunning == true)
            {
                bonusTimeStart -= Time.deltaTime;
            }
            if(bonusTimeStart < 0)
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
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        pistol.Flip();
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ThompsonBox") && IsSpeed != 1 && IsWinchester != 1 && IsThompson != 1)
        {
            //if (IsWinchester == 1)
            //{
            //    IsWinchester--;
            //}
            Destroy(other.gameObject);
            IsThompson++;
        }

        else if (other.CompareTag("WinchesterBox") && IsSpeed != 1 && IsWinchester != 1 && IsThompson != 1)
        {
            //if (IsThompson == 1)
            //{
            //    IsThompson--;
            //}
            Destroy(other.gameObject);
            IsWinchester++;
        }

        else if (other.CompareTag("SpeedBox") && IsSpeed != 1 && IsWinchester != 1 && IsThompson != 1)
        {
            // if (IsSpeed == 1)
            // {
            //     IsSpeed--;
            //}
            Destroy(other.gameObject);
            IsSpeed++;
            speedPlayer = speedPlayer + 5f;
        }

        else if (other.CompareTag("HealthBox"))
        {
            if((health + 20) < healthFull)
            {
                health += 20;
            }
            else 
            {
                while(health < healthFull)
                {
                    health++;
                }
            }
            Destroy(other.gameObject);
            Debug.Log("Health: " + health);
        }
    }
}
