using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using static Tools;
using static PlayerData;
/// <summary>
/// 玩家行动
/// 2024.8.17 update C
/// </summary>
public class PlayerAction : MonoBehaviour
{
    public Rigidbody2D rgd;
    //public GameObject playerBody;

    public float moveSpeed = 7f; // 玩家移动速度
    public float dashSpeed = 7.5f;
    public float moveForce = 400f;

    float dashTimeLast = 0.5f;//冲刺持续时间
    float dashLimitTime = 0.7f;//冲刺cd时间

    Vector2 dashDirection;

    float horizontalInput;
    float verticalInput;

    void Start(){
        
    }
    void Update(){
        if (Input.GetMouseButtonDown(1)) {
            genDashFunc();
        }
    }
    private void FixedUpdate(){
        movement();
        dashFunction();
    }
    void movement() { 
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (moveDirection != Vector2.zero && !banMove){
            Vector2 moveVector = moveDirection * moveSpeed * Time.fixedDeltaTime;
            rgd.velocity = moveVector;
            //rgd.MovePosition(new Vector2(transform.position.x,transform.position.y) + moveVector);
            //rgd.AddForce(moveDirection * moveForce);
        }
        else {
            rgd.velocity = new Vector2(0, 0);
        }
    }
    //Dash Function
    void dashFunction() { 
        if (isDash) {
            dashMove();
        }
    }
    void dashMove() {
        Vector2 moveVector = dashDirection * dashSpeed * Time.deltaTime;
        rgd.MovePosition(new Vector2(transform.position.x,transform.position.y) + moveVector);
    }
    public void genDashFunc() {
        if (!banDash){
            dashMoveStart();
            dashMoveEnd();
            dashCDFunc();
        }
    }
    public void dashMoveStart() {
        isDash = true;
        banDash = true;
        PlayerSuperCtrl.instance.hitBox.enabled = false;
        isInvincible = true;
        dashDirection = (getMousePointV2() - v3to2(transform.position)).normalized;
    }
    public async void dashMoveEnd() { 
        await UniTask.Delay(TimeSpan.FromSeconds(dashTimeLast), ignoreTimeScale: false);
        isDash = false;
        PlayerSuperCtrl.instance.hitBox.enabled = true;
        isInvincible = false;
    }
    public async void dashCDFunc() {
        await UniTask.Delay(TimeSpan.FromSeconds(dashLimitTime), ignoreTimeScale: false);
        banDash = false;
    }
}
