using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 废弃子弹类
/// 2024.8.12 update C
/// </summary>
public class UniversalBullet : MonoBehaviour
{
    [Header("Parameter")]
    public bool isUsed = false;
    public float reduceTime = 2f;
    [Space]
    public bool enablePierce = false;
    public int pierceNum = 3;
    [Space]
    public bool enableRepel = false;
    public float repelForce=0;
    [Space]
    public bool enableBounce = false;

    [HideInInspector]
    public float BulletSpeed = 0.1f;
    public float BulletDamage = 0;
    public float BulletSpreadRange = 0;

    Vector3 mousescreenPosition;
    Vector3 screenPositionInworld;

    Vector3 direction;

    float timer = 0;

    void Start(){
    }
    void Update(){
    }
    void FixedUpdate(){
        if (isUsed){
            Function();
            outTimeReduceFunction();
        }
    }
    public void ShootFunc() {
        isUsed = true;
        DirAngle();
    }
    /// <summary>
    /// 调整角度
    /// </summary>
    public void DirAngle(){

        mousescreenPosition = Input.mousePosition;
        mousescreenPosition.z = 0;
        screenPositionInworld = Camera.main.ScreenToWorldPoint(mousescreenPosition);
        screenPositionInworld.z = 0;

        Vector3 face = transform.up;
        direction = screenPositionInworld - PlayerSuperCtrl.instance.transform.position;
        float angle = Vector3.SignedAngle(face, direction, Vector3.forward);

        angle += BulletSpreadRange;

        transform.Rotate(0, 0, angle);
    }
    public void Function(){
        transform.Translate(0, BulletSpeed, 0, Space.Self);
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
        //Destroy(gameObject);
        gameObject.name = "reduce";
        BulletPool.GameObjectPoolManager.Instance.Recycle("BulletUR", gameObject);
    }
    public void HitEvent(GameObject gameObject,Vector2 pos) {
        /*if (gameObject.layer == EnemyEnt && enablePierce && pierceCount < pierceNum){
            gameObject.SendMessage("hurt", loadWeapon.Damage * multipler);
            pierceCount++;
            multipler -= 0.1f;

            if (enableRepel)
                gameObject.GetComponent<Force>()?.addForce(loadWeapon.RepelForce, pos);
        }
        else if(gameObject.layer == EnemyEnt && !enablePierce) {
            gameObject.SendMessage("hurt", loadWeapon.Damage);

            if(enableRepel)
                gameObject.GetComponent<Force>()?.addForce(loadWeapon.RepelForce, pos);

            reduceBullet();
        }
        else{
            reduceBullet();
        }

        var Sparkers = Instantiate(ObjectLoader.Spark);
        Sparkers.transform.position = pos;*/
    }
}
