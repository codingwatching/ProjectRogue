using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData;
/// <summary>
/// 玩家BUFF So
/// 2024.8.12 update C
/// </summary>
[CreateAssetMenu(fileName = "new Buff", menuName = "Player/Buff/new BuffPrefab")]
public class BuffPrefab : ScriptableObject
{
    [Tooltip("BUFF名字")]
    public string BuffName;
    [Tooltip("BUFF贴图")]
    public Sprite BuffSprite;
    [Tooltip("BUFF作用类型")]
    public PlayerDataType ApplyType;
    [Tooltip("BUFF作用参数")]
    public float Parameter;
    [Tooltip("BUFF持续时间")]
    public float LastTime;
    [Tooltip("是否可恢复")]
    public bool canRecovery;
    [Header("Runtime")]
    public float currentTime;
}
