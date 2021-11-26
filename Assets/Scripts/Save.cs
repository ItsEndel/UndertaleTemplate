using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveProfile
{
    // 玩家名称
    public string name;

    // 玩家所在房间
    public int room;
}

public class IniSaveProfile
{
    public class General
    {
        // 玩家数据
        public string Room;
        public string Kills;
        public string Time;
        public string Love;
        public string Name;

        // 彩蛋值
        public string fun = "100";
    }

    public class Flowey
    {
        // 第一次遇见的情况（全接/全躲）
        public int Met1;
    }
}
