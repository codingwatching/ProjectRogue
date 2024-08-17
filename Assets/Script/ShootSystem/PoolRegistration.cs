using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池实例化注册类
/// 2024.8.17 update C
/// </summary>
public class PoolRegistration : MonoBehaviour
{
    public GameObject BulletUR;
    public GameObject DropUR;

    public static GameObject BulletUR_Pool;
    public static GameObject DropUR_Pool;

    void Start(){
        initFolder();
        initBullet();
    }
    void Update(){
        
    }
    public void initFolder() {
        BulletUR_Pool = new GameObject() { name = "BulletUR_Pool" };
        DropUR_Pool = new GameObject() { name = "DropUR_Pool" };
    }
    public void initBullet() {
        BulletPool.GameObjectPoolManager.Instance.Register("BulletUR", BulletUR, 200, BulletUR_Pool);
        BulletPool.GameObjectPoolManager.Instance.Register("DropUR", DropUR, 200, DropUR_Pool);
    }
}
