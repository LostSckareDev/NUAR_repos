using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float offset;
    public GameObject bullet; //объект пули
    public Transform shotPoint; //точка, из которой вылетает пуля
    private float timeBtwShots; //время перезарядки пистолета 
    private float startTimeBtwShots = 0.5f; //начальное время перезарядки пистолета
    private float startTimeBtwShots_T = 0.1f;
    private PlayerController player; //скрипт игрока
    private float rotZ;
    public Joystick joystick; //джойстик стрельбы
    private bool facingRight = true; //переменная, определяющая сторону, в которую смотрит пистолет (если вправо, то true, влево - false)
    public GameObject Player; //объект игрока

    private void Start()
    {
        
    }
    void Update()
    {
        if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f) //если джойстик отклонён на 0.3 по всем осям 
        {
            rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg; //поворот пистолета относительно джойстика
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if(PlayerController.IsThompson == 0)
        {
            if((timeBtwShots <= 0) && (Mathf.Abs(joystick.Horizontal) > 0.4f || Mathf.Abs(joystick.Vertical) > 0.4f)) //если время преезарядки кончилось и джойстик отклонён на 0.3 по всем осям, то произвести выстрел
            {
                if(joystick.Vertical != 0 || joystick.Horizontal != 0)
                {
                    Shoot();
                }
            }
            else //иначе декрементировать переменную перезарядки 
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

        else if(PlayerController.IsThompson == 1)
        {
            if((timeBtwShots <= 0) && (Mathf.Abs(joystick.Horizontal) > 0.4f || Mathf.Abs(joystick.Vertical) > 0.4f)) //если время преезарядки кончилось и джойстик отклонён на 0.3 по всем осям, то произвести выстрел
            {
                if(joystick.Vertical != 0 || joystick.Horizontal != 0)
                {
                    Shoot();
                }
            }
            else //иначе декрементировать переменную перезарядки 
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

        
        //условие поворота пистолета если игрок смотрит в правую сторону
        if(Player.transform.localScale.x > 0)
        {
            if(!facingRight && joystick.Horizontal > 0)
            {
            Flip();
            }
            else if(facingRight && joystick.Horizontal < 0)
            {
            Flip();
            }
        }
        //условие поворота пистолета если игрок смотрит в левую сторону
        else if(Player.transform.localScale.x < 0)
        {
            if(facingRight && joystick.Horizontal > 0)
            {
            Flip();
            }
            else if(!facingRight && joystick.Horizontal < 0)
            {
            Flip();
            }
        }
    }

    public void Shoot() //функция стрельбы 
    {
        if(PlayerController.IsThompson == 0)
        {
        Instantiate(bullet, shotPoint.position, transform.rotation);
        timeBtwShots = startTimeBtwShots;
        }

        else if(PlayerController.IsThompson == 1)
        {
        Instantiate(bullet, shotPoint.position, transform.rotation);
        timeBtwShots = startTimeBtwShots_T;
        }
    }

    private void Flip() //функция поворота пистолета 
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
