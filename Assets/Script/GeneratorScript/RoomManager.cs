using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }
    public Tilemap checkTilemap;

    public TileBase DoorTile;
    public TileBase IndexTile;
    public TileBase RoadTile;
    void Start(){
        
    }
    void Update(){
        
    }
}
