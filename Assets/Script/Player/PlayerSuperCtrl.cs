using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static PlayerData;
/// <summary>
/// 玩家总控 == 控制玩家转向，武器旋转
/// 2024.8.12 update C
/// </summary>
public class PlayerSuperCtrl : MonoBehaviour
{
    public static PlayerSuperCtrl instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }

    public GameObject HandHeld;

    public BoxCollider2D hitBox;
    public SpriteRenderer playerSprite;

    Vector3 mousescreenPosition;
    Vector3 screenPositionInworld;
    Vector3 direction;
    Vector2 midPoint;

    void Start(){
        
    }
    void Update(){
        midPoint = Camera.main.WorldToScreenPoint(transform.position);
        mousePointCheck();
        if(!banRotate)weaponRotate();
    }
    public void onHit(int damage) {
        if (!isInvincible){
            Blood -= damage;
            isInvincible = true;
            if (Blood <= 0){

            }
        }
    }
    public void onHitRedFlick() { 

    }
    public void mousePointCheck() {
        mousescreenPosition = Input.mousePosition;
        mousescreenPosition.z = 0;
        screenPositionInworld = Camera.main.ScreenToWorldPoint(mousescreenPosition);
        screenPositionInworld.z = 0;

        if (mousescreenPosition.x > midPoint.x && !banTurn){
            turnBody(1);
        }
        else if (mousescreenPosition.x <= midPoint.x && !banTurn)
            turnBody(-1);

    }
    public void weaponRotate() {
        Vector3 face = transform.up;

        direction = screenPositionInworld - transform.position;
        float angle = Vector3.SignedAngle(face, direction, Vector3.forward);

        HandHeld.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + forward * 90));
    }
    public void turnBody(int dir) {
        transform.localScale = new Vector3(dir, 1, 1);
        forward = dir;
    }
}
