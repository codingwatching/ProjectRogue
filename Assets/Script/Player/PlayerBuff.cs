using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData;
/// <summary>
/// ���buff���ýű�
/// 2024.8.12 update
/// </summary>
public class PlayerBuff : MonoBehaviour
{
    [Tooltip("���BUFF�б�")]
    public List<BuffPrefab> currentBuffList;
    [Tooltip("�Ƿ�����BUFF")]
    public bool enableApplyBuff = false;

    void Start(){
        //StartCoroutine(Timer());
    }
    void Update(){
        
    }
    //ÿ��һ�����buff�б����ˢ��
    IEnumerator Timer(){
        while (enableApplyBuff){
            yield return new WaitForSeconds(1.0f);
            refreshBuffEffect();
        }
    }
    //���buff
    public void addBuff(BuffPrefab buff) {
        buff.currentTime = buff.LastTime;
        currentBuffList.Add(buff);
    }
    //ˢ��buff
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
    //Ӧ��buffЧ��
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
    //�ָ�buffЧ��
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
