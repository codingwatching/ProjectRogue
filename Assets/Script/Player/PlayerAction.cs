using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tools;
using static PlayerData;
/// <summary>
/// 玩家行动
/// 2024.8.12 update C
/// </summary>
public class PlayerAction : MonoBehaviour
{
    public Rigidbody2D rgd;
    //public GameObject playerBody;

    public float moveSpeed = 7f; // 玩家移动速度
    public float dashSpeed = 7.5f;
    public float moveForce = 400f;
    //
    float dashTimeRecord = 0;
    float dashTimeLast = 0.5f;
    //
    float dashLimitRecord = 0;
    float dashLimitTime = 0.7f;

    bool isDash = false;
    bool isDashCoolDown = false;
    Vector2 dashDirection;

    float horizontalInput;
    float verticalInput;

    void Start(){
        
    }
    void Update(){
        //Debug.Log(isDashCoolDown);
        if (Input.GetMouseButtonDown(1) && !isDashCoolDown) {
            isDash = true;
            isDashCoolDown = true;
            dashDirection = (getMousePointV2() - v3to2(transform.position)).normalized;
        }
    }
    private void FixedUpdate(){
        movement();
        dashFunction();
    }
    void movement() { 
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (moveDirection != Vector2.zero && !banMove){
            Vector2 moveVector = moveDirection * moveSpeed * Time.deltaTime;
            rgd.velocity = moveVector;
            //rgd.MovePosition(new Vector2(transform.position.x,transform.position.y) + moveVector);
            //rgd.AddForce(moveDirection * moveForce , ForceMode2D.Impulse);
        }
        else {
            rgd.velocity = new Vector2(0, 0);
        }
    }
    //Dash Function
    void dashFunction() { 
        if (isDash) {
            banMove = true;
            PlayerSuperCtrl.instance.hitBox.enabled = false;
            dashMove();
            dashTimeRecord += Time.deltaTime;
            if(dashTimeRecord >= dashTimeLast) {
                isDash = false;
                dashTimeRecord = 0;
                banMove = false;
                PlayerSuperCtrl.instance.hitBox.enabled = true;
            }
        }
        if (isDashCoolDown) {
            dashCoolDownFunc();
        }
    }
    void dashMove() {
        Vector2 moveVector = dashDirection * dashSpeed * Time.deltaTime;
        rgd.MovePosition(new Vector2(transform.position.x,transform.position.y) + moveVector);
    }
    void dashCoolDownFunc() {
        dashLimitRecord += Time.deltaTime;
        if(dashLimitRecord >= dashLimitTime) {
            isDashCoolDown = false;
            dashLimitRecord = 0;
        }
    }
    //----
}
