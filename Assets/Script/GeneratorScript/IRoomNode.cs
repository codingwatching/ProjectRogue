using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoomNode 
{
    public void onEnterRoom();//�����뷿��ʱ
    public void onExitRoom();//���˳�����ʱ
    public void onFinishRoom();//����ɷ���ʱ
}
