using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ScriptableObject
{
    [Header("Public")]
    [Tooltip("�ӵ�����")] 
    public string Bullet_Name;
    [Tooltip("�ӵ���ײ�䷶Χ")] 
    public float Bullet_HitBoxRange;
    [Space]
    [Tooltip("�ӵ���ͼ��")] 
    public Sprite[] Bullet_Sprites;
    [Tooltip("�ӵ���ͼ�л�Ƶ��")]
    public float Bullet_SpriteFrequency;
    [Space]
    [Tooltip("�Ƿ�������ʵ��")] 
    public bool enableEntityGen;
    [Tooltip("��ʵ�������б�")] 
    public Bullet_SubEntity Bullet_SubEntity;
    [Space]
    [Tooltip("�Ƿ񷴵�")] 
    public bool enableBounce;
    [Tooltip("�Ƿ�͸")] 
    public bool enablePierce;
}
