using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 敌人移动通用脚本
/// 2024.8.15 update C
/// </summary>
public class EnemyAction : MonoBehaviour
{
    public NavMeshAgent agent;
    GameObject Player;

    public bool enableTracePlayer = false;

    void Start(){
        EnemySetUp();
        Player = PlayerSuperCtrl.instance.gameObject;
    }
    void Update(){
        
    }
    public void EnemySetUp() {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.Warp(transform.position);
    }
    public void TracePlayerFunc() {
        agent.SetDestination(Player.transform.position);
    }
    public void MoveRndPostion(float range) {
        Vector2 newPos = new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        agent.SetDestination(newPos);
    }
}
