using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// 掉落物生成器
/// 2024.8.15 update C
/// </summary>
public class DropGenerator : MonoBehaviour
{
    public DropURPSprite URPSprite;
    void Start(){
        
    }
    void Update(){
        
    }
    public void Drop(Vector2 pos,Drop drop) { 
        if(drop.itemType == DropItemType.Weapon) {
            DropWeapon(pos,drop.dropWeapon);
        }
        if(drop.itemType == DropItemType.Blood) {
            DropBlood(pos,drop.itemData);
        }
        if(drop.itemType == DropItemType.Coin) {
            DropCoin(pos,drop.itemData);
        }
    }
    //生成武器掉落物 默认可捡起
    public void DropWeapon(Vector2 pos, Weapon weapon) {
        GameObject coin = new GameObject() { name = "Weapon:"+weapon.Weapon_Name };
        coin.transform.position = pos;
        coin.AddComponent<DropWeapon>().LoadDropWeapon(weapon,false);
        coin.AddComponent<DropMove>().genDynamicDrop();
    }
    //生成硬币 默认动态掉落
    public void DropCoin(Vector2 pos,int num) {
        GameObject coin = new GameObject() { name = "Coin" };
        coin.transform.position = pos;
        coin.AddComponent<DropItem>().LoadDropItem(URPSprite, DropItemType.Coin, num);
        coin.AddComponent<DropMove>().genDynamicDrop();
    }
    //生成血 默认动态掉落
    public void DropBlood(Vector2 pos,int num) {
        GameObject coin = new GameObject() { name = "Blood" };
        coin.transform.position = pos;
        coin.AddComponent<DropItem>().LoadDropItem(URPSprite, DropItemType.Blood, num);
        coin.AddComponent<DropMove>().genDynamicDrop();
    }
}
