using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Player_Idle", menuName = "Player/PlayerState/new Player_Idle")]
public class Player_Idle : PlayerState
{
    public override void onEnter(){
        //animator.Play("Idle");
    }
    public override void onLogicUpdate(){
        
    }
}
