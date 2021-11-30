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

        printer.Text = "[delay=30][size=24][charSpace=-9][tremble=level:1,delay:5]//w\\";
    }
}
