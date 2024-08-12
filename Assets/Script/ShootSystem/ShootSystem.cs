using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ∑¢…‰œµÕ≥
/// 2024.8.12 update C
/// </summary>
public class ShootSystem : MonoBehaviour
{
    public UniversalWeapon defaultWeapon;
    public GameObject FirePos;
    public Weapon Weapon_Current;

    private Bullet GetBullet;

    [Tooltip("Universal")]
    float WeaponCurrent_Damage;
    float WeaponCurrent_BulletSpeed;
    float WeaponCurrent_Frequency;
    float WeaponCurrent_Range;
    float WeaponCurrent_RepelForce;
    bool WeaponCurrent_EnableAuto;
    [Tooltip("ShotGun")]
    int WeaponCurrent_SpreadNum;

    void Start(){
        loadGunWeapon(defaultWeapon);
        checkGunWeaponType(defaultWeapon);
    }
    void Update(){
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }
    public void shoot() {
        var bullets = checkBulletType();
        loadBulletData(bullets);
        bullets.transform.position = FirePos.transform.position;
        bullets.name = "Bullet_moving";
        bullets.GetComponent<Bullet_BaseNode>().ShootFunc();
        Shaker.instance.genShake(0.1f);
    }
    public void loadGunWeapon(UniversalWeapon weapon) {
        Weapon_Current = weapon;
        GetBullet = weapon.BulletType;
    }
    public void checkGunWeaponType(UniversalWeapon weapon) {
        WeaponCurrent_Damage = weapon.Weapon_Damage;
        WeaponCurrent_BulletSpeed = weapon.Weapon_BulletSpeed;
        WeaponCurrent_Frequency = weapon.Weapon_Frequency;
        WeaponCurrent_Range = weapon.Weapon_Range;
        WeaponCurrent_RepelForce = weapon.Weapon_RepelForce;

        if(weapon is ShotGun) {
            var shotgun = (ShotGun)weapon;
            WeaponCurrent_SpreadNum = shotgun.Weapon_SpreadNum;
        }
    }
    public GameObject checkBulletType() {
        GameObject bullet = null;
        if (GetBullet is Bullet_Universal) {
            bullet = BulletPool.GameObjectPoolManager.Instance.Get("BulletUR");
        }
        return bullet;
    }
    public GameObject loadBulletData(GameObject bullet) {
        var bulletScript = bullet.GetComponent<Bullet_BaseNode>();

        bulletScript.BulletDamage = WeaponCurrent_Damage;
        bulletScript.BulletSpeed = WeaponCurrent_BulletSpeed;
        bulletScript.BulletSpreadRange = Random.Range(-WeaponCurrent_Range, WeaponCurrent_Range);

        bulletScript.BulletSpriteLoopFrequency = GetBullet.Bullet_SpriteFrequency;
        bulletScript.BulletSprites = GetBullet.Bullet_Sprites;

        return bullet;
    }
}
