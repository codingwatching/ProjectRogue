using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// 通用武器类
/// 2024.8.12 update C
/// </summary>
public class Weapon : ScriptableObject
{
    [Header("Base")]
    public string Weapon_Name; //武器名字
    public Sprite Wepaon_Sprite; //武器贴图
    public int Weapon_Cost; //武器售价
    public WeaponRank Weapon_Rank; //武器品级
    public int ID; //武器id
    [Space]
    public Bullet BulletType; //子弹类型
}
