using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ScriptableObject
{
    [Header("Public")]
    [Tooltip("子弹名称")] 
    public string Bullet_Name;
    [Tooltip("子弹碰撞箱范围")] 
    public float Bullet_HitBoxRange;
    [Space]
    [Tooltip("子弹贴图集")] 
    public Sprite[] Bullet_Sprites;
    [Tooltip("子弹贴图切换频率")]
    public float Bullet_SpriteFrequency;
    [Space]
    [Tooltip("是否生成子实体")] 
    public bool enableEntityGen;
    [Tooltip("子实体生成列表")] 
    public Bullet_SubEntity Bullet_SubEntity;
    [Space]
    [Tooltip("是否反弹")] 
    public bool enableBounce;
    [Tooltip("是否穿透")] 
    public bool enablePierce;
}
