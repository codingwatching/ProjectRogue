using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesList : MonoBehaviour
{
    public static EnemiesList instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }
    public List<GameObject> Scene1EnemyList;

    public List<GameObject> EnemyLevel1List;
    public List<GameObject> EnemyLevel2List;
    public List<GameObject> EnemyLevel3List;
    public List<GameObject> EnemyLevel4List;

    void Start(){
        
    }
    void Update(){
        
    }
}
