using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static PlayerData;
/// <summary>
/// 近战系统
/// 2024.8.17 update C
/// </summary>
public class MeleeSystem : MonoBehaviour
{
    public MeleeWeapon meleeWeapon;
    public GameObject meleeHitBox;
    public MeleeHeld meleeHeld;

    public bool HeldMeleeWeapon = false;

    public float HitBoxRange;
    public float MeleeDamage;
    public float HitFrequency;

    int index = 1;

    void Start(){
        loadMeleeWeapon(meleeWeapon);
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.F)) {
            combatMelee();
        }
    }
    public void loadMeleeWeapon(MeleeWeapon weapon) {
        meleeWeapon = weapon;
        HitBoxRange = weapon.MeleeWeapon_HitBoxRange;
        MeleeDamage = weapon.MeleeWeapon_Damage;
        HitFrequency = weapon.MeleeWeapon_HitFrequency;
        meleeHeld.loadMeleeAnchor(weapon);
    }
    public void onMeleeHit(GameObject obj,Vector3 pos) {
        obj.SendMessage("hurt", MeleeDamage);
        Shaker.instance.genShake(0.05f);
    }
    public void combatMelee() {
        index = -index;
        banAction(true);
        transform.DORotate(new Vector3(0, 0, forward * 225 * index), HitFrequency, RotateMode.WorldAxisAdd).SetEase(Ease.InOutExpo)
            .OnComplete(() => {
                transform.DOShakePosition(0.1f, new Vector3(0.1f, 0), 5, 90, false);
                banAction(false);
            });
    }
    public void banAction(bool val) {
        banRotate = val;
        banTurn = val;
        meleeHitBox.SetActive(val);
    }
}
