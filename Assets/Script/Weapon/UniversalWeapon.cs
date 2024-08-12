using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ͨ��ǹе��
/// 2024.8.12 update C
/// </summary>
[CreateAssetMenu(fileName = "new Weapon", menuName = "Weapon/new UniversalWeapon")]
public class UniversalWeapon : Weapon
{
    [Header("UniversalWeapon")]
    [Tooltip("�����˺�")]
    public float Weapon_Damage;
    [Tooltip("�ӵ�����")]
    public float Weapon_BulletSpeed;
    [Tooltip("�������Ƶ��")]
    public float Weapon_Frequency;
    [Tooltip("�ӵ���ɢ���Ƕ�")]
    public float Weapon_Range;
    [Tooltip("��������������ѡ��")]
    public float Weapon_RepelForce;
    [Tooltip("�Ƿ�����")]
    public bool Weapon_EnableAuto;
    [Tooltip("�������� (0 - 1.0 defualt 0.1f)")]
    public bool Weapon_ShakeForce;
}
