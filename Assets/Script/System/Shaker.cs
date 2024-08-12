using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/// <summary>
/// ͨ����Ļ�����ű�
/// 2024.8.12 update C
/// </summary>
public class Shaker : MonoBehaviour
{
    public static Shaker instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }
    public CinemachineImpulseSource source;
    void Start() {
        
    }
    void Update() {
        
    }
    //��������� force=ǿ��
    public void genShake(float force) {
        source.GenerateImpulse(new Vector3(Random.Range(0,2) == 1 ? -1 : 1, Random.Range(0, 2) == 1 ? -1 : 1, 0 ) * force);
    }
    //�Զ��巽����
    public void genShake(Vector3 dir) {
        source.GenerateImpulse(dir);
    }
}
