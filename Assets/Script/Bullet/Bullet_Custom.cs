using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CustomBullet", menuName = "Bullet/new CustomBullet")]
public class Bullet_Custom : Bullet
{
    [Header("Curve Bullet")]
    public AnimationCurve Bullet_Curve;
}
