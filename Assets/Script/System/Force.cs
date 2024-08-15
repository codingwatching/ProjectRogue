using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 通用刚体类 接收外力/施加外力
/// 2024.8.15 update C
/// </summary>
public class Force : MonoBehaviour
{
    private Rigidbody2D rgd;

    void Start(){
        rgd = GetComponent<Rigidbody2D>();
    }
    void Update(){       
    }
    /// <summary>
    /// 重置速度为0
    /// </summary>
    public void resetVelocity() => GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    public void resetVelocity(string dir) { 
        if (dir == "x") GetComponent<Rigidbody2D>().velocity = new Vector2(0, rgd.velocity.y);
        if (dir == "y") GetComponent<Rigidbody2D>().velocity = new Vector2(rgd.velocity.x, 0);
    }
    /// <summary>
    /// 重置速度为原速度的几倍
    /// </summary>
    public void fixVelocity(float rate) {
        var rgd = GetComponent<Rigidbody2D>();
        rgd.velocity = new Vector2(rgd.velocity.x * rate,rgd.velocity.y * rate);
    }
    /// <summary>
    /// 施加外力
    /// </summary>
    /// <param name="power">力的大小(100unit/per)</param>
    /// <param name="source">施力来源/Vector3</param>
    public void addForce(float power,Vector3 source) {
        float distant = Vector3.Distance(transform.position,source);
        Vector3 dir = Vector3.Normalize(transform.position - source);
        float newForce = power/distant;
        //Debug.Log(distant+":distant/"+newForce+"Force!"+gameObject.name);
        if(newForce >= 1)
            rgd.AddForce(new Vector2(dir.x*newForce,dir.y*newForce),ForceMode2D.Force);
    }
    /// <summary>
    /// 朝一个目标施加外力
    /// </summary>
    /// <param name="power">力</param>
    /// <param name="target">目标位置</param>
    public void addDirForce(float power,Vector3 target) {
        Vector3 dir = Vector3.Normalize(target);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * power, dir.y * power), ForceMode2D.Force);
    }
    /// <summary>
    /// 相对于物体坐标朝一个目标施加外力
    /// </summary>
    /// <param name="power">力的大小</param>
    /// <param name="target">目标位置</param>
    public void addRelativeForce(float power,Vector3 target) {
        Vector3 relative = target - transform.position;
        Vector3 dir = Vector3.Normalize(relative);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * power, dir.y * power), ForceMode2D.Force);
    }
    public void addAnlgeForce(float power,float angle) {
        Vector2 dir = new Vector2(Mathf.Sin(angle),Mathf.Cos(angle));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * power, dir.y * power), ForceMode2D.Force);
    }
}
