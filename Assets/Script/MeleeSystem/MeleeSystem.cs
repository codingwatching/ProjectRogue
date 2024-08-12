using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static PlayerData;
/// <summary>
/// 近战系统 待完善
/// 2024.8.12 update C
/// </summary>
public class MeleeSystem : MonoBehaviour
{
    public MeleeWeapon meleeWeapon;
    public GameObject MeleeGameObj;

    public bool HeldMeleeWeapon = false;

    public float HitBoxRange;
    public float MeleeDamage;
    public float HitFrequency;

    int index = 1;

    void Start(){
        
    }
    void Update(){
        //Debug.Log(forward);
        if (Input.GetKeyDown(KeyCode.F)) {
            combatMelee();
        }
    }
    public void loadMeleeWeapon(MeleeWeapon weapon) {
        meleeWeapon = weapon;
        HitBoxRange = weapon.MeleeWeapon_HitBoxRange;
        MeleeDamage = weapon.MeleeWeapon_Damage;
        HitFrequency = weapon.MeleeWeapon_HitFrequency;
    }
    public void combatMelee() {
        index = -index;
        banAction(true);
        transform.DORotate(new Vector3(0, 0, forward * 225 * index), 0.3f, RotateMode.WorldAxisAdd).SetEase(Ease.InOutExpo)
            .OnComplete(() => {
                transform.DOShakePosition(0.1f, new Vector3(0.1f, 0), 5, 90, false);
                banAction(false);
            });
    }
    public void banAction(bool val) {
        banRotate = val;
        banTurn = val;
    }
}
