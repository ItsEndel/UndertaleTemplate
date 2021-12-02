using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSystem : MonoBehaviour
{
    private int timer;

    void Start()
    {
        Application.targetFrameRate = 30;


    }

    void Update()
    {
        timer++;
        if (timer > 30) { SceneManager.LoadScene(Main.Scene.Story); }
    }
}
