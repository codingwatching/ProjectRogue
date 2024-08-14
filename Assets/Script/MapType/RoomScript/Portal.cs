using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// ´«ËÍÃÅ
/// 2024.8.14 update C
/// </summary>
public class Portal : MonoBehaviour
{
    public string SceneName;

    bool isTrigger = false;

    void Start(){
        
    }
    void Update(){
        
    }
    public void gotoScene(string name) => SceneManager.LoadScene(SceneName);
    private void OnTriggerEnter2D(Collider2D collision){
        
    }
    private void OnTriggerExit2D(Collider2D collision){
        
    }
}
