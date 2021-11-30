using UnityEngine;

public class NewPlayerSystem : MonoBehaviour
{
    private GameObject UI;

    public GameObject printerPrefab;

    private GameObject printer;
    private Printer printerScript;

    private int state = 0;
    private int timer = 0;
    
    void Start()
    {
        UI = GameObject.Find("UI");

        printer = Instantiate<GameObject>(printerPrefab, UI.transform);
        printerScript = printer.GetComponent<Printer>();
        printer.transform.position = new Vector3(120, 160, 0);
        printerScript.Text = "[charSpaceCn=13][lineSpace=13][delay=60]�ܾ���ǰ��[sleep=120]������\n��������ͳ���ţ�[sleep=120]\n����͹��";
    }

    

    void Update()
    {
        if (printerScript.Finished())
        {
            if (timer == 0)
            {
                timer = 480;
            } else if (timer == 1)
            {
                NextIntro();
            } else
            {
                timer--;
            }
        }
    }

    private void NextIntro()
    {

    }
}
