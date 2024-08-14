using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// ×Ô¶¯Ë÷µÐ½Å±¾
/// 2024.08.14 C
/// </summary>
public class AutoAim : MonoBehaviour
{
    public List<GameObject> TriggerEnemyList = new List<GameObject>();
    void Start(){
        
    }
    void Update(){
        
    }
    public GameObject GetNearlyEnemy() {
        GameObject nearlyEnemy = null;
        foreach(var val in TriggerEnemyList) { 

        }
        return nearlyEnemy;
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == EnemyLayer) {
            TriggerEnemyList.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == EnemyLayer) {
            TriggerEnemyList.Remove(collision.gameObject);
        }
    }
}
