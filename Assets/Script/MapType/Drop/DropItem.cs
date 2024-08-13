using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// 物品掉落物 - 控制掉落物 物品贴图载入 以及碰撞检测 
/// 类型：血，硬币 /待定
/// 2024.8.13 update C
/// </summary>
public class DropItem : MonoBehaviour
{
    public SpriteRenderer dropItemRender;
    public BoxCollider2D TriggerBox;
    [Space]
    public DropItemType local_type;
    public DropURPSprite local_sprites;
    public int local_DropNum;

    Sprite dropSprite =null;

    void Start(){
        
    }
    void Update(){
        
    }
    public void LoadDropItem(DropURPSprite sprites, DropItemType type , int dropNum) {
        local_type = type;
        local_sprites = sprites;
        local_DropNum = dropNum;

        foreach (var val in local_sprites.sprites) { 
            if(val.type == local_type) {
                dropSprite = val.sprite;
            }
        }

        dropItemRender.sprite = dropSprite;
    }
    public void PickCoin() {
        Eco.instance.coin(+local_DropNum);
    }
    public void PickBlood() { 

    }
    public void PickFunc() { 
        if(local_type == DropItemType.Blood) {
            PickBlood();
        }else if(local_type == DropItemType.Coin) {
            PickCoin();
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == PlayerLayer) {
            PickFunc();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){

    }
}
