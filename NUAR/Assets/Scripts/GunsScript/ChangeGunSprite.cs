using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ChangeGunSprite : MonoBehaviour
{
    public Sprite spritePistol;
    public Sprite spriteThompson;
    public Sprite spriteWinchester;
    public SpriteRenderer spriteRenderer;
    public PlayerController player;
    public string Gun;
    bool timerRunning = true;
    internal PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if (player.IsThompson == 1)
            {
                Gun = "T";
                if (spriteRenderer.sprite != spriteThompson)
                    view.RPC("ChangeSprite", RpcTarget.AllBufferedViaServer, Gun);
                if (timerRunning == true)
                {
                    player.bonusTimeStart -= Time.deltaTime;
                }
                if (player.bonusTimeStart < 0)
                {
                    Gun = "P";
                    player.IsThompson = 0;
                    player.bonusTimeStart = 10f;
                    view.RPC("ChangeSprite", RpcTarget.AllBufferedViaServer, Gun);
                }
            }

            else if (player.IsWinchester == 1)
            {
                Gun = "W";
                if (spriteRenderer.sprite != spriteWinchester)
                {
                    view.RPC("ChangeSprite", RpcTarget.AllBufferedViaServer, Gun);
                }
                if (timerRunning == true)
                {
                    player.bonusTimeStart -= Time.deltaTime;
                }
                if (player.bonusTimeStart < 0)
                {
                    Gun = "P";
                    player.IsWinchester = 0;
                    player.bonusTimeStart = 10f;
                    view.RPC("ChangeSprite", RpcTarget.AllBufferedViaServer, Gun);
                }
            }
            else
            {
                Gun = "P";
                if (spriteRenderer.sprite != spritePistol)
                {
                    view.RPC("ChangeSprite", RpcTarget.AllBufferedViaServer, Gun);
                }

            }
        }
    }

    [PunRPC]
    public void ChangeSprite(string a)
    {
        if (a == "T")
            spriteRenderer.sprite = spriteThompson;
        else if(a == "W")
            spriteRenderer.sprite = spriteWinchester;
        else 
            spriteRenderer.sprite = spritePistol;
    }

}
