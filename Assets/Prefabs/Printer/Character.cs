using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Transform position;

    public Dictionary<string, string> effects;

    void Start()
    {
        position = this.transform;
    }

    void Update()
    {
        if (effects.ContainsKey("Tremble"))
        {

        }
    }
}
