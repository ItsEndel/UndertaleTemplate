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
    public int PrintDelay = 5;                          // 打印延迟
                                                        //
    public Color CharColor = new Color(1, 1, 1, 1);     // 文字的颜色
    public int CharSize = 24;                           // 文字的尺寸
    public Font Font;                                   // 文字的字体
    public Font FontCn;                                 // 中文文字的字体
                                                        //
    public AudioClip voice;                             // 打字机音效
                                                        //
    public int CharSpace = -9;                          // 字符间距（推荐charSize*3/8）
    public int CharSpaceCn = 0;                         // 中文字符间距
    public int LineSpace = 8;                           // 行间距（推荐charSize/3*2）

    // 打字机内部变量
    private char[] silentChars = new char[] {                       // 不播放voice的字符
        ' ',
        '\n',
        ',',
        '.',
        ':',
        '，',
        '。',
        '：'
    };                                                              
                                                                    //
    private string printText;                                       // 打字机要显示的文本及文本代码
                                                                    //
    private AudioSource audioSource;                                // 音源组件
                                                                    //
    public GameObject charPrefab;                                   // 字符实例预制件
    private List<GameObject> chars = new List<GameObject>();        // 字符实例列表
                                                                    //
    private List<charEffect> charEffects = new List<charEffect>();  // 字符效果列表
                                                                    //
    private Vector3 charPos = new Vector3(0, 0, 0);                 // 下一个字符显示的位置
                                                                    //
    private int printed = 0;                                        // 已检查字数
                                                                    //
    private int delay = 0;                                          // 显示下一个字前的延迟 
                                                                    //
    private bool afterBackslash = false;                            // 是否在反斜杠后
    private bool readingCodeName = false;                           // 是否正在读取文字代码
    private bool readingCodeValue = false;                          // 是否正在读取文字代码值
                                                                    //
    private string codeName = "";                                   // 文字代码的名字
    private string codeValue = "";                                  // 文字代码的值



    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        printText = Text;
    }



    void Update()
    {
        if (printed == printText.Length) { finished = true; }
        else 
        {
            finished = false;
            if (printed < printText.Length && delay > 0) { delay--; }
            else
            {
                for (int i = printed; i < printText.Length; i++)
                {
                    printed++;

                    char c = printText[i];

                    if (readingCodeName)            // 读取文本代码名
                    {
                        if (c == '=')
                        {
                            readingCodeName = false;
                            readingCodeValue = true;
                        }
                        else
                        {
                            codeName += c;
                        }
                    }
                    else if (readingCodeValue)    // 读取文本代码值
                    {
                        if (c == ']')
                        {
                            readingCodeValue = false;
                            switch (codeName)
                            {
                                case "sleep":
                                    delay = int.Parse(codeValue);
                                    return;

                                case "delay":
                                    PrintDelay = int.Parse(codeValue);
                                    break;

                                case "color":
                                    CharColor = String.ToColor(codeValue);
                                    break;

                                case "size":
                                    CharSize = int.Parse(codeValue);
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
                                    CharSpace = int.Parse(codeValue);
                                    break;

                                case "charSpaceCn":
                                    CharSpaceCn = int.Parse(codeValue);
                                    break;

                                case "lineSpace":
                                    LineSpace = int.Parse(codeValue);
                                    break;

                                case "position":
                                    charPos = String.ToVector3(codeValue);
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
                                    Debug.LogWarning(LogWarning.unknownTextCode);
                                    break;
                            }
                        }
                        else
                        {
                            codeValue += c;
                        }
                    }
                    else if (afterBackslash)      // 打印下一个字符
                    {
                        Print(c);
                        afterBackslash = false;
                        return;
                    }
                    else                          // 检测字符
                    {
                        switch (c)
                        {
                            case '\n':      // 换行
                                charPos.x = 0;
                                charPos.y -= CharSize + LineSpace;
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
        
    }



    private void Print(char c)
    {
        bool contains = false;

        GameObject charInstance = Instantiate(charPrefab, this.transform);
        Character charScript = charInstance.GetComponent<Character>();
        Text charText = charInstance.GetComponent<Text>();
        charText.text = c.ToString();
        charText.color = CharColor;
        charText.fontSize = CharSize;
        charText.font = (c > 32 && c < 127) ? Font : FontCn;
        charInstance.transform.position = charPos + this.transform.position;
        charInstance.transform.Translate(320, -240, 0);

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

        charPos.x += (c > 32 && c < 127) ? (CharSize + CharSpace) : (CharSize + CharSpaceCn);

        delay = PrintDelay;

        foreach (char i in silentChars) { if (i == c) contains = true; }

        if (!contains) audioSource.PlayOneShot(audioSource.clip);
    }

    public bool Finished()
    {
        return finished;
    }
}
