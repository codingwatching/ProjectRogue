using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 敌人基类 - 读取敌人数据/敌人受击击退/闪烁
/// 2024.8.15 update C
/// </summary>
public class EnemyBase : MonoBehaviour
{
    public EnemyData enemyData;
    public Rigidbody2D rgd2d;
    public SpriteRenderer spriteRender;
    public Gradient gradient;
    [Header("Runtime")]
    public float Blood;
    public float Damage;
    void Start(){
        
    }
    void Update(){
        //if()
    }
    public void loadEnemyData(EnemyData data) {
        enemyData = data;
        Blood = enemyData.Enemy_Health;
        
    }
    public void onHitRedFlick() {
        spriteRender.DOGradientColor(gradient, 0.4f);
    }
}
