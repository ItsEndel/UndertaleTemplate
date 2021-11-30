using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour
{
    public List<charEffect> effects = new List<charEffect>();                       // �ַ�Ч���б�
    void Start() { foreach (charEffect i in effects) i.Initial(this.gameObject);}   // ��ʼ���ַ�Ч��
    void Update() { foreach (charEffect i in effects) i.Update(); }                 // ÿ֡�����ַ�Ч��
}
