using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static RoomData;
using static RoomGenerator;

//连接房间脚本
public class RoomJunction : MonoBehaviour
{
    public Vector2Int worldPos;
    public Vector2Int roomPos;
    public Vector2Int Rect = new Vector2Int(15, 15);
    public Tilemap tilemap;
    public Tilemap colliderMap;
    public int index;

    void Start(){
        
    }
    void Update(){
        
    }
    //产生分支房间
    public void getElseRoom() {
        if (instance.World[worldPos.x - 1, worldPos.y] == 0){
            reset2ThisNode();
            instance.genNormalRoom(Forward.Left);
            Debug.Log("Has Left");
        }
        if (instance.World[worldPos.x + 1, worldPos.y] == 0){
            reset2ThisNode();
            instance.genNormalRoom(Forward.Right);
            Debug.Log("Has Right");
        }
        if (instance.World[worldPos.x, worldPos.y - 1] == 0){
            reset2ThisNode();
            instance.genNormalRoom(Forward.Down);
            Debug.Log("Has Down");
        }
        if (instance.World[worldPos.x, worldPos.y + 1] == 0){
            reset2ThisNode();
            instance.genNormalRoom(Forward.Top);
            Debug.Log("Has Top");
        }
    }
    //重置坐标下标到当前节点
    public void reset2ThisNode() {
        instance.currentIndex = worldPos;
        instance.currentPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }
    //检测路网连接
    public void checkRectRoadTile() { 
        for(int x =roomPos.x;x<roomPos.x + Rect.x;x++) {
            if (checkRoadConnect(new Vector2Int(x, roomPos.y))) {
                setDoorTile(new Vector2Int(x, roomPos.y));
            }
            if (checkRoadConnect(new Vector2Int(x, roomPos.y+Rect.y-1))) {
                setDoorTile(new Vector2Int(x, roomPos.y+Rect.y-1));
            }
        }
        for(int y =roomPos.y;y<roomPos.y + Rect.y;y++) {
            if (checkRoadConnect(new Vector2Int(roomPos.x,y))) {
                setDoorTile(new Vector2Int(roomPos.x,y));
            }
            if (checkRoadConnect(new Vector2Int(roomPos.x+Rect.x-1, y))) {
                setDoorTile(new Vector2Int(roomPos.x+Rect.x-1, y));
            }
        }
    }
    public bool checkRoadConnect(Vector2Int pos) {
        if(isConnectRoad(new Vector2Int(pos.x - 1, pos.y))) { //Left
            return true;
        }
        else if (isConnectRoad(new Vector2Int(pos.x + 1, pos.y))){ //Right
            return true;
        }
        else if (isConnectRoad(new Vector2Int(pos.x, pos.y + 1))){ // Top
            return true;
        }
        else if (isConnectRoad(new Vector2Int(pos.x, pos.y - 1))){ //Down
            return true;
        }
        else return false;
    }
    public Vector2Int NormalizeV2(Vector2Int pos) => new Vector2Int(pos.x - roomPos.x - Rect.x / 2, pos.y - roomPos.y - Rect.y / 2);
    public Vector3Int NormalizeV3(Vector2Int pos) => new Vector3Int(pos.x - roomPos.x - Rect.x / 2, pos.y - roomPos.y - Rect.y / 2 , 0);
    public bool isConnectRoad(Vector2Int pos) {
        if (RoomManager.instance.checkTilemap.GetTile(new Vector3Int(pos.x, pos.y, 0)) == RoomManager.instance.RoadTile) return true;
        else return false;
    }
    public void setDoorTile(Vector2Int pos) {
        colliderMap.SetTile(NormalizeV3(pos),null);
    }
}
