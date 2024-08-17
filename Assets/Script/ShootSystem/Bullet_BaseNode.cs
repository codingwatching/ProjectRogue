using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
/// <summary>
/// 通用子弹基类 - 参数加载 组件加载 子弹池引用回收
/// 2024.8.15 update C
/// </summary>
public class Bullet_BaseNode : MonoBehaviour
{
    [Header("Parameter")]
    public bool isUsed = false;
    public float reduceTime = 5f;

    public Bullet_EntityNode EntityNode;
    public SpriteRenderer SpriteRender;

    public bool enableBounce = false;
    public bool enablePierce = false;
    public bool enableEntity = false;

    public bool enableRepel = false;
    public float repelForce = 0;

    public float BulletSpeed = 0.1f;
    public float BulletDamage = 0;
    public float BulletSpreadRange = 0;
    public Sprite[] BulletSprites;
    public float BulletSpriteLoopFrequency=0.5f;

    Vector3 mousescreenPosition;
    Vector3 screenPositionInworld;

    Vector3 direction;

    float timer = 0;

    int SpriteCount=1;
    int SpriteCurrentIndex = 0;
    float SpriteLoopTimer = 0;
    bool isLoopSprite = false;

    void Start(){

    }
    void Update(){
        if (isLoopSprite) LoopBulletSprite();
    }
    void FixedUpdate(){
        if (isUsed){
            Function();
            outTimeReduceFunction();
        }
    }
    public void ShootFunc() {
        InitBulletSprite();
        DirAngle();
        isUsed = true;
    }
    public void InitBulletSprite() {
        SpriteCount = BulletSprites.Length;
        if (SpriteCount == 1) {
            SpriteRender.sprite = BulletSprites[0];
        }
        else if(SpriteCount > 1){
            isLoopSprite = true;
        }
        else if(SpriteCount < 1) {
        }
    }
    /// <summary>
    /// 调整角度
    /// </summary>
    public void DirAngle(){

        mousescreenPosition = Input.mousePosition;
        mousescreenPosition.z = 0;
        screenPositionInworld = Camera.main.ScreenToWorldPoint(mousescreenPosition);
        screenPositionInworld.z = 0;

        Debug.Log(mousescreenPosition+"/"+ screenPositionInworld);
        
        Vector3 face = transform.up;
        direction = screenPositionInworld - PlayerSuperCtrl.instance.transform.position;
        float angle = Vector3.SignedAngle(face, direction, Vector3.forward);

        angle += BulletSpreadRange;

        transform.Rotate(0, 0, angle);
    }
    public void Function(){
        transform.Translate(0, BulletSpeed, 0, Space.Self);
    }
    void LoopBulletSprite() {
        SpriteLoopTimer += Time.deltaTime;
        if(SpriteLoopTimer >= BulletSpriteLoopFrequency) {
            SpriteLoopTimer = 0;
            SpriteCurrentIndex++;
            if (SpriteCurrentIndex == SpriteCount)
                SpriteCurrentIndex = 0;

            SpriteRender.sprite = BulletSprites[SpriteCurrentIndex];
        }
    }
    public void outTimeReduceFunction() {
        timer += Time.deltaTime;
        if(timer >= reduceTime) {
            timer = 0;
            reduceBullet();
        }
    }
    public void reduceBullet() {
        isUsed = false;
        isLoopSprite = false;

        //gameObject.name = "reduce";

        BulletPool.GameObjectPoolManager.Instance.Recycle("BulletUR", gameObject);
    }

    public void HitEvent(GameObject gameObject,Vector2 pos) {
        var Player = PlayerSuperCtrl.instance.gameObject;
        if (gameObject.layer == EnemyLayer && enablePierce){
            gameObject.SendMessage("hurt", BulletDamage);

            if (enableRepel)
                gameObject.GetComponent<Force>()?.addRelativeForce(repelForce, -Player.transform.position);
        }
        else if(gameObject.layer == EnemyLayer && !enablePierce) {
            gameObject.SendMessage("hurt", BulletDamage);

            if (enableRepel)
                gameObject.GetComponent<Force>()?.addRelativeForce(repelForce,-Player.transform.position);

            reduceBullet();
        }
        else{
            reduceBullet();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        //Debug.Log("Trigger");
        HitEvent(collision.gameObject,collision.transform.position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //HitEvent(collision.gameObject, collision.transform.position);
    }
}
