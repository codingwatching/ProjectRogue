using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 自定义曲线子弹类 待完善
/// 2024.8.12 update C
/// </summary>
public class CustomBullet : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 5f; // 曲线运动的总时长
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

        float curveValue = curve.Evaluate(timer); // 获取曲线在归一化时间点的值
        //Debug.Log(curveValue);
        // 假设物体沿着x轴移动，y和z轴保持不变
        transform.Translate(0, Speed, 0, Space.Self);
        Vector3 newPosition = new Vector3(transform.position.x + curveValue, transform.position.y, 0);
        transform.position = newPosition;

        if (timer >= duration)
        {
            enabled = false;
        }
    }
}
