using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public float speedPlayer;
    private Rigidbody2D Rigidbody;
    private Vector2 moveInput;
    private Vector2 moveVelosity;
    public Joystick joystick;
    private Animator anim;
    private bool facingRight = true;
    public float health;
    public static int IsThompson = 0;
    public static int IsWinchester = 0;
    public Transform thompsonShotPoint;
    public Transform pistolShotPoint;
    
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        Camera.main.GetComponent<CameraFollow>().player = gameObject.transform;
    }

    void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelosity = moveInput.normalized * speedPlayer;

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
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ThompsonBox"))
        {
            if (IsWinchester == 1)
            {
                IsWinchester--;
            }
            Destroy(other.gameObject);
            IsThompson++;
        }

        else if (other.CompareTag("WinchesterBox"))
        {
            if (IsThompson == 1)
            {
                IsThompson--;
            }
            Destroy(other.gameObject);
            IsWinchester++;
        }
    }
}
