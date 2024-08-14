using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour , IEnemy
{
    public GameObject player;
    NavMeshAgent agent;

    public int getDiffcult()
    {
        return 3;
    }

    public void hurt()
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
        /*player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
        agent.autoRepath = true;
        agent.Warp(transform.position);*/
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.transform.position);
        //agent.destination = player.transform.position;
    }
}
