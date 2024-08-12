using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/// <summary>
/// 通用屏幕抖动脚本
/// 2024.8.12 update C
/// </summary>
public class Shaker : MonoBehaviour
{
    public static Shaker instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }
    public CinemachineImpulseSource source;
    void Start() {
        
    }
    void Update() {
        
    }
    //随机方向震动 force=强度
    public void genShake(float force) {
        source.GenerateImpulse(new Vector3(Random.Range(0,2) == 1 ? -1 : 1, Random.Range(0, 2) == 1 ? -1 : 1, 0 ) * force);
    }
    //自定义方向震动
    public void genShake(Vector3 dir) {
        source.GenerateImpulse(dir);
    }
}
