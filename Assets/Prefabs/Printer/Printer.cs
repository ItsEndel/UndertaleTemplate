using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Printer : MonoBehaviour
{
    // 打字机参数
    private bool finished = false;                      // 是否打印完成
                                                        //
    public string Text;                                 // 打字机要显示的文本及文本代码
                                                        //
    public Color charColor = new Color(1, 1, 1, 1);     // 文字的颜色
    public int charSize = 24;                           // 文字的尺寸
    public Font Font;                                   // 文字的字体
    public Font FontCn;                                 // 中文文字的字体
                                                        //
    public AudioClip voice;                             // 打字机音效
                                                        //
    public int charSpace = -9;                          // 字符间距（推荐charSize*3/8）
    public int charSpaceCn = 0;                         // 中文字符间距
    public int lineSpace = 16;                          // 行间距

    // 打字机内部变量
    private AudioSource audioSource;                                // 音源组件
                                                                    //
    public GameObject charPrefab;                                   // 字符实例预制件
    private List<GameObject> chars = new List<GameObject>();        // 字符实例列表
                                                                    //
    private List<charEffect> charEffects = new List<charEffect>();  // 字符效果列表
                                                                    //
    private bool textSet = false;                                   // 文本是否被设置过
                                                                    //
    private Vector3 charPos = new Vector3(0, 0, 0);            // 下一个字符显示的位置
                                                                    //
    private int printed = 0;                                        // 已检查字数
                                                                    //
    private int printDelay = 50;                                    // 打印延迟
                                                                    //
    private int delay = 0;                                          // 显示下一个字前的延迟 
    private bool afterBackslash = false;                            // 是否在反斜杠后
    private bool readingCodeName = false;                           // 是否正在读取文字代码
    private bool readingCodeValue = false;                          // 是否正在读取文字代码值
                                                                    //
    private string codeName = "";                                   // 文字代码的名字
    private string codeValue = "";                                  // 文字代码的值



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



    void Update()
    {
        if (printed < Text.Length + 1 && delay > 0)
        {
            delay--;
        } else
        {
            for (int i = printed; i < Text.Length; i++)
            {
                printed++;

                char c = Text[i];

                if (readingCodeName)            // 读取文本代码名
                {
                    if (c == '=')
                    {
                        readingCodeName = false;
                        readingCodeValue = true;
                    } else
                    {
                        codeName += c;
                    }
                } else if (readingCodeValue)    // 读取文本代码值
                {
                    if (c == ']')
                    {
                        readingCodeValue = false;
                        switch (codeName)
                        {
                            case "color":
                                charColor = String.ToColor(codeValue);
                                break;

                            case "size":
                                charSize = int.Parse(codeValue);
                                break;

                            case "font":
                                Font = Resources.Load<Font>(codeValue);
                                break;

                            case "fontCn":
                                FontCn = Resources.Load<Font>(codeValue);
                                break;

                            case "voice":
                                voice = Resources.Load<AudioClip>(codeValue);
                                audioSource.clip = voice;
                                break;

                            case "charSpace":
                                charSpace = int.Parse(codeValue);
                                break;

                            case "charSpaceCn":
                                charSpaceCn = int.Parse(codeValue);
                                break;

                            case "lineSpace":
                                lineSpace = int.Parse(codeValue);
                                break;

                            case "tremble":
                                trembleEffect effect = new trembleEffect();
                                effect.Read(codeValue);
                                charEffects.Add(effect);
                                break;

                            case "/tremble":
                                int index = charEffects.FindIndex(i => i is trembleEffect);
                                charEffects.RemoveAt(index);
                                break;

                            default:
                                Debug.LogWarning(LogMessages.unknownTextCode);
                                break;
                        }
                    } else
                    {
                        codeValue += c;
                    }
                } else if (afterBackslash)      // 打印下一个字符
                {
                    Print(c);
                    afterBackslash = false;
                    return;
                } else                          // 检测字符
                {
                    switch (c)
                    {
                        case '\n':      // 换行
                            charPos.x = 0;
                            charPos.y -= charSize + lineSpace;
                            break;

                        case '/':       // 强制打印下一个字符
                            afterBackslash = true;
                            break;

                        case '[':       // 读取文本代码
                            codeName = "";
                            codeValue = "";
                            readingCodeName = true;
                            break;

                        default:        // 打印
                            Print(c);
                            return;
                    }
                }
            }
        }
    }



    private void Print(char c)
    {
        GameObject charInstance = Instantiate(charPrefab, this.transform);
        Character charScript = charInstance.GetComponent<Character>();
        Text charText = charInstance.GetComponent<Text>();
        charText.text = c.ToString();
        charText.color = charColor;
        charText.fontSize = charSize;
        charText.font = (c > 32 && c < 127) ? Font : FontCn;
        charInstance.transform.position = charPos;

        List<charEffect> effects = new List<charEffect>();
        foreach (charEffect a in charEffects)
        {
            if (a is trembleEffect)
            {
                trembleEffect effect = new trembleEffect();
                effect.args = a.args;
                effects.Add(effect);
            }
        }
        charScript.effects = effects;

        charPos.x += (c > 32 && c < 127) ? (charSize + charSpace) : (charSize + charSpaceCn);

        delay = printDelay;

        if (c != ' ') audioSource.PlayOneShot(audioSource.clip);
    }

    public bool Finished()
    {
        return finished;
    }
}
