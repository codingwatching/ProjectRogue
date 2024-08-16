using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class CamControl : MonoBehaviour
{
    public static CamControl Instance;
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        }
        Instance = this;
    }
    [Header("Componet")]
    public CinemachineVirtualCamera cam;
    public CinemachineConfiner camConfiner;
    [Header("Confiner")]
    public PolygonCollider2D Universal;
    public List<Poly> confinerList = new List<Poly>();
    [Header("Parameter")]
    public float rate;

    [System.Serializable]
    public struct Poly {
        public string name;
        public PolygonCollider2D poly;
    }

    bool canReDelay = false;

    void Start(){

    }
    void Update(){
        if (canReDelay) reDelay();
    }
    public void setConfiner(string name) {
        PolygonCollider2D poly2d=null;
        foreach(var s in confinerList) { 
            if(s.name == name) {
                poly2d = s.poly;
            }
        }
        camConfiner.m_BoundingShape2D = poly2d;
    }
    public void setDefault() => camConfiner.m_BoundingShape2D = Universal;
    public void setDefault(string name) { 
        PolygonCollider2D poly2d=null;
        foreach(var s in confinerList) { 
            if(s.name == name) {
                poly2d = s.poly;
            }
        }
        Universal = poly2d; 
    }
    public void setDelay(int index) {
        camConfiner.m_Damping = index;
        canReDelay = true;
    }
    public void reDelay() {
        camConfiner.m_Damping -= rate;
        if (camConfiner.m_Damping <= 0) {
            canReDelay = false;
        }
    }
}
