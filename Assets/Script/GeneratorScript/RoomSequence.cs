using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomData;
/// <summary>
/// 房间顺序类
/// 2024.8.13 update C
/// </summary>
[CreateAssetMenu(fileName = "new RoomSequence", menuName = "Map/Room/new RoomSequence")]
public class RoomSequence : ScriptableObject
{
    [Tooltip("房间生成顺序")]
    public List<RoomType> so_roomSequence;
    [Tooltip("分支房间生成节点")]
    public List<int> so_subRoomGenIndex; 
}
