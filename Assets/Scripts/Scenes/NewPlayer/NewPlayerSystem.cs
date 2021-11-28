using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerSystem : MonoBehaviour
{
    public GameObject printerPrefab;
    
    void Start()
    {
        Invoke("wait", 1);
    }

    

    void Update()
    {
        
    }

    void wait()
    {
        GameObject printerInstance = Instantiate<GameObject>(printerPrefab, GameObject.Find("UI").transform);
        Printer printer = printerInstance.GetComponent<Printer>();

        printer.Text = "����һ��[color=0,1,0]Test[color=1,1,1]�ı���.\n\n[size=32][charSpace=-12]���һ��ܱ��/[/[BIG SHOT]]";
        printer.charSize = 24;
        printer.charSpace = -9;

    }
}
