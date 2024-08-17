using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;
using Random = UnityEngine.Random;
/// <summary>
/// 掉落物移动类 - 控制掉落物移动
/// 2024.8.17 update C
/// </summary>
public class DropMove : MonoBehaviour
{
    public AnimationCurve curve;

    public bool enableBounce = true;
    public bool enableRndPos = true;
    public bool enableMove = true;
    public bool enableTracePlayer = false;

    public float BounceRate = 1f;

    float timer = 0;

    void Start(){
        //genDynamicDrop();
    }
    void Update(){
        //if (enableBounce) bounceFunction();
        //if (enableTracePlayer) tracePlayerFunc();
    }
    public void genIdleDrop() {
        enableBounce = false;
        enableRndPos = false;
        enableMove = false;
    }
    public void genDynamicDrop(AnimationCurve curve) {
        this.curve = curve;
        BounceRate = Random.Range(0.5f, 1f);
        rndPos();
        //moveFunction();
        enableBounce = true;
        waitForTrace();
    }
    public void rndPos() {
        Vector3 rndPos = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
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
    public async void waitForTrace() {
        await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);
        enableTracePlayer = true;
    }
    public void tracePlayerFunc() {
        var player = PlayerSuperCtrl.instance.gameObject;

        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;

        Vector2 v2 = new Vector2(transform.position.x + x / 6 * Time.fixedDeltaTime, transform.position.y + y / 6 * Time.fixedDeltaTime);

        transform.position = v2;
    }
    public void resetParameter() {
        enableBounce = false;
        enableTracePlayer = false;
    }
}
