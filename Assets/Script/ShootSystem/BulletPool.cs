using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;

/// <summary>
/// 子弹对象池
/// 2024.8.12 update C
/// </summary>
namespace BulletPool
{
    // 对象池管理器
    public class GameObjectPoolManager
    {
        public static GameObjectPoolManager Instance = new GameObjectPoolManager();

        public Dictionary<string, GameObjectPool> pools = new Dictionary<string, GameObjectPool>();

        // 对象池注册
        public void Register(string name, GameObject prefab,int num,GameObject fold){
            if (pools.TryGetValue(name, out GameObjectPool pool)){
                pool.prefab = prefab;
            }
            else{
                pool = new GameObjectPool();
                pool.prefab = prefab;
                pools.Add(name, pool);

                for (int i = 0; i < num; i++){
                    var s = GameObject.Instantiate(prefab);
                    pool.pool.Enqueue(s);
                    s.transform.parent = fold.transform;
                    s.transform.position = defaultPos;
                }
            }
        }

        public GameObject Get(string name){
            if (pools.TryGetValue(name, out GameObjectPool pool)){
                return pool.Get();
            }
            else{
                return null;
            }
        }

        public void Recycle(string name, GameObject go){
            if (pools.TryGetValue(name, out GameObjectPool pool)){
                pool.Recycle(go);
            }
            else{
                pool = new GameObjectPool();
                pool.prefab = go;
                pools.Add(name, pool);
            }
        }
    }

    /// <summary>
    /// 对象池
    /// </summary>
    public class GameObjectPool
    {
        public GameObject prefab;
        public Queue<GameObject> pool = new Queue<GameObject>();
        public GameObject Get(){
            if (pool.Count > 0){
                return pool.Dequeue();
            }
            else{
                return GameObject.Instantiate(prefab);
            }
        }
        public void Recycle(GameObject go) { 
            go.transform.position = defaultPos;
            pool.Enqueue(go);
        }
    }
}
