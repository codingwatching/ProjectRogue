using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData;
/// <summary>
/// 玩家buff作用脚本
/// 2024.8.12 update
/// </summary>
public class PlayerBuff : MonoBehaviour
{
    [Tooltip("玩家BUFF列表")]
    public List<BuffPrefab> currentBuffList;
    [Tooltip("是否启用BUFF")]
    public bool enableApplyBuff = false;

    void Start(){
        //StartCoroutine(Timer());
    }
    void Update(){
        
    }
    //每隔一秒遍历buff列表进行刷新
    IEnumerator Timer(){
        while (enableApplyBuff){
            yield return new WaitForSeconds(1.0f);
            refreshBuffEffect();
        }
    }
    //添加buff
    public void addBuff(BuffPrefab buff) {
        buff.currentTime = buff.LastTime;
        currentBuffList.Add(buff);
    }
    //刷新buff
    public void refreshBuffEffect() { 
        if(currentBuffList.Count != 0) { 
            for(int i = 0; i < currentBuffList.Count; i++) {
                currentBuffList[i].currentTime--;
                if(currentBuffList[i].currentTime <= 0) {
                    recoverBuffEffect(currentBuffList[i]);
                    currentBuffList.RemoveAt(i);
                }
                else {
                    applyBuffEffect(currentBuffList[i]);
                }
            }
        }
    }
    //应用buff效果
    public void applyBuffEffect(BuffPrefab buff) { 
        if(buff.ApplyType == PlayerDataType.Speed) {
            Speed += buff.Parameter;
        }
        if(buff.ApplyType == PlayerDataType.Blood) {
            Blood += buff.Parameter;
        }
        if(buff.ApplyType == PlayerDataType.Damage) {
            DamageMultiplier += buff.Parameter;
        }
        if(buff.ApplyType == PlayerDataType.EmissionRate) {
            EmissionRate += buff.Parameter;
        }
    }
    //恢复buff效果
    public void recoverBuffEffect(BuffPrefab buff) { 
        if(buff.ApplyType == PlayerDataType.Speed) {
            Speed -= buff.Parameter;
        }
        if(buff.ApplyType == PlayerDataType.Damage) {
            DamageMultiplier -= buff.Parameter;
        }
        if(buff.ApplyType == PlayerDataType.EmissionRate) {
            EmissionRate -= buff.Parameter;
        }
    }
}
