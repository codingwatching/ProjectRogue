using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ӵ�ͨ��ʵ��ڵ� - �ӵ����� ʵ�����
/// 2024.8.12 update C
/// </summary>
public class Bullet_EntityNode : MonoBehaviour
{
    [HideInInspector]
    public Bullet_SubEntity subEntity;

    private GameObject[] subPrefab;
    private int subEntityNum=1;
    private float subEmitRange=180f;
    private int subEmitNum=1;

    void Start(){
        
    }
    void Update(){
        
    }
    public void LoadSubEntity() {
        subEmitNum = subEntity.Sub_EmitNum;
        subEmitRange = subEntity.Sub_SpreadRange;
        subEntityNum = subEntity.Sub_EntityNum;
        subPrefab = subEntity.Sub_Entity;
    }
    public void CheckSubEntityNum() {
        if (subEntityNum == 1) {

        }
        else { 

        }
    }
    public void EmitSubEntity() { 

    }
}
