using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tools;
/// <summary>
/// ���侺���� �������ñ�
/// 2024.8.14 update C
/// </summary>
[CreateAssetMenu(fileName = "new ArenaCFG", menuName = "Map/CFG/new ArenaCFG")]
public class ArenaCFG : ScriptableObject
{
    public Range DiffcultIndex;//������������Ѷ�ϵ��
    public Range EachWaveMaxDiffcult;//������������Ѷ�ϵ��
}
