using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���侺���� - ����������˲���
/// 2024.8.14 update C
/// </summary>
public class Arena : MonoBehaviour
{
    public ArenaCFG arenaCFG;//�����������ļ�
    public RoomAdaptor Adaptor;//����������
    public bool enableEligeEnemy;//�Ƿ��о�Ӣ��

    List<List<GameObject>> TotalEnemyList = new List<List<GameObject>>();//�ܲ���list
    List<int> waveIndex = new List<int>();//ÿ���Ѷ�ϵ��

    List<GameObject> ArenaCurrentEnemyList = new List<GameObject>();//���ϵ���list

    //Runtime Data
    int totalIndex = 50;//���Ѷ�ϵ��
    int currentWaveIndex = 0;//��ǰ�����Ѷ�ϵ��

    GameObject EnemyFolder;//���˸��ڵ�

    void Start(){
        EnemyFolder = new GameObject() { name ="EnemyFolder" };
        EnemyFolder.transform.parent = transform;
        InitEnemy();
        GenPreEnemy();
        EnemyFolder.name = totalIndex + "/"+waveIndex[0];
    }
    void Update(){
        
    }
    //����Ԥ���ɵ���
    public void GenPreEnemy() { 
        foreach(var val in TotalEnemyList[currentWaveIndex]) {
            GameObject enemy = Instantiate(val);
            enemy.GetComponent<IEnemy>().setFreeze();
            enemy.transform.position = Adaptor.getRndPos();
            enemy.transform.parent = EnemyFolder.transform;
            ArenaCurrentEnemyList.Add(enemy);
        }
    }
    //�������
    public void ActiveEnemy() { 
        foreach(var val in ArenaCurrentEnemyList) {
            val.GetComponent<IEnemy>().setActive();
        }
    }
    //������һ������
    public void GenNextWaveEnemy() {
        currentWaveIndex += 1;
        foreach (var val in TotalEnemyList[currentWaveIndex]){
            GameObject enemy = Instantiate(val);
            enemy.GetComponent<IEnemy>().setFreeze();
            enemy.transform.position = Adaptor.getRndPos();
        }
    }
    //���ɵ�������
    public void GenSingleEnemy(GameObject enemy) { 
    }
    //��ʼ������
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
    //�����˴�list���벨���� ���Ѷ�ϵ���ܺͼ���
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
