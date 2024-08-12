using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Íæ¼Ò×´Ì¬Àà
/// 2024.8.12 update C
/// </summary>
public class PlayerState : ScriptableObject , IState
{
    public Animator animator;
    public PlayerStateMachine statemachine;
    public void init(Animator animator, PlayerStateMachine statemachine) {
        this.animator = animator;
        this.statemachine = statemachine;
    }
    public virtual void onEnter(){
    }
    public virtual void onExit(){
    }
    public virtual void onLogicUpdate(){
    }
    public virtual void onPhysicsUpdate(){
    }
}
