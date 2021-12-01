using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    private bool? finished = null;

    private SpriteRenderer spriteRenderer;

    public List<Sprite> sprites = new List<Sprite>();

    private int index = 0;

    private int timer = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer--;

            if (timer == 0)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

                finished = null;
            }
            else if (timer == 15)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

                index += 1;
                spriteRenderer.sprite = sprites[index];

                finished = true;
            }
            else if (timer > 15) { spriteRenderer.color -= new Color(0f, 0f, 0f, 0.06f); }
            else if (timer < 15) { spriteRenderer.color += new Color(0f, 0f, 0f, 0.06f); }
        }
    }



    public void Next()
    {
        finished = false;

        timer = 30;
    }



    public bool? Finished()
    {
        return finished;
    }
}
