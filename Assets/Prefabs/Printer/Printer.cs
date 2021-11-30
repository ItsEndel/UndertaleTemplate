using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Printer : MonoBehaviour
{
    // ���ֻ�����
    private bool finished = false;                      // �Ƿ��ӡ���
                                                        //
    public string Text;                                 // ���ֻ�Ҫ��ʾ���ı����ı�����
                                                        //
    public int PrintDelay = 50;                         // ��ӡ�ӳ�
                                                        //
    public Color CharColor = new Color(1, 1, 1, 1);     // ���ֵ���ɫ
    public int CharSize = 24;                           // ���ֵĳߴ�
    public Font Font;                                   // ���ֵ�����
    public Font FontCn;                                 // �������ֵ�����
                                                        //
    public AudioClip voice;                             // ���ֻ���Ч
                                                        //
    public int CharSpace = -9;                          // �ַ���ࣨ�Ƽ�charSize*3/8��
    public int CharSpaceCn = 0;                         // �����ַ����
    public int LineSpace = 16;                          // �м��

    // ���ֻ��ڲ�����
    private string printText;

    private AudioSource audioSource;                                // ��Դ���
                                                                    //
    public GameObject charPrefab;                                   // �ַ�ʵ��Ԥ�Ƽ�
    private List<GameObject> chars = new List<GameObject>();        // �ַ�ʵ���б�
                                                                    //
    private List<charEffect> charEffects = new List<charEffect>();  // �ַ�Ч���б�
                                                                    //
    private bool textSet = false;                                   // �ı��Ƿ����ù�
                                                                    //
    private Vector3 charPos = new Vector3(0, 0, 0);                 // ��һ���ַ���ʾ��λ��
                                                                    //
    private int printed = 0;                                        // �Ѽ������
                                                                    //
    private int delay = 0;                                          // ��ʾ��һ����ǰ���ӳ� 
    private bool afterBackslash = false;                            // �Ƿ��ڷ�б�ܺ�
    private bool readingCodeName = false;                           // �Ƿ����ڶ�ȡ���ִ���
    private bool readingCodeValue = false;                          // �Ƿ����ڶ�ȡ���ִ���ֵ
                                                                    //
    private string codeName = "";                                   // ���ִ��������
    private string codeValue = "";                                  // ���ִ����ֵ



    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        printText = Text;
    }



    void Update()
    {
        if (printed < printText.Length + 1 && delay > 0)
        {
            delay--;
        } else
        {
            for (int i = printed; i < printText.Length; i++)
            {
                printed++;

                char c = printText[i];

                if (readingCodeName)            // ��ȡ�ı�������
                {
                    if (c == '=')
                    {
                        readingCodeName = false;
                        readingCodeValue = true;
                    } else
                    {
                        codeName += c;
                    }
                } else if (readingCodeValue)    // ��ȡ�ı�����ֵ
                {
                    if (c == ']')
                    {
                        readingCodeValue = false;
                        switch (codeName)
                        {
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
                } else if (afterBackslash)      // ��ӡ��һ���ַ�
                {
                    Print(c);
                    afterBackslash = false;
                    return;
                } else                          // ����ַ�
                {
                    switch (c)
                    {
                        case '\n':      // ����
                            charPos.x = 0;
                            charPos.y -= CharSize + LineSpace;
                            break;

                        case '/':       // ǿ�ƴ�ӡ��һ���ַ�
                            afterBackslash = true;
                            break;

                        case '[':       // ��ȡ�ı�����
                            codeName = "";
                            codeValue = "";
                            readingCodeName = true;
                            break;

                        default:        // ��ӡ
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
        charText.color = CharColor;
        charText.fontSize = CharSize;
        charText.font = (c > 32 && c < 127) ? Font : FontCn;
        charInstance.transform.position = charPos;
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

        if (c != ' ') audioSource.PlayOneShot(audioSource.clip);
    }

    public bool Finished()
    {
        return finished;
    }
}
