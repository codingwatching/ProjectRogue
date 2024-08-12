using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ×·×Ù×Óµ¯Àà ´ýÍêÉÆ
/// 2024.8.12 update C
/// </summary>
public class CurveBullet : MonoBehaviour
{
    public float speed=5;
    public float damage = 20f;
    public bool enableBreak = false;
    public bool enableHurtPlayer=false;
    public bool enableTraceLast = true;

    float timer = 0;

    void Start(){
        outTimeReduceFunction();
    }
    public void loadData(float in_damage) {
        damage = in_damage;
    }
    public IEnumerator Move(Vector2 start,Vector2 midpoint,Transform target) {
        for(float i=0;i<=1;i+= Time.deltaTime) {
            Vector2 p1 = Vector2.Lerp(start,midpoint,i);
            Vector2 p2 = Vector2.Lerp(midpoint, target.position, i);
            Vector2 p = Vector2.Lerp(p1, p2, i);
            yield return StartCoroutine(moveToPoint(p));
        }
        if(enableTraceLast)yield return StartCoroutine(moveToTarget(target));
    }
    IEnumerator moveToPoint(Vector2 pos) { 
        while(Vector2.Distance(transform.position,pos) > 0.1f) {
            Vector2 dir = pos - new Vector2(transform.position.x,transform.position.y);
            transform.up = dir;
            transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * speed);
            yield return null;
        }
    }
    IEnumerator moveToTarget(Transform target) { 
        while(Vector2.Distance(transform.position, target.position) > 0.1f) {
            Vector2 dir = target.position - transform.position;
            transform.up = dir;
            transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
            yield return null;
        }
    }
    void Update(){
        Function();
        outTimeReduceFunction();
    }
    public void Function(){
        RaycastHit2D rayhit = Physics2D.Raycast(transform.position,transform.up, 0.5f);
        if (rayhit.collider != null){
            HitEvent(rayhit.collider.gameObject,rayhit.point);
        }
    }
    public void outTimeReduceFunction() {
        timer += Time.deltaTime;
        if(timer >= 3f) {
            timer = 0;
            Destroy(gameObject);
        }
    }
    public void HitEvent(GameObject obj,Vector2 pos) {
        /*if (!enableHurtPlayer && obj.layer == EnemyEnt){
            obj.SendMessage("hurt", damage);
            HitFunction(pos);
        }
        if (enableHurtPlayer) {
            if (obj.layer == PlayerLayer) {
                OnHit(damage);
                HitFunction(pos);
            }
            if(obj.layer == Map) {
                HitFunction(pos);
            }
        }*/
    }
    public void HitFunction(Vector2 pos) {
        /*var Sparkers = Instantiate(ObjectLoader.Spark);
        Sparkers.transform.position = pos;

        Destroy(gameObject);*/
    }
    public void hurt(float damage){
        if(enableBreak)HitFunction(transform.position);
    }
}
