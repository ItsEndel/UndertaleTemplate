using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour
{
    public List<charEffect> effects = new List<charEffect>();                       // 字符效果列表
    void Start() { foreach (charEffect i in effects) i.Initial(this.gameObject);}   // 初始化字符效果
    void Update() { foreach (charEffect i in effects) i.Update(); }                 // 每帧更新字符效果
}
