using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ͨ��״̬��ӿ�
/// 2024.8.12 update C
/// </summary>
public interface IState
{
    //������״̬ʱ
    public void onEnter();
    //���˳�״̬ʱ
    public void onExit();
    //״̬�߼�����
    public void onLogicUpdate();
    //״̬�������
    public void onPhysicsUpdate();
}
