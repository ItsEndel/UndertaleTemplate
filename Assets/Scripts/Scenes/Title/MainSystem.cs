using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSystem : MonoBehaviour
{
    // ��Ϸ����
    Title title;
    Text text;

    // �׶�
    int state = 0;

    void Start()
    {
        // ����֡������
        Application.targetFrameRate = 30;

        // ��ȡ����
        title = GameObject.Find("/Title").GetComponent<Title>();
        text = GameObject.Find("/Canvas/Text").GetComponent<Text>();



        // ������Ч
        AudioSource audio = this.GetComponent<AudioSource>();
        audio.Play();
    }

    private void Update()
    {
        AudioSource audio = this.GetComponent<AudioSource>();

        // �ж���Ƶ������ϻ���Z��
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
