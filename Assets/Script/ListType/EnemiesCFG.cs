using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tools;

[CreateAssetMenu(fileName = "new EnemyCFG", menuName = "Enemy/new EnemyCFG")]
public class EnemiesCFG : ScriptableObject
{
    public Range DiffcultIndex;

    public Range Level1Range;
    public Range Level2Range;
    public Range Level3Range;
    public Range Level4Range;
}
