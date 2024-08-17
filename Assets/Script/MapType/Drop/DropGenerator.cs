using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// 掉落物生成器
/// 2024.8.17 update C
/// </summary>
public class DropGenerator : MonoBehaviour
{
    public static DropGenerator instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }
    public DropURPSprite URPSprite;
    public AnimaCurve curve;

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
    public GameObject getDrop() {
        GameObject drop = null;
        drop = BulletPool.GameObjectPoolManager.Instance.Get("DropUR");
        return drop;
    }
    public void reduceDrop(GameObject gameObject) {
        //Destroy(gameObject.GetComponent<DropWeapon>());
        //Destroy(gameObject.GetComponent<DropItem>());
        BulletPool.GameObjectPoolManager.Instance.Recycle("DropUR", gameObject);
    }
    //生成武器掉落物 默认可捡起
    public void DropWeapon(Vector2 pos, Weapon weapon) {
        GameObject coin = new GameObject() { name = "Weapon:"+weapon.Weapon_Name };
        coin.transform.position = pos;
        coin.AddComponent<SpriteRenderer>().sortingOrder=3;
        coin.AddComponent<DropWeapon>().LoadDropWeapon(weapon,false);
        coin.AddComponent<DropMove>().genDynamicDrop(curve.animaCurve);
    }
    //生成硬币 默认动态掉落
    public void DropCoin(Vector2 pos,int num) {
        //GameObject coin = new GameObject() { name = "Coin" };
        GameObject coin = getDrop();
        coin.transform.position = pos;
        coin.AddComponent<DropItem>().LoadDropItem(URPSprite.sprites[2].sprite, DropItemType.Coin, num);
        //coin.GetComponent<DropMove>().genDynamicDrop(curve.animaCurve);
    }
    //生成血 默认动态掉落
    public void DropBlood(Vector2 pos,int num) {
        GameObject blood = getDrop();
        blood.transform.position = pos;
        blood.AddComponent<DropItem>().LoadDropItem(URPSprite.sprites[1].sprite, DropItemType.Blood, num);
        //blood.GetComponent<DropMove>().genDynamicDrop(curve.animaCurve);
    }
}
