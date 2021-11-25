using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSystem : MonoBehaviour
{
    // 游戏对象
    Title title;
    Text text;

    // 阶段
    int state = 0;

    void Start()
    {
        // 设置帧率限制
        Application.targetFrameRate = 30;

        // 获取对象
        title = GameObject.Find("/Title").GetComponent<Title>();
        text = GameObject.Find("/Canvas/Text").GetComponent<Text>();



        // 播放音效
        AudioSource audio = this.GetComponent<AudioSource>();
        audio.Play();
    }

    private void Update()
    {
        AudioSource audio = this.GetComponent<AudioSource>();

        // 判断音频播放完毕或按下Z键
        if (!audio.isPlaying || Input.GetKeyDown(KeyCode.Z))
        {
            switch (state)
            {
                // UNDERTALE -> OriginalGame
                case 0:
                    title.Hide();

                    text.text = "Original Game\n                - By TobyFox";

                    audio.Play();

                    break;

                // OrignalGame -> UndertaleTemplate
                case 1:
                    text.text = "Undertale Template\n               - By ItsEndel";

                    audio.Play();

                    break;

                // UndertaleTemplate -> 
                case 2:
                    text.text = "";

                    audio.Play();

                    break;

                // Switch Scene
                case 3:
                    Debug.Log("Done");

                    SceneManager.LoadScene(1);

                    break;
            }

            state += 1;
        }
    }
}
