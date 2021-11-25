using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public void Hide()
    {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

        renderer.enabled = false;
    }
}
