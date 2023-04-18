using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGunSprite : MonoBehaviour
{
    public Sprite spritePistol;
    public Sprite spriteThompson;
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(PlayerController.IsThompson == 1)
        {
            spriteRenderer.sprite = spriteThompson;
        }
        else if(PlayerController.IsThompson == 0)
        {
            spriteRenderer.sprite = spritePistol;
        }
    }

}
