using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    public static RoomLoader instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }
    [Tooltip("起始房间")]
    public List<RoomPrefab> StartRoom;
    [Tooltip("普通房间")]
    public List<RoomPrefab> NormalRoom;
    [Tooltip("奖励房间")]
    public List<RoomPrefab> BonusRoom;
    [Tooltip("商店房间")]
    public List<RoomPrefab> ShopRoom;
    [Tooltip("BOSS房间")]
    public List<RoomPrefab> BossRoom;
    [Tooltip("结束房间")]
    public List<RoomPrefab> EndRoom;

    void Start(){
        
    }
    void Update(){
        
    }
}
