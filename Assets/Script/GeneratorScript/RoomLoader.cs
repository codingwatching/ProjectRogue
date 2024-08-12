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
    [Tooltip("��ʼ����")]
    public List<RoomPrefab> StartRoom;
    [Tooltip("��ͨ����")]
    public List<RoomPrefab> NormalRoom;
    [Tooltip("��������")]
    public List<RoomPrefab> BonusRoom;
    [Tooltip("�̵귿��")]
    public List<RoomPrefab> ShopRoom;
    [Tooltip("BOSS����")]
    public List<RoomPrefab> BossRoom;
    [Tooltip("��������")]
    public List<RoomPrefab> EndRoom;

    void Start(){
        
    }
    void Update(){
        
    }
}
