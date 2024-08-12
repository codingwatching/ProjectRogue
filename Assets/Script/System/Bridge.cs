using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 数据总桥 = 跨场景使用 待写
/// 2024.8.12 update C
/// </summary>
public class Bridge : MonoBehaviour
{
    public static Bridge instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    public int Coin = 0;

    void Start(){
        
    }
    void Update(){
        
    }
}
