using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// ½üÕ½Åö×²Ïä
/// 2024.8.17 update C
/// </summary>
public class MeleeHitBox : MonoBehaviour
{
    public BoxCollider2D hitBox;
    public MeleeSystem meleeSystem;
    void Start(){
        
    }
    void Update(){
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.layer == EnemyLayer) {
            meleeSystem.onMeleeHit(collision.gameObject, collision.transform.position);
        }
    }
}
