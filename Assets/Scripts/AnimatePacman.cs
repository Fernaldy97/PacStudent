using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePacman : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites = new Sprite[0];
    public float time = 0.25f;
    public int animFrame;
    public bool loop = true;

    //Initialise sprite
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Invoke every amount of x seconds
        InvokeRepeating(nameof(Repeat), time, time);
    }

    private void Repeat()
    {
        this.animFrame++;
        //Check if it needs loop
        if(this.animFrame >= this.sprites.Length && loop)
        {
            animFrame = 0; //Reset back to 0
        }
        //Update the sprites
        if(this.animFrame >= 0 && this.animFrame < this.sprites.Length)
        {
            spriteRenderer.sprite = sprites[animFrame];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
