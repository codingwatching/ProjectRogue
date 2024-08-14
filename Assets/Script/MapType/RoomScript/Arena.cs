using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 房间竞技场 - 随机产生敌人波数
/// 2024.8.14 update C
/// </summary>
public class Arena : MonoBehaviour
{
    public ArenaCFG arenaCFG;//竞技场配置文件
    public RoomAdaptor Adaptor;//房间适配器
    public bool enableEligeEnemy;//是否有精英怪

    List<List<GameObject>> TotalEnemyList = new List<List<GameObject>>();//总波数list
    List<int> waveIndex = new List<int>();//每波难度系数

    List<GameObject> ArenaCurrentEnemyList = new List<GameObject>();//场上敌人list

    //Runtime Data
    int totalIndex = 50;//总难度系数
    int currentWaveIndex = 0;//当前波数难度系数

    GameObject EnemyFolder;//敌人父节点

    void Start(){
        EnemyFolder = new GameObject() { name ="EnemyFolder" };
        EnemyFolder.transform.parent = transform;
        InitEnemy();
        GenPreEnemy();
        EnemyFolder.name = totalIndex + "/"+waveIndex[0];
    }
    void Update(){
        
    }
    //生成预生成敌人
    public void GenPreEnemy() { 
        foreach(var val in TotalEnemyList[currentWaveIndex]) {
            GameObject enemy = Instantiate(val);
            enemy.GetComponent<IEnemy>().setFreeze();
            enemy.transform.position = Adaptor.getRndPos();
            enemy.transform.parent = EnemyFolder.transform;
            ArenaCurrentEnemyList.Add(enemy);
        }
    }
    //激活敌人
    public void ActiveEnemy() { 
        foreach(var val in ArenaCurrentEnemyList) {
            val.GetComponent<IEnemy>().setActive();
        }
    }
    //生成下一波敌人
    public void GenNextWaveEnemy() {
        currentWaveIndex += 1;
        foreach (var val in TotalEnemyList[currentWaveIndex]){
            GameObject enemy = Instantiate(val);
            enemy.GetComponent<IEnemy>().setFreeze();
            enemy.transform.position = Adaptor.getRndPos();
        }
    }
    //生成单个敌人
    public void GenSingleEnemy(GameObject enemy) { 
    }
    //初始化敌人
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
    //将敌人从list加入波数中 按难度系数总和计算
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
