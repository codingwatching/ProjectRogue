using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// µÙ¬‰ŒÔ≈‰÷√±Ì
/// 2024.8.15 update C
/// </summary>
[CreateAssetMenu(fileName = "new DropCFG", menuName = "Drop/DropCFG")]
public class Drop : ScriptableObject
{
    public DropItemType itemType;
    public Weapon dropWeapon;
    public int itemData;
}
