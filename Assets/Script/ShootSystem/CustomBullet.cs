using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �Զ��������ӵ��� ������
/// 2024.8.12 update C
/// </summary>
public class CustomBullet : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 5f; // �����˶�����ʱ��
    public float Speed = 1f;

    float timer = 0;

    Vector3 mousescreenPosition;
    Vector3 screenPositionInworld;

    Vector3 direction;

    void Start()
    {
        DirAngle();
    }

    void Update(){
        
    }
    private void FixedUpdate(){
        function();
    }
    public void DirAngle(){

        //Debug.Log(PlayerSuperCtrl.instance.transform.position);

        mousescreenPosition = Input.mousePosition;
        mousescreenPosition.z = 0;
        screenPositionInworld = Camera.main.ScreenToWorldPoint(mousescreenPosition);
        screenPositionInworld.z = 0;

        //GameObject players = Player;
        Vector3 face = transform.up;
        direction = screenPositionInworld - PlayerSuperCtrl.instance.transform.position;
        float angle = Vector3.SignedAngle(face, direction, Vector3.forward);

        //angle += Random.Range(-loadWeapon.SpreadRange, loadWeapon.SpreadRange);

        transform.Rotate(0, 0, angle);
    }
    public void function() { 
        timer += Time.deltaTime;

        float curveValue = curve.Evaluate(timer); // ��ȡ�����ڹ�һ��ʱ����ֵ
        //Debug.Log(curveValue);
        // ������������x���ƶ���y��z�ᱣ�ֲ���
        transform.Translate(0, Speed, 0, Space.Self);
        Vector3 newPosition = new Vector3(transform.position.x + curveValue, transform.position.y, 0);
        transform.position = newPosition;

        if (timer >= duration)
        {
            enabled = false;
        }
    }
}
