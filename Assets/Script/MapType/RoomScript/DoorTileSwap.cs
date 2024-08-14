using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// ·¿¼ätileÇÐ»»Æ÷
/// 2024.8.14 update C
/// </summary>
[CreateAssetMenu(fileName = "new DoorTileSwag", menuName = "Map/DoorTileSwag/new TileSwag")]
public class DoorTileSwap : ScriptableObject
{
    public TileBase Door_IdleTile;
    public AnimatedTile Door_ActiveAnimTile;
    public AnimatedTile Door_DisactiveAnimTile;
}
