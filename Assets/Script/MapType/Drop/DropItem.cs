using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// ��Ʒ������ - ���Ƶ����� ��Ʒ��ͼ���� �Լ���ײ��� 
/// ���ͣ�Ѫ��Ӳ�� /����
/// 2024.8.13 update C
/// </summary>
public class DropItem : MonoBehaviour
{
    public SpriteRenderer dropItemRender;
    public BoxCollider2D TriggerBox;
    public DropItemType local_type;
    public DropURPSprite local_sprites;

    [Space]
    public int DropNum;
    Sprite dropSprite =null;

    void Start(){
        
    }
    void Update(){
        
    }
    public void LoadDropItem(DropURPSprite sprites, DropItemType type) {
        local_type = type;
        local_sprites = sprites;

        foreach(var val in local_sprites.sprites) { 
            if(val.type == local_type) {
                dropSprite = val.sprite;
            }
        }

        dropItemRender.sprite = dropSprite;
    }
    public void PickWeapon() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == PlayerLayer) {
            PickWeapon();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){

    }
}
