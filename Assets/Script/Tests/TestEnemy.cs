using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class TestEnemy : MonoBehaviour , IEnemy
{
    public GameObject player;
    NavMeshAgent agent;
    public SpriteRenderer renderer2d;
    public int getDiffcult()
    {
        return 3;
    }

    public void hurt(float damage)
    {
    }

    public void onEnemyCreate()
    {
    }

    public void onEnemyDead()
    {
    }

    public void setActive()
    {
    }

    public void setFreeze()
    {
    }
    Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        // 设置颜色关键帧
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0] = new GradientColorKey(Color.red, 0f); // 起始颜色
        colorKeys[1] = new GradientColorKey(Color.white, 1f); // 结束颜色

        // 配置渐变
        gradient.colorKeys = colorKeys;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.transform.position);
        //agent.destination = player.transform.position;
    }
}
