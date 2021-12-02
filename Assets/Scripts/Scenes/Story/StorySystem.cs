using UnityEngine;
using UnityEngine.SceneManagement;

public class StorySystem : MonoBehaviour
{
    private GameObject ui;
    private GameObject intro;
    private Intro introScript;

    public GameObject printerPrefab;

    private GameObject printer;
    private Printer printerScript;

    private int state = 0;
    private int timer = 0;
    
    void Start()
    {
        Application.targetFrameRate = 30;

        ui = GameObject.Find("UI");
        intro = GameObject.Find("Intro");
        introScript = intro.GetComponent<Intro>();

        printer = Instantiate<GameObject>(printerPrefab, ui.transform);
        printerScript = printer.GetComponent<Printer>();
        printer.transform.position = new Vector3(120, 160, 0);
        printerScript.Text = "[charSpaceCn=13][lineSpace=13][delay=3]很久以前，[sleep=15]地球由\n两个种族统治着：[sleep=15]\n人类和怪物。";
    }

    

    void Update()
    {
        if (printerScript.Finished())
        {
            if (timer == 0)
            {
                timer = 55;
            } else if (timer == 1)
            {
                if (introScript.Finished() == true)
                {
                    timer = 0;

                    GameObject.Destroy(printer);

                    state += 1;

                    switch (state)
                    {
                        case 0:
                            printer = Instantiate<GameObject>(printerPrefab, ui.transform);
                            printerScript = printer.GetComponent<Printer>();
                            printer.transform.position = new Vector3(120, 160, 0);
                            printerScript.Text = "[charSpaceCn=13][lineSpace=13][delay=3]很久以前，[sleep=15]地球由\n两个种族统治着：[sleep=15]\n人类和怪物。";
                            break;

                        case 1:
                            printer = Instantiate<GameObject>(printerPrefab, ui.transform);
                            printerScript = printer.GetComponent<Printer>();
                            printer.transform.position = new Vector3(120, 160, 0);
                            printerScript.Text = "[charSpaceCn=13][lineSpace=13][delay=3]有一天，[sleep=15]两个种族之间\n爆发了战争。";
                            break;

                        case 2:
                            printer = Instantiate<GameObject>(printerPrefab, ui.transform);
                            printerScript = printer.GetComponent<Printer>();
                            printer.transform.position = new Vector3(120, 160, 0);
                            printerScript.Text = "[charSpaceCn=13][lineSpace=13][delay=3]经历漫长的战争之后，[sleep=15]\n人类赢得了胜利。";
                            break;

                        case 3:
                            printer = Instantiate<GameObject>(printerPrefab, ui.transform);
                            printerScript = printer.GetComponent<Printer>();
                            printer.transform.position = new Vector3(120, 160, 0);
                            printerScript.Text = "[charSpaceCn=13][lineSpace=13][delay=3]他们用一道魔法咒文\n将怪物们封印在了\n地底。";
                            break;

                        case 4:
                            printer = Instantiate<GameObject>(printerPrefab, ui.transform);
                            printerScript = printer.GetComponent<Printer>();
                            printer.transform.position = new Vector3(120, 160, 0);
                            printerScript.Text = "[charSpaceCn=13][lineSpace=13][delay=3]很多年过去了.[sleep=15].[sleep=15].";
                            break;

                        case 5:
                            printer = Instantiate<GameObject>(printerPrefab, ui.transform);
                            printerScript = printer.GetComponent<Printer>();
                            printer.transform.position = new Vector3(120, 160, 0);
                            printerScript.Text = "[charSpaceCn=13][lineSpace=13][delay=3][position=150.5,0,0]EBOTT山[sleep=15][position=170,-37,0]202X";
                            break;

                        case 6:
                            printer = Instantiate<GameObject>(printerPrefab, ui.transform);
                            printerScript = printer.GetComponent<Printer>();
                            printer.transform.position = new Vector3(120, 160, 0);
                            printerScript.Text = "[charSpaceCn=13][lineSpace=13][delay=3]传说那些爬上山去的\n人们从此再也没有\n回来过。";
                            break;

                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            printer = Instantiate<GameObject>(printerPrefab, ui.transform);
                            printerScript = printer.GetComponent<Printer>();
                            printer.transform.position = new Vector3(120, 160, 0);
                            printerScript.Text = "[sleep=75] ";
                            break;

                        case 11:
                            printer = Instantiate<GameObject>(printerPrefab, ui.transform);
                            printerScript = printer.GetComponent<Printer>();
                            printer.transform.position = new Vector3(120, 160, 0);
                            printerScript.Text = "[sleep=90]";
                            break;

                        case 12:
                            SceneManager.LoadScene(Main.Scene.Title);
                            break;

                    }
                }
                else if (introScript.Finished() == null) { introScript.Next(); }
            } else { timer--; }
        }
    }
}
