using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tools;
/// <summary>
/// 房间竞技场 敌人配置表
/// 2024.8.14 update C
/// </summary>
[CreateAssetMenu(fileName = "new ArenaCFG", menuName = "Map/CFG/new ArenaCFG")]
public class ArenaCFG : ScriptableObject
{
    public Range DiffcultIndex;//单个房间最大难度系数
    public Range EachWaveMaxDiffcult;//单个波次最大难度系数
}
