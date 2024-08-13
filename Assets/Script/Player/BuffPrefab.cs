using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData;
/// <summary>
/// ���BUFF So
/// 2024.8.12 update C
/// </summary>
[CreateAssetMenu(fileName = "new Buff", menuName = "Player/Buff/new BuffPrefab")]
public class BuffPrefab : ScriptableObject
{
    [Tooltip("BUFF����")]
    public string BuffName;
    [Tooltip("BUFF��ͼ")]
    public Sprite BuffSprite;
    [Tooltip("BUFF��������")]
    public PlayerDataType ApplyType;
    [Tooltip("BUFF���ò���")]
    public float Parameter;
    [Tooltip("BUFF����ʱ��")]
    public float LastTime;
    [Tooltip("�Ƿ�ɻָ�")]
    public bool canRecovery;
    [Header("Runtime")]
    public float currentTime;
}
