using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGunSprite : MonoBehaviour
{
    public Sprite spritePistol;
    public Sprite spriteThompson;
    public Sprite spriteWinchester;
    private SpriteRenderer spriteRenderer;
    public PlayerController player;
    
    private void Start()
    {
        //player = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(player.IsThompson == 1 && player.IsWinchester == 0)
        {
            spriteRenderer.sprite = spriteThompson;
        }
        else if(player.IsThompson == 0 && player.IsWinchester == 1)
        {
            spriteRenderer.sprite = spriteWinchester;
        }
        else if(player.IsThompson == 0 && player.IsWinchester == 0)
        {
            spriteRenderer.sprite = spritePistol;
        }
    }

}
