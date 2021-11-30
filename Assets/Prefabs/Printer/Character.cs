using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;



public class Character : MonoBehaviour
{
    public List<charEffect> effects = new List<charEffect>();

    void Start() { foreach (charEffect i in effects) i.Initial(this.gameObject); Debug.Log(Time.deltaTime); }

    void Update() { foreach (charEffect i in effects) i.Update(); }
}



public class charEffect {                           
    public GameObject obj;                                                      // 字符对象
    public Dictionary<string, string> args = new Dictionary<string, string>();  // 自动读取的参数
    public void Read(string value)
    {
        Regex regex = new Regex("([A-Za-z]+):([0-9.]+)");
        MatchCollection match = regex.Matches(value);
        foreach (Match a in match)
        {
            GroupCollection groups = a.Groups;

            args.Add(groups[1].Value, groups[2].Value);
        };
    }                                   // 读取参数
    public virtual void Initial(GameObject it) { obj = it; }                    // 初始化
    public virtual void Update() { }                                            // 每帧更新
}  // 字符效果基类

public class trembleEffect : charEffect
{


    private float level = 5;
    private int delay = 15;

    private int timer = 0;

    Vector3 position;

    public override void Initial(GameObject it) {
        base.Initial(it);

        if (args.ContainsKey("level")) { level = float.Parse(args["level"]); }
        if (args.ContainsKey("delay")) { delay = int.Parse(args["delay"]); }

        position = it.transform.localPosition;

        timer = delay;
    }

    public override void Update()
    {
        if (timer > 0)
        {
            timer--;
        } else
        {
            float a = ((float)Tool.Random.NextDouble() * (level * 2)) - level;
            float b = ((float)Tool.Random.NextDouble() * (level * 2)) - level;

            obj.transform.localPosition = new Vector3((position.x + a), (position.y + b), 0);

            timer = delay;
        }
    }
}
