using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// 物品掉落物 - 控制掉落物 物品贴图载入 以及碰撞检测 
/// 类型：血，硬币 /待定
/// 2024.8.17 update C
/// </summary>
public class DropItem : MonoBehaviour
{
    public SpriteRenderer dropItemRender;
    public BoxCollider2D TriggerBox;
    [Space]
    public DropItemType local_type;
    public Sprite local_sprites;
    public int local_DropNum;

    Sprite dropSprite =null;

    void Start(){
        dropItemRender = GetComponent<SpriteRenderer>();
    }
    void Update(){
        
    }
    public void LoadDropItem(Sprite sprite, DropItemType type , int dropNum) {
        local_type = type;
        local_sprites = sprite;
        local_DropNum = dropNum;

        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void PickCoin() {
        //Eco.instance.coin(+local_DropNum);
        removeFunc();
    }
    public void PickBlood() {
        removeFunc();
    }
    public void PickFunc() { 
        if(local_type == DropItemType.Blood) {
            PickBlood();
        }else if(local_type == DropItemType.Coin) {
            PickCoin();
        }
    }
    public void removeFunc() {
        GetComponent<DropMove>().resetParameter();
        DropGenerator.instance.reduceDrop(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == PlayerLayer) {
            PickFunc();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){

    }
}
