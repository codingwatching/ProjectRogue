using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
public class TestTask : MonoBehaviour
{
    void Start(){
        Test();
        Test2();
    }
    void Update(){
        
    }
    public async void Test() {
        await UniTask.Delay(TimeSpan.FromSeconds(4), ignoreTimeScale: false);
        Debug.Log("Test Success!");
    }
    public async void Test2() {
        await UniTask.Delay(2000);
        Debug.Log("Delay 2s");
    }
}
