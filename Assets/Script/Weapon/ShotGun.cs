using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ö±µ¯Ç¹
[CreateAssetMenu(fileName = "new ShotGun", menuName = "Weapon/new ShotGunWeapon")]
public class ShotGun : UniversalWeapon
{
    [Header("ShotGun")]
    [Tooltip("×Óµ¯À©É¢ÊýÁ¿")]
    public int Weapon_SpreadNum;
}
