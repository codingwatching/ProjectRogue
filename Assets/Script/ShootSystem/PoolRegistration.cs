using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池实例化注册类
/// 2024.8.12 update C
/// </summary>
public class PoolRegistration : MonoBehaviour
{
    public GameObject BulletUR;

    public static GameObject BulletUR_Pool;

    void Start(){
        initFolder();
        initBullet();
    }
    void Update(){
        
    }
    public void initFolder() {
        BulletUR_Pool = new GameObject() { name = "BulletUR_Pool" };
    }
    public void initBullet() {
        BulletPool.GameObjectPoolManager.Instance.Register("BulletUR", BulletUR, 200, BulletUR_Pool);
    }
}
