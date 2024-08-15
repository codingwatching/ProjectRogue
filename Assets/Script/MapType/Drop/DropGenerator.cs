using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// ������������
/// 2024.8.15 update C
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
    //�������������� Ĭ�Ͽɼ���
    public void DropWeapon(Vector2 pos, Weapon weapon) {
        GameObject coin = new GameObject() { name = "Weapon:"+weapon.Weapon_Name };
        coin.transform.position = pos;
        coin.AddComponent<SpriteRenderer>().sortingOrder=3;
        coin.AddComponent<DropWeapon>().LoadDropWeapon(weapon,false);
        coin.AddComponent<DropMove>().genDynamicDrop(curve.animaCurve);
    }
    //����Ӳ�� Ĭ�϶�̬����
    public void DropCoin(Vector2 pos,int num) {
        GameObject coin = new GameObject() { name = "Coin" };
        coin.transform.position = pos;
        coin.AddComponent<SpriteRenderer>().sortingOrder = 3;
        coin.AddComponent<DropItem>().LoadDropItem(URPSprite.sprites[2].sprite, DropItemType.Coin, num);
        coin.AddComponent<DropMove>().genDynamicDrop(curve.animaCurve);
    }
    //����Ѫ Ĭ�϶�̬����
    public void DropBlood(Vector2 pos,int num) {
        GameObject coin = new GameObject() { name = "Blood" };
        coin.transform.position = pos;
        coin.AddComponent<SpriteRenderer>().sortingOrder = 3;
        coin.AddComponent<DropItem>().LoadDropItem(URPSprite.sprites[1].sprite, DropItemType.Blood, num);
        coin.AddComponent<DropMove>().genDynamicDrop(curve.animaCurve);
    }
}
