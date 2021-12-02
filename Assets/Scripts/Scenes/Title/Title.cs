using UnityEngine;

public class Title : MonoBehaviour
{
    public void Hide()
    {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

        renderer.enabled = false;
    }
}
