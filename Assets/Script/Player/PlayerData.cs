using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Íæ¼ÒÊý¾Ý
/// 2024.8.12 update C
/// </summary>
public class PlayerData
{
    public static float Speed;
    public static float Blood;
    public static float DamageMultiplier;
    public static float EmissionRate;

    public static bool banMove = false;
    public static bool banDash = false;
    public static bool banShoot = false;
    public static bool banTurn = false;
    public static bool banRotate = false;
    public static bool banBuff = false;

    public static int forward = 1;

    //PlayerState
    public static bool isDash = false;
    public static bool isInvincible = false;
    public static bool isDead = false;

    public static float invincibleTime = 1f;

    public enum PlayerDataType { Speed , Blood , Damage , EmissionRate }
}
