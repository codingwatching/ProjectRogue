using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 房间竞技场
/// 2024.8.14 update C
/// </summary>
public class Arena : MonoBehaviour
{
    public ArenaCFG arenaCFG;

    List<List<GameObject>> TotalEnemyList = new List<List<GameObject>>();
    List<int> waveIndex = new List<int>();

    List<GameObject> ArenaCurrentEnemyList;

    public int diffcultLevel;
    public bool enableEligeEnemy;
    //Runtime Data
    int totalIndex = 50;
    int currentWaveIndex = 0;

    void Start(){
        
    }
    void Update(){
        
    }
    public void GenPreEnemy() { 
        foreach(var val in TotalEnemyList[currentWaveIndex]) { 

        }
    }
    public void ActiveEnemy() { 
        foreach(var val in ArenaCurrentEnemyList) {
            val.GetComponent<IEnemy>().setActive();
        }
    }
    public void GenNextWaveEnemy() {
        currentWaveIndex += 1;
        foreach (var val in TotalEnemyList[currentWaveIndex]){

        }
    }
    public void GenSingleEnemy(GameObject enemy) { 
    }
    public void InitEnemy() {
        totalIndex = Random.Range(arenaCFG.DiffcultIndex.min, arenaCFG.DiffcultIndex.max);
        while (totalIndex > 0) {
            int rndIndex = Random.Range(arenaCFG.EachWaveMaxDiffcult.min, arenaCFG.EachWaveMaxDiffcult.max);
            totalIndex -= rndIndex;
            waveIndex.Add(rndIndex);
        }
        foreach(var val in waveIndex) {
            pickByEnemyList(val);
        }
    }
    public void pickByEnemyList(int waveIndex) {
        List<GameObject> innerEnemyList = new List<GameObject>();
        int count = 0;
        while(count < waveIndex) {
            var enemy = EnemiesList.instance.Scene1EnemyList[Random.Range(0, EnemiesList.instance.Scene1EnemyList.Count)];
            count += enemy.GetComponent<IEnemy>().getDiffcult();
            innerEnemyList.Add(enemy);
        }
        TotalEnemyList.Add(innerEnemyList);
    }
}
