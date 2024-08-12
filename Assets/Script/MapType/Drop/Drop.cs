using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 掉落物基类 - 控制掉落物移动
/// 2024.8.12 update C
/// </summary>
public class Drop : MonoBehaviour
{
    public AnimationCurve curve;

    public bool enableBounce = true;
    public bool enableRndPos = true;
    public bool enableMove = true;

    public float BounceRate = 1f;

    float timer = 0;

    void Start(){
        genDynamicDrop();
    }
    void Update(){
        if(enableBounce)bounceFunction();
    }
    public void genIdleDrop() {
        enableBounce = false;
        enableRndPos = false;
        enableMove = false;
    }
    public void genDynamicDrop() {
        BounceRate = Random.Range(0.5f, 1f);
        rndPos();
        moveFunction();
        enableBounce = true;
    }
    public void rndPos() {
        Vector3 rndPos = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        transform.position += rndPos;
    }
    public void moveFunction() {
        var time = Random.Range(1f, 1.5f);
        Vector3 dir = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        transform.DOMove(transform.position + dir,time).SetEase(Ease.OutCirc);
    }
    public void bounceFunction() {
        timer += Time.deltaTime;

        float curveValue = curve.Evaluate(timer);
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + BounceRate *  curveValue, 0);
        transform.position = newPosition;

        if (timer >= 1f){
            enableBounce = false;
        }
    }
}
