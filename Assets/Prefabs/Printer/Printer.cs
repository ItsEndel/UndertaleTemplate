using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    // ���ֻ�����
    public string text = "";

    public Font font;
    public Color color = new Color(1, 1, 1, 1);

    // ���ֻ��ڲ�����
    private int printed = 0;

    private int printDelay = 1;

    private bool inBrackets = false;

    void Update()
    {
        
    }
}
