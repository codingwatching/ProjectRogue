using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// ������������
/// 2024.8.13 update C
/// </summary>
public class DropGenerator : MonoBehaviour
{
    public DropURPSprite URPSprite;
    void Start(){
        
    }
    void Update(){
        
    }
    //�������������� Ĭ�Ͽɼ���
    public void DropWeapon(Vector2 pos, Weapon weapon) {
        GameObject coin = new GameObject() { name = "Weapon:"+weapon.Weapon_Name };
        coin.transform.position = pos;
        coin.AddComponent<DropWeapon>().LoadDropWeapon(weapon,false);
        coin.AddComponent<Drop>().genDynamicDrop();
    }
    //����Ӳ�� Ĭ�϶�̬����
    public void DropCoin(Vector2 pos,int num) {
        GameObject coin = new GameObject() { name = "Coin" };
        coin.transform.position = pos;
        coin.AddComponent<DropItem>().LoadDropItem(URPSprite, DropItemType.Coin, num);
        coin.AddComponent<Drop>().genDynamicDrop();
    }
    //����Ѫ Ĭ�϶�̬����
    public void DropBlood(Vector2 pos,int num) {
        GameObject coin = new GameObject() { name = "Blood" };
        coin.transform.position = pos;
        coin.AddComponent<DropItem>().LoadDropItem(URPSprite, DropItemType.Blood, num);
        coin.AddComponent<Drop>().genDynamicDrop();
    }
}