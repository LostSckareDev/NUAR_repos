using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pistol : MonoBehaviour
{
    public float offset;
    public GameObject bullet; //объект пули
    public Transform shotPoint; //точка для пистолета, из которой вылетает пуля
    public Transform shotPoint_T; //точка для Томпсона, из которой вылетает пуля
    public Transform shotPoint_W; //точка для Томпсона, из которой вылетает пуля
    private float timeBtwShots; //время перезарядки пистолета 
    private float startTimeBtwShots = 0.5f; //начальное время перезарядки пистолета
    private float startTimeBtwShots_T = 0.1f; //начальное время перезарядки Томпсона
    private float startTimeBtwShots_W = 1f; //начальное время перезарядки Винчествера
    private PlayerController player; //скрипт игрока
    private float rotZ;
    public Joystick joystick; //джойстик стрельбы
    private bool facingRight = true; //переменная, определяющая сторону, в которую смотрит пистолет (если вправо, то true, влево - false)
    public GameObject Player; //объект игрока

    public PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (view.IsMine)
        {
            if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f) //если джойстик отклонён на 0.3 по всем осям 
            {
                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg; //поворот пистолета относительно джойстика
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            if ((timeBtwShots <= 0) && (Mathf.Abs(joystick.Horizontal) > 0.4f || Mathf.Abs(joystick.Vertical) > 0.4f)) //если время преезарядки кончилось и джойстик отклонён на 0.3 по всем осям, то произвести выстрел
            {
                if (joystick.Vertical != 0 || joystick.Horizontal != 0)
                {
                    Shoot();
                }
            }
            else //иначе декрементировать переменную перезарядки 
            {
                timeBtwShots -= Time.deltaTime;
            }

            //условие поворота пистолета если игрок смотрит в правую сторону
            if (Player.transform.localScale.x > 0)
            {
                if (!facingRight && joystick.Horizontal > 0)
                {
                    Flip();
                }
                else if (facingRight && joystick.Horizontal < 0)
                {
                    Flip();
                }
            }
            //условие поворота пистолета если игрок смотрит в левую сторону
            else if (Player.transform.localScale.x < 0)
            {
                if (facingRight && joystick.Horizontal > 0)
                {
                    Flip();
                }
                else if (!facingRight && joystick.Horizontal < 0)
                {
                    Flip();
                }
            }
        }
    }

    public void Shoot() //функция стрельбы 
    {
        if (view.IsMine)
        {
            if (PlayerController.IsThompson == 0 && PlayerController.IsWinchester == 0)
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }

            else if (PlayerController.IsThompson == 1 && PlayerController.IsWinchester == 0)
            {
                float angle = 10f; // Угол разброса
                float randomAngle = Random.Range(-angle, angle); // Случайное значение угла

                Quaternion bulletRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + randomAngle);

                Instantiate(bullet, shotPoint_T.position, bulletRotation);
                timeBtwShots = startTimeBtwShots_T;
            }

            else if (PlayerController.IsThompson == 0 && PlayerController.IsWinchester == 1)
            {

                int WinBull = 5;
                while (WinBull > 0)
                {
                    float angle = 20f;
                    float randomAngle = Random.Range(-angle, angle);
                    Quaternion bulletRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + randomAngle);
                    Instantiate(bullet, shotPoint_W.position, bulletRotation);
                    WinBull--;
                }
                timeBtwShots = startTimeBtwShots_W;
            }
        }
    }

    public void Flip() //функция поворота пистолета 
    {
        
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;

    }
}
