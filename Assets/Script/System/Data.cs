using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ϸ�ܲ�����
/// 2024.8.12 update C
/// </summary>
public static class Data
{
    public enum WeaponRank { Normal , Elige , Legend , Contraband } //����Ʒ��
    public enum DropItemType { Weapon , Blood , Coin }

    public static Vector2 defaultPos = new Vector2(0,0); //�����Ĭ��λ��

    //������ӵ�����
    public static string BulletUR = "BulletUR";
    public static string BulletCurve = "BulletCurve";
    public static string BulletCustom = "BulletCustom";

    //ͼ������
    public static int DefaultLayer = 0;
    public static int IgnoreRayCast = 2;
    public static int MapType = 3;
    public static int PlayerLayer = 6;
    public static int EnemyLayer = 7;
    public static int DropLayer = 8;
    public static int Attacker = 9;
}
