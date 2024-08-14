using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomData;

[CreateAssetMenu(fileName = "new RoomPrefab", menuName = "Map/Room/new RoomPrefab")]
public class RoomPrefab : ScriptableObject
{
    [Tooltip("��������")]
    public RoomType roomType;
    [Tooltip("����prefab")]
    public GameObject roomPrefab;
    [Tooltip("�����С �������¿�ʼ���㣩")]
    public Vector2Int Rect;
    [Tooltip("���ͷ���ѡȡ����")]
    public float PickRate;
}
