using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ����װ����
/// 2024.8.14 update C
/// </summary>
public class EnemyLoader : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyData enemyData;
    void Start(){
    }
    void Update(){

    }
    public void LoadEnemyData() { 

    }
    public void EnemySetUp() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.autoRepath = true;
        agent.Warp(transform.position);
    }
}
