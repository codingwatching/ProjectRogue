using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ǹ
[CreateAssetMenu(fileName = "new ShotGun", menuName = "Weapon/new ShotGunWeapon")]
public class ShotGun : UniversalWeapon
{
    [Header("ShotGun")]
    [Tooltip("�ӵ���ɢ����")]
    public int Weapon_SpreadNum;
}
