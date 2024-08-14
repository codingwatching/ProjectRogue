using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomData;

[CreateAssetMenu(fileName = "new RoomPrefab", menuName = "Map/Room/new RoomPrefab")]
public class RoomPrefab : ScriptableObject
{
    [Tooltip("房间类型")]
    public RoomType roomType;
    [Tooltip("房间prefab")]
    public GameObject roomPrefab;
    [Tooltip("房间大小 （从左下开始计算）")]
    public Vector2Int Rect;
    [Tooltip("类型房间选取概率")]
    public float PickRate;
}
