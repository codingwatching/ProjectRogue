using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// π§æﬂ¿‡
/// </summary>
public class Tools
{
    //Texture to Sprite
    public static Sprite Tex2Sp(Texture2D tex2d) => Sprite.Create(tex2d, new Rect(0,0, tex2d.width, tex2d.height),new Vector2(0.5f,0.5f),16);
    public static Sprite Tex2Weapon(Texture2D tex2d) => Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height), new Vector2(0.25f, 0.5f), 16);
    public static Sprite Tex2Sp(Texture2D tex2d,int pixel) => Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height), Vector2.zero, pixel);
    public static Sprite[] Tex2Sp(Texture2D tex2d,int pixel,int index){
        Sprite[] sprites;
        List<Sprite> sp = new List<Sprite>();
        int width = tex2d.width / index;
        for(int i=0;i< index; i++){
             sp.Add(Sprite.Create(tex2d, new Rect(width * i, 0, width, tex2d.height), Vector2.zero, pixel));
        }
        sprites = sp.ToArray();
        return sprites;
    }

    public static Texture2D Load(string s) => Resources.Load(s) as Texture2D;
    public static Material LoadMaterial(string s) => Resources.Load(s) as Material;
    public static GameObject LoadPrefab(string s) => Resources.Load(s) as GameObject;

    //Vector Tool
    public static Vector2 getMousePoint(){
        Vector3 point = Input.mousePosition;
        point.z = 0;
        float x = Camera.main.ScreenToWorldPoint(point).x;
        float y = Camera.main.ScreenToWorldPoint(point).y;
        return new Vector2(x, y);
    }
    public static Vector2Int getMousePointInt(){
        Vector3 point = Input.mousePosition;
        point.z = 0;
        int x = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(point).x);
        int y = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(point).y);
        return new Vector2Int(x, y);
    }
    public static Vector2 getMousePointV2() {
        Vector3 point = Input.mousePosition;
        return new Vector2(
            Camera.main.ScreenToWorldPoint(point).x,
            Camera.main.ScreenToWorldPoint(point).y);
    }
    public static Vector2Int xy(int x,int y) => new Vector2Int(x,y);
    //public static Vector2 xy(int x, int y) => new Vector2Int(x, y);
    public static Vector2Int v2int(Vector2 v2) => new Vector2Int((int)Mathf.Round(v2.x), (int)Mathf.Round(v2.y));
    public static Vector2 vf2i(Vector2 floatv2) => new Vector2(Mathf.Round(floatv2.x), Mathf.Round(floatv2.y));
    public static Vector2 v3to2(Vector3 v3) => new Vector2(v3.x,v3.y);

    //Random index
    public static float Rnd() => Random.Range(0,1.0f);
    public static int Rand(int max) => Random.Range(0,max);
    public static int Rand(int start, int end) => Random.Range(start, end);
    public static float R2f(float max) => Random.Range(0, max);
    public static float R2f(float start,float end) => Random.Range(start, end);

    //Random Trigger
    public delegate void rndFunc();
    public static void rndFunction(float rate,rndFunc func) {
        float index = Random.Range(0f, 1f);
        if(rate >= index) {
            func();
        }
    }
    [System.Serializable]
    public struct Range {
        public int min;
        public int max;
    }
    [System.Serializable]
    public struct TileSwapSet {
        public TileBase OriginTile;
        public TileBase AfterTile; 
    }
}
