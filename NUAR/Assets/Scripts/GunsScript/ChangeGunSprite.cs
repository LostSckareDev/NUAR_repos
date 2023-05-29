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
    internal char Gun;

    private void Start()
    {
        spriteRenderer = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }
    [PunRPC]
    internal void ChangeSprite()
    {
        if (player.IsThompson == 1)
            spriteRenderer.sprite = spriteThompson;
        else if(player.IsWinchester == 1)
            spriteRenderer.sprite = spriteWinchester;
        else
            spriteRenderer.sprite = spritePistol;
        while (true)
        {
            if (player.bonusTimeStart < 0)
            {
                ChangeSprite();
                break;
            }
        }
    }

}
