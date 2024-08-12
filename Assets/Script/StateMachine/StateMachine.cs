using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Í¨ÓÃ×´Ì¬»ú
/// 2024.8.12 update C
/// </summary>
public class StateMachine : MonoBehaviour
{
    public IState currentState;
    public Dictionary<System.Type, IState> StateList;
    void Start(){
        
    }
    void Update(){
        currentState.onLogicUpdate();
    }
    void FixedUpdate(){
        currentState.onPhysicsUpdate();
    }
    public void switchOnState(IState state) {
        currentState = state;
        currentState.onEnter();
    }
    public void switchState(IState newState) {
        currentState.onExit();
        switchOnState(newState);
    }
    public void switchState(System.Type newState) {
        switchState(StateList[newState]);
    }
}
