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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.transform.position);
        //agent.destination = player.transform.position;
    }
}
