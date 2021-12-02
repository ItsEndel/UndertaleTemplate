using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TitleSystem : MonoBehaviour
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



    void Update()
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
                    Debug.Log(IniSave.Path);
                    if (!File.Exists(IniSave.Path)) // ���"undertale.ini"������
                    {
                        SceneManager.LoadScene(Main.Scene.NewPlayer);

                        // Save save = new Save();

                        // BinaryFormatter fomatter = new BinaryFormatter();
                        // FileStream file = File.Create(Application.persistentDataPath + "/file0");
                        // fomatter.Serialize(file, save);
                        // file.Close();

                        IniSave.Write("General", "fun", 100f.ToString());
                    } else
                    {
                        SceneManager.LoadScene(Main.Scene.MainMenu);
                    }

                    break;
            }

            state += 1;
        }
    }
}
