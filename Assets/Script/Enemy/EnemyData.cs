using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// µÐÈËÊý¾Ý SO
/// 2024.8.15 update C
/// </summary>
[CreateAssetMenu(fileName = "new EnemyData", menuName = "Enemy/new EnemyData")]
public class EnemyData : ScriptableObject
{
    public string Enemy_Name;
    public float Enemy_Health;
    public int LevelNum;
    public int Enemy_ID;
    [Space]
    public Drop[] Enemy_Drop;
}
