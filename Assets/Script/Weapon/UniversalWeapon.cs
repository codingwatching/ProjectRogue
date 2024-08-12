using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 通用枪械类
/// 2024.8.12 update C
/// </summary>
[CreateAssetMenu(fileName = "new Weapon", menuName = "Weapon/new UniversalWeapon")]
public class UniversalWeapon : Weapon
{
    [Header("UniversalWeapon")]
    [Tooltip("武器伤害")]
    public float Weapon_Damage;
    [Tooltip("子弹移速")]
    public float Weapon_BulletSpeed;
    [Tooltip("武器射击频率")]
    public float Weapon_Frequency;
    [Tooltip("子弹扩散最大角度")]
    public float Weapon_Range;
    [Tooltip("武器后坐力（可选）")]
    public float Weapon_RepelForce;
    [Tooltip("是否连发")]
    public bool Weapon_EnableAuto;
    [Tooltip("抖动力度 (0 - 1.0 defualt 0.1f)")]
    public bool Weapon_ShakeForce;
}
