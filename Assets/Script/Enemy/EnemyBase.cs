using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 敌人基类 - 读取敌人数据/敌人受击击退/闪烁
/// 2024.8.15 update C
/// </summary>
public class EnemyBase : MonoBehaviour , IEnemy
{
    public EnemyData enemyData;
    public SpriteRenderer spriteRender;
    public EnemyAction action;
    public EnemyStateMachine StateMachine;
    public Gradient gradient;
    [Header("Runtime")]
    public float Blood;
    public float Damage;
    void Start(){
        loadEnemyData(enemyData);
    }
    void Update(){

    }
    public void loadEnemyData(EnemyData data) {
        enemyData = data;
        Blood = enemyData.Enemy_Health;    
    }
    public void onHitRedFlick() {
        spriteRender.DOGradientColor(gradient, 0.4f);
    }
    public int getDiffcult(){
        return enemyData.LevelNum;
    }
    public void hurt(float damage){
        Blood -= damage;
        onHitRedFlick();
        if (Blood <= 0) {
            onEnemyDead();
        }
    }

    public void onEnemyCreate(){

    }
    public void onEnemyDead(){
        foreach(var val in enemyData.Enemy_Drop) {
            DropGenerator.instance.Drop(transform.position,val);
        }
        Destroy(gameObject);
    }
    public void setActive(){
        action.enabled = true;
        StateMachine.enabled = true;
    }
    public void setFreeze(){
        action.enabled = false;
        StateMachine.enabled = false;
    }
}
