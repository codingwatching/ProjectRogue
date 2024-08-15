using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Eco;
using static Data;
/// <summary>
/// Œ‰∆˜µÙ¬‰ŒÔ - øÿ÷∆µÙ¬‰ŒÔ Œ‰∆˜Ã˘Õº‘ÿ»Î “‘º∞≈ˆ◊≤ºÏ≤‚
/// 2024.8.15 update C
/// </summary>
public class DropWeapon : MonoBehaviour
{
    public SpriteRenderer dropWeaponRender;
    public BoxCollider2D TriggerBox;

    [Space]
    public Weapon dropWeapon;

    public bool isSellItem = false;

    Sprite dropSprite =null;
    int dropCost = 0;

    void Start(){
        dropWeaponRender = GetComponent<SpriteRenderer>();
    }
    void Update(){
        
    }
    public void LoadDropWeapon(Weapon weapon,bool isSell) {
        dropWeapon = weapon;
        dropSprite = dropWeapon.Wepaon_Sprite;
        dropCost = dropWeapon.Weapon_Cost;
        isSellItem = isSell;

        dropWeaponRender.sprite = dropSprite;
    }
    public void BuyWeapon() {
        instance.coin(dropCost);
        PickWeapon();
    }
    public void PickWeapon() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == PlayerLayer && !isSellItem) {
            PickWeapon();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){

    }
}