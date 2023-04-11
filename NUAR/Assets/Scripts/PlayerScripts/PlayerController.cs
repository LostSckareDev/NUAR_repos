using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public float speedPlayer;
    public Joystick joystick;
    private float _moveInputX;
    private float _moveInputY;
    
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        Camera.main.GetComponent<CameraFollow>().player = gameObject.transform;
    }
    
    void FixedUpdate()
    {
        _moveInputX = joystick.Horizontal * speedPlayer;
        _moveInputY = joystick.Vertical * speedPlayer;
        
        Vector2 position = transform.position;
        position.x = position.x + _moveInputX * Time.deltaTime;
        position.y = position.y + _moveInputY * Time.deltaTime;
        transform.position = position;
        
    }
}
