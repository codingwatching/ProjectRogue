using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Data;
/// <summary>
/// ����������
/// 2024.8.14 update C
/// </summary>
public class RoomAdaptor : MonoBehaviour
{
    [Header("�������")]
    public Vector2Int worldIndexPos;//����index���� ռλ��
    public Vector2Int roomPos;//ʵ������
    public Vector2Int Rect;//�����С

    public DoorTileSwap doorTile;
    [Header("�������")]
    public Tilemap RenderMap;//��ͼ��
    public Tilemap ColliderMap;//��ײ��
    [Space]
    public int index;

    public GameObject RoomNode;
    public bool isEnterRoom = false;

    //������ϼ�
    List<Vector3Int> DoorPosition = new List<Vector3Int>();

    void Start(){
        
    }
    void Update(){
        
    }
    public Vector3 getRndPos() {
        return new Vector3(Random.Range(roomPos.x+2,roomPos.x+Rect.x-2), Random.Range(roomPos.y + 2, roomPos.y + Rect.y -2));
    }
    //���μ���Ƿ���·������
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
    //��ⵥ��tile4���Ƿ���·������
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
    //�����������Ƿ���·��tile
    public bool isConnectRoad(Vector2Int pos) {
        if (RoomManager.instance.checkTilemap.GetTile(new Vector3Int(pos.x, pos.y, 0)) == RoomManager.instance.RoadTile) return true;
        else return false;
    }
    //������tile
    public void setDoorTile(Vector2Int pos) {
        DoorPosition.Add(new Vector3Int(pos.x, pos.y, 0));
        ColliderMap.SetTile(NormalizeV3(pos),null);
        //RenderMap.SetTile(NormalizeV3(pos),doorTile.Door_IdleTile);
    }
    public void closeDoor() { 
        foreach(var val in DoorPosition) {
            RenderMap.SwapTile(doorTile.Door_IdleTile,doorTile.Door_ActiveAnimTile);
            //ColliderMap.SetTile(NormalizeV3(pos), null);
        }
    }
    public void openDoor() { 
        foreach(var val in DoorPosition) {
            RenderMap.SwapTile(doorTile.Door_IdleTile,doorTile.Door_DisactiveAnimTile);
            ColliderMap.SetTile(val, null);
        }
    }
    //----
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == PlayerLayer) {
            isEnterRoom = true;
            RoomNode.GetComponent<IRoomNode>().onEnterRoom();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == PlayerLayer) {
            isEnterRoom = false;
            RoomNode.GetComponent<IRoomNode>().onExitRoom();
        }
    }
}
