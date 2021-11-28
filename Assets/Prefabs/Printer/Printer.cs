using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    // 打字机参数
    public string text = "";

    public Font font;
    public Color color = new Color(1, 1, 1, 1);

    // 打字机内部变量
    private int printed = 0;

    private int printDelay = 1;

    private bool inBrackets = false;

    void Update()
    {
        
    }
}
