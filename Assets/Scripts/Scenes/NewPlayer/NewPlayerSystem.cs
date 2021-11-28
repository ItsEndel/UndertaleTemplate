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

        printer.Text = "这是一个[color=0,1,0]Test[color=1,1,1]文本。.\n\n[size=32][charSpace=-12]并且还能变成/[/[BIG SHOT]]";
        printer.charSize = 24;
        printer.charSpace = -9;

    }
}
