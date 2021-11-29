using System;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;



public static class Tool
{
    public static System.Random Random = new System.Random();
}

public static class IniFunc
{
    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="section">段落名</param>
    /// <param name="key">键名</param>
    /// <param name="defval">读取异常是的缺省值</param>
    /// <param name="retval">键名所对应的的值，没有找到返回空值</param>
    /// <param name="size">返回值允许的大小</param>
    /// <param name="filepath">ini文件的完整路径</param>
    /// <returns></returns>
    [DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileString(
        string section,
        string key,
        string defval,
        StringBuilder retval,
        int size,
        string filepath);

    /// <summary>
    /// 写入
    /// </summary>
    /// <param name="section">需要写入的段落名</param>
    /// <param name="key">需要写入的键名</param>
    /// <param name="val">写入值</param>
    /// <param name="filepath">ini文件的完整路径</param>
    /// <returns></returns>
    [DllImport("kernel32.dll")]
    private static extern int WritePrivateProfileString(
        string section,
        string key,
        string val,
        string filepath);


    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="section">段落名</param>
    /// <param name="key">键名</param>
    /// <param name="def">没有找到时返回的默认值</param>
    /// <param name="filename">ini文件完整路径</param>
    /// <returns></returns>
    public static string getString(string section, string key, string def, string filename)
    {
        StringBuilder sb = new StringBuilder(1024);
        GetPrivateProfileString(section, key, def, sb, 1024, filename);
        return sb.ToString();
    }

    /// <summary>
    /// 写入数据
    /// </summary>
    /// <param name="section">段落名</param>
    /// <param name="key">键名</param>
    /// <param name="val">写入值</param>
    /// <param name="filename">ini文件完整路径</param>
    public static void writeString(string section, string key, string val, string filename)
    {
        WritePrivateProfileString(section, key, val, filename);
    }
}

public static class String
{
    public static Vector3 ToVector3(string i)
    {
        string[] args = i.Split(',');

        switch (args.Length)
        {
            default:
                Debug.LogWarning("错误：无法从 string 转为 Vector3（" + i + "）");
                return new Vector3();

            case 1:
                Debug.LogWarning("\"Vector3\"不包括采用 1 个参数的构造函数");
                return new Vector3();

            case 2:
                return new Vector3(float.Parse(args[0]), float.Parse(args[1]));

            case 3:
                return new Vector3(float.Parse(args[0]), float.Parse(args[1]), float.Parse(args[2]));
        }
    }

    public static Color ToColor(string i)
    {
        string[] args = i.Split(',');

        switch (args.Length)
        {
            default:
                Debug.LogWarning("错误：无法从 string 转为 Vector3（" + i + "）");
                return new Color();

            case 1:
                Debug.LogWarning("\"Color\"不包括采用 1 个参数的构造函数");
                return new Color();

            case 2:
                Debug.LogWarning("\"Color\"不包括采用 2 个参数的构造函数");
                return new Color();

            case 3:
                return new Color(float.Parse(args[0]), float.Parse(args[1]), float.Parse(args[2]));

            case 4:
                return new Color(float.Parse(args[0]), float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
        }
    }
}
