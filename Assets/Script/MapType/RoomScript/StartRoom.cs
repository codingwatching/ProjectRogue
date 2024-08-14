using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 初始房间 - 调整玩家位置
/// 2024.8.14 update C
/// </summary>
public class StartRoom : MonoBehaviour
{
    void Start(){
        resetPlayerPosition();
    }
    void Update(){
        
    }
    public void resetPlayerPosition() => PlayerSuperCtrl.instance.transform.position = transform.position;
}
