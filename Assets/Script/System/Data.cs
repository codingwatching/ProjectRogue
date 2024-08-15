using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏总参数类
/// 2024.8.12 update C
/// </summary>
public static class Data
{
    public enum WeaponRank { Normal , Elige , Legend , Contraband } //武器品级
    public enum DropItemType { Weapon , Blood , Coin }

    public static Vector2 defaultPos = new Vector2(0,0); //对象池默认位置

    //对象池子弹名字
    public static string BulletUR = "BulletUR";
    public static string BulletCurve = "BulletCurve";
    public static string BulletCustom = "BulletCustom";

    //图层名字
    public static int DefaultLayer = 0;
    public static int IgnoreRayCast = 2;
    public static int MapType = 3;
    public static int PlayerLayer = 6;
    public static int EnemyLayer = 7;
    public static int DropLayer = 8;
    public static int Attacker = 9;
}
