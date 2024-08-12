using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Íæ¼Ò×´Ì¬»ú
/// 2024.8.12 update C
/// </summary>
public class PlayerStateMachine : StateMachine
{
    public Animator animator;
    [SerializeField]
    public PlayerState[] states;
    private void Awake(){
        StateList = new Dictionary<System.Type, IState>(states.Length);
        
    }
    public void initAllState() { 
        foreach(var state in states) {
            state.init(animator,this);
            StateList.Add(state.GetType(),state);
        }
    }
    void Start(){
        
    }
    void Update(){
        
    }
}
