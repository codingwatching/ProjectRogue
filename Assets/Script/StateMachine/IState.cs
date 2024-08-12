using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 通用状态类接口
/// 2024.8.12 update C
/// </summary>
public interface IState
{
    //当进入状态时
    public void onEnter();
    //当退出状态时
    public void onExit();
    //状态逻辑更新
    public void onLogicUpdate();
    //状态物理更新
    public void onPhysicsUpdate();
}
