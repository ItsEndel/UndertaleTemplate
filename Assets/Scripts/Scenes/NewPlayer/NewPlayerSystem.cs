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
        GameObject printerInstance = Instantiate<GameObject>(printerPrefab,new Vector3(0, 0, 0), new Quaternion(), GameObject.Find("UI").transform);
        Printer printer = printerInstance.GetComponent<Printer>();

        printer.Text = "[size=24][charSpace=-9]这是一段[color=0,1,0][tremble=level:7.5,delay:5][voice=voc_evil]Test[/tremble=][color=1,1,1]文本.";
    }
}
