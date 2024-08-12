using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;

public class Chester : MonoBehaviour
{
    public Drop DropItem;
    public BoxCollider2D TriggerBox;

    public bool isTriggerChester = false;

    void Start(){
        
    }
    void Update(){
        
    }
    public virtual void openChester() { }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == PlayerLayer) {
            isTriggerChester = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == PlayerLayer) {
            isTriggerChester = false;
        }
    }
}
