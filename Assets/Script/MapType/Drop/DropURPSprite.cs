using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// ������ͨ����ͼ�ϼ� So
/// ���ͣ�Ѫ��Ӳ�� /����
/// 2024.8.13 update C
/// </summary>
[CreateAssetMenu(fileName = "new DropURPSprite", menuName = "Drop/DropURPSprite")]
public class DropURPSprite : ScriptableObject
{
    [System.Serializable]
    public struct SpriteCollect {
        public DropItemType type;
        public Sprite sprite;
    }
    public List<SpriteCollect> sprites;
}
