using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomData;
/// <summary>
/// ����˳����
/// 2024.8.13 update C
/// </summary>
[CreateAssetMenu(fileName = "new RoomSequence", menuName = "Map/Room/new RoomSequence")]
public class RoomSequence : ScriptableObject
{
    [Tooltip("��������˳��")]
    public List<RoomType> so_roomSequence;
    [Tooltip("��֧�������ɽڵ�")]
    public List<int> so_subRoomGenIndex; 
}
