using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ½üÕ½Àà
/// 2024.8.17 update C
/// </summary>
[CreateAssetMenu(fileName = "new Weapon", menuName = "Weapon/new MeleeWeapon")]
public class MeleeWeapon : Weapon
{
    [Header("MeleeWeapon")]
    public float MeleeWeapon_HitBoxRange;
    public float MeleeWeapon_Damage;
    public float MeleeWeapon_HitFrequency;
}
