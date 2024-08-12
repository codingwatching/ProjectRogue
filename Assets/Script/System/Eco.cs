using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏经济类
/// 2024.8.12 update C
/// </summary>
public class Eco : MonoBehaviour
{
    public static Eco instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }
    void Start(){
        
    }
    void Update(){
        
    }
    public void addCoin(int num) => Bridge.instance.Coin += num;
}
