using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// ͨ��������
/// 2024.8.12 update C
/// </summary>
public class Weapon : ScriptableObject
{
    [Header("Base")]
    public string Weapon_Name; //��������
    public Sprite Wepaon_Sprite; //������ͼ
    public int Weapon_Cost; //�����ۼ�
    public WeaponRank Weapon_Rank; //����Ʒ��
    public int ID; //����id
    [Space]
    public Bullet BulletType; //�ӵ�����
}
