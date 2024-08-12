using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyData", menuName = "Enemy/new EnemyData")]
public class EnemyData : ScriptableObject
{
    public string Enemy_Name;
    public float Enemy_Health;
    public int LevelNum;
    public int Enemy_ID;
    [Space]
    public GameObject[] Enemy_Drop;
}
