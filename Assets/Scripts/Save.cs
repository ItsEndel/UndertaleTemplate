using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Save
{
    public string name;     // 玩家名称

    public int room;        // 玩家所在房间

    public int exp;         // 玩家经验
}

public static class IniSave
{
    /* Ini File
     * ------------------------------
     * [General]
     * Room = ""   // 房间ID
     * Kills = ""  // 杀死怪物数
     * Time = ""   // 游玩时间（秒）
     * Love = ""   // 暴力指数
     * Name = ""   // 名字
     * 
     * fun = ""    // FUN?
     * 
     * [Flowey]
     * Met1 = ""
     * 
     */

    public static string Path = Application.persistentDataPath + "/undertale.ini";

    public static string Read(string section, string key)
    {
        return IniFunc.getString(section, key, "0", Path);
    }

    public static void Write(string section, string key, string value)
    {
        IniFunc.writeString(section, key, value, Path);
    }
}
