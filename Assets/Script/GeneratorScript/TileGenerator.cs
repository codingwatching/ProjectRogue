using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public static TileGenerator instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }

    public Tilemap roadTilemap;
    public Tilemap checkTilemap;

    public TileBase[] roadTile;

    public TileBase roadWall;
    void Start(){
        
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha5)){
            genRoadTile();
        }
    }
    public void genRoadTile() { 
        foreach(var outerList in RoomGenerator.instance.RoadCollection) { 
            foreach(var innerList in outerList) {
                checkRoadWall(innerList);
                setRoadTile(innerList);
            }
        }
    }
    public void checkRoadWall(Vector2Int pos) {
        Vector2Int nearby = new Vector2Int(pos.x,pos.y);
        if(isCollide(new Vector2Int(pos.x - 1, pos.y))) { //Left
            nearby = new Vector2Int(pos.x - 1, pos.y);
            setRoadWall(nearby);
        }
        if (isCollide(new Vector2Int(pos.x + 1, pos.y))){ //Right
            nearby = new Vector2Int(pos.x + 1, pos.y);
            setRoadWall(nearby);
        }
        if (isCollide(new Vector2Int(pos.x, pos.y + 1))){ // Top
            nearby = new Vector2Int(pos.x, pos.y + 1);
            setRoadWall(nearby);
        }
        if (isCollide(new Vector2Int(pos.x, pos.y - 1))){ //Down
            nearby = new Vector2Int(pos.x, pos.y - 1);
            setRoadWall(nearby);
        }

        if(isCollide(new Vector2Int(pos.x - 1, pos.y -1))) { 
            nearby = new Vector2Int(pos.x - 1, pos.y -1);
            setRoadWall(nearby);
        }
        if (isCollide(new Vector2Int(pos.x + 1, pos.y+1))){ 
            nearby = new Vector2Int(pos.x + 1, pos.y+1);
            setRoadWall(nearby);
        }
        if (isCollide(new Vector2Int(pos.x - 1, pos.y + 1))){ 
            nearby = new Vector2Int(pos.x - 1, pos.y + 1);
            setRoadWall(nearby);
        }
        if (isCollide(new Vector2Int(pos.x + 1, pos.y - 1))){ 
            nearby = new Vector2Int(pos.x + 1, pos.y - 1);
            setRoadWall(nearby);
        }
    }
    public void setRoadTile(Vector2Int pos) {
        roadTilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), roadTile[Random.Range(0,roadTile.Length)]);
    }
    public void setRoadWall(Vector2Int pos) => roadTilemap.SetTile(new Vector3Int(pos.x, pos.y, 0),roadWall);
    public bool isCollide(Vector2Int pos) => checkTilemap.GetTile(new Vector3Int(pos.x, pos.y, 0)) == null;
}
