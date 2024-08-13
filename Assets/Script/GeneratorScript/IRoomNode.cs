using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoomNode 
{
    public void onEnterRoom();//当进入房间时
    public void onExitRoom();//当退出房间时
    public void onFinishRoom();//当完成房间时
}
