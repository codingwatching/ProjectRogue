using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using static RoomData;
/// <summary>
/// 地图生成器
/// 2024.8.12 update C
/// </summary>
public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator instance;
    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }

    public GameObject Grid;

    [Tooltip("房间生成总数")]
    public int roomTotalNum=10;
    [Tooltip("房间生成间距范围")]
    public Vector2Int rangeOffset;
    [Tooltip("房间生成位置波动范围")]
    public int shakeOffset;

    [Space]
    public Tilemap indexMap;
    public TileBase indexTile;
    public TileBase roadTile;
    [Space]
    public RoomSequence sequence;
    public GameObject roadNodePrefab; //预生成的路口节点房间
    public List<RoomPrefab> roomPrefabList = new List<RoomPrefab>(); //预生成so房间列表 => GenRoom

    [Space]
    [Header("游戏内用参数")]
    //InGame Using
    public int[,] World = new int[32, 32]; //世界index坐标 占位符（0/1）
    public GameObject[,] WorldPrefab = new GameObject[32, 32]; //世界index坐标 gameobj型

    public Vector2Int currentPosition = new Vector2Int(128, 128); //起始坐标
    public Vector2Int currentIndex = new Vector2Int(16,16);  //起始index

    int roomPrefabListCount = 0; //列表房间数量
    int currentRoomCount = 0; //当前房间生成数量

    int loopCount = 0; //当前循环次数
    int loopMax = 50; //最大循环次数

    Forward FromDir;
    bool IsMainRoomGenFinish = false; //是否完成主线生成

    public List<GameObject> Room = new List<GameObject>(); //已生成的so中的gameobj
    //SO生成列表 / 房间世界坐标
    public struct RoomNode {
        public Vector2Int pos;
        public RoomPrefab roomPrefab;
    }
    public List<RoomNode> roomGenList = new List<RoomNode>();//已生成房间roomNode结构体

    public List<GameObject> roadList = new List<GameObject>();//路口房间list gameobj 

    public List<List<Vector2Int>> RoadCollection = new List<List<Vector2Int>>();//路网列表  —  存储全部路tile的详细坐标

    public List<List<GameObject>> RoadNodeCollection = new List<List<GameObject>>();//路口各方向连接gameobj合集

    void Start(){
        roomPrefabListCount = roomPrefabList.Count;

        loadSequenceGenMapRoom(sequence.so_roomSequence, sequence.so_subRoomGenIndex);
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            newGenMapRoom();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            foreach (var val in roadList)
                loadRoadNode4VectorRoom(val.GetComponent<RoomJunction>().worldPos);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            connectRoomRoad();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { 
            foreach(var val in Room) {
                val.GetComponent<RoomAdaptor>().checkRectRoadTile();
            }
            foreach(var val in roadList) {
                val.GetComponent<RoomJunction>().checkRectRoadTile();
            }
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            for(int x = 0; x < 32; x++) {
                for (int y = 0; y < 32; y++)
                    if (World[x, y] == 1)
                        Debug.Log(new Vector2Int(x,y)+"Index");
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            roadList[0].GetComponent<RoomJunction>().getElseRoom();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            roadList[1].GetComponent<RoomJunction>().getElseRoom();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            loadSequenceGenMapRoom(sequence.so_roomSequence,sequence.so_subRoomGenIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            StandardMapGen();
        }
    }
    //按顺序so生成房间
    public void loadSequenceGenMapRoom(List<RoomType> roomSquence,List<int> subRoomGenIndex) {

        startRoomGen();

        for (int val =0;val< roomSquence.Count; val++) {
            if (roomSquence[val] == RoomType.Start) genStartRoom();
            if (roomSquence[val] == RoomType.Normal) genNormalRoom(getRndForward());
            if (roomSquence[val] == RoomType.Bonus) genNormalRoom(getRndForward());
            if (roomSquence[val] == RoomType.Boss) genBossRoom(getRndForward());
            if (roomSquence[val] == RoomType.End) genEndRoom(getRndForward());

            if(val != roomSquence.Count - 1) {
                genCorridorRoom();
            }
        }

        FinishRoomGen();
        foreach(var val in subRoomGenIndex) {
            roadList[val].GetComponent<RoomJunction>().getElseRoom();
            Debug.Log("subRoomIndex:"+val);
        }

        foreach (var val in roadList)
            loadRoadNode4VectorRoom(val.GetComponent<RoomJunction>().worldPos);

        connectRoomRoad();

        foreach(var val in Room) {
            val.GetComponent<RoomAdaptor>().checkRectRoadTile();
        }
        foreach(var val in roadList) {
            val.GetComponent<RoomJunction>().checkRectRoadTile();
        }

        TileGenerator.instance.genRoadTile();
    }
    //旧编码生成房间
    public void newGenMapRoom() {
        startRoomGen();

        genStartRoom();
        genCorridorRoom();
        genNormalRoom(getRndForward());
        genCorridorRoom();
        genBossRoom(getRndForward());
        genCorridorRoom();
        genEndRoom(getRndForward());

        FinishRoomGen();
    }
    //完整地图生成
    public void StandardMapGen() {
        newGenMapRoom();
        genElseRoom();

        foreach (var val in roadList)
            loadRoadNode4VectorRoom(val.GetComponent<RoomJunction>().worldPos);

        connectRoomRoad();

        foreach(var val in Room) {
            val.GetComponent<RoomAdaptor>().checkRectRoadTile();
        }
        foreach(var val in roadList) {
            val.GetComponent<RoomJunction>().checkRectRoadTile();
        }

        TileGenerator.instance.genRoadTile();
    }
    public void genElseRoom() {
        roadList[0].GetComponent<RoomJunction>().getElseRoom();
        roadList[1].GetComponent<RoomJunction>().getElseRoom();
    }
    //初始生成房间-全部数据清空
    public void startRoomGen() {
        if (IsMainRoomGenFinish){
            //清空全部list，以及gameobj
            foreach(var roomVal in Room) {
                Destroy(roomVal.gameObject);
            }
            foreach(var roadVal in roadList) {
                Destroy(roadVal.gameObject);
            }
            RoadCollection.Clear();
            RoadNodeCollection.Clear();
            roomGenList.Clear();
            Room.Clear();
            roadList.Clear();

            for (int x = 0; x < 32; x++){
                for (int y = 0; y < 32; y++){
                    World[x, y] = 0;
                    WorldPrefab[x, y] = null;
                }            
            }

            //恢复全部参数至默认
            currentPosition = new Vector2Int(128, 128);
            currentIndex = new Vector2Int(16,16);
            currentRoomCount = 0;
            loopCount = 0;

            //清空全部indexTilemap
            indexMap.ClearAllTiles();
            TileGenerator.instance.roadTilemap.ClearAllTiles();
            
            IsMainRoomGenFinish = false;
        }
        Debug.Log("--StartGenRoom--");
    }
    public void genStartRoom() {//起始房间生成
        int count = Random.Range(0, roomPrefabListCount);
        var prefab = roomPrefabList[count];
        var pos = currentPosition;

        genGameObjectRoom(prefab, pos , new Vector2Int(16,16));
        World[16, 16] = 1;

        Debug.Log("genStartRoom" + loopCount);
    }
    public Vector2Int genCorridorRoom() {//走廊房间生成
        Forward dir = getRndForward();
        var rndPos = genRndPosition(dir,rangeOffset);
        GameObject road=null;

        Debug.Log("genCorridorRoom" + dir + "/" + loopCount);

        loopCount++;
        if (!genRoadGameObjectRoom(roadNodePrefab, rndPos, new Vector2Int(15, 15),ref road) && loopCount <= loopMax) genCorridorRoom();
        else
        {
            var rndIndex = getWorldPos(dir, currentIndex);
            currentIndex = rndIndex;
            currentPosition = rndPos;
            FromDir = dir;
            World[currentIndex.x, currentIndex.y] = 1;
            road.GetComponent<RoomJunction>().worldPos = currentIndex;
            road.name = "Road:"+currentIndex;

            Debug.Log("GenSuccess:" + currentIndex + "- CorridorRoom");
        }
        return currentIndex;
    }
    public Vector2Int genNormalRoom(Forward dir) {//普通战斗房间生成
        int count = Random.Range(0, roomPrefabListCount);
        var prefab = roomPrefabList[count];
        var rndPos = genRndPosition(dir, rangeOffset);
        var rndIndex = getWorldPos(dir, currentIndex);

        Debug.Log("genNormalRoom/" +dir+"/"+ loopCount);

        loopCount++;

        if(checkIndexMap(rndIndex, dir) && loopCount <= loopMax) {
            if (!genGameObjectRoom(prefab, rndPos, rndIndex)){
                if(!IsMainRoomGenFinish) genNormalRoom(getRndForward());
                else genNormalRoom(dir);
            }
            else{
                currentIndex = rndIndex;
                currentPosition = rndPos;
                FromDir = dir;
                World[currentIndex.x, currentIndex.y] = 1;

                Debug.Log("GenSuccess:" + currentIndex + "- NormalRoom");
            }
        }else if(loopCount <= loopMax)
            genNormalRoom(getRndForward());
        else
            Debug.Log("--ShutDown Generation!--");

        return currentIndex;
    }
    public Vector2Int genBossRoom(Forward dir) {//Boss房间生成
        int count = Random.Range(0, roomPrefabListCount);
        var prefab = roomPrefabList[count];
        var rndPos = genRndPosition(dir, rangeOffset);
        var rndIndex = getWorldPos(dir, currentIndex);

        Debug.Log("genBossRoom" + dir + "/" + loopCount);

        loopCount++;
        if(checkIndexMap(rndIndex, dir) && loopCount <= loopMax) {
            if (!genGameObjectRoom(prefab, rndPos, rndIndex)){
                genBossRoom(getRndForward());
            }
            else{
                currentIndex = rndIndex;
                currentPosition = rndPos;
                FromDir = dir;
                World[currentIndex.x, currentIndex.y] = 1;

                Debug.Log("GenSuccess:" + currentIndex + "- BossRoom");
            }
        }else if (loopCount <= loopMax)
            genBossRoom(getRndForward());
        else
            Debug.Log("--ShutDown Generation!--");

        return currentIndex;
    }
    public Vector2Int genEndRoom(Forward dir) {//终点房间生成
        int count = Random.Range(0, roomPrefabListCount);
        var prefab = roomPrefabList[count];
        var rndPos = genRndPosition(dir, rangeOffset);
        var rndIndex = getWorldPos(dir, currentIndex);

        Debug.Log("genEndRoom" + dir + "/" + loopCount);

        loopCount++;
        if(checkIndexMap(rndIndex, dir) && loopCount <= loopMax) {
            if (!genGameObjectRoom(prefab, rndPos, rndIndex)){
                genEndRoom(getRndForward());
            }
            else{
                currentIndex = rndIndex;
                currentPosition = rndPos;
                FromDir = dir;
                World[currentIndex.x, currentIndex.y] = 1;

                Debug.Log("GenSuccess:" + currentIndex + "- EndRoom");
            }
        }else if (loopCount <= loopMax)
            genEndRoom(getRndForward());
        else
            Debug.Log("--ShutDown Generation!--");

        return currentIndex;
    }
    //房间结束后
    public void FinishRoomGen() {
        IsMainRoomGenFinish = true;
        loopCount = 0;
        Debug.Log("--Finish Main Room Gen!--");
    }
    //生成地图prefab / 填满占位tile / 房间数+1 
    public bool genGameObjectRoom(RoomPrefab prefab,Vector2Int pos,Vector2Int worldPos) {
        bool finish = false;
        if (!checkRoomRectCollide(pos, prefab.Rect)){
            fillRect(pos, prefab.Rect);
            currentRoomCount++;

            add2GenList(pos, prefab);
            genRoomPrefab(prefab, pos , worldPos);
            //WorldPrefab[worldPos.x, worldPos.y] = prefab;

            finish = true;
        }else
            finish = false;
        return finish;
    }
    public bool genRoadGameObjectRoom(GameObject prefab, Vector2Int pos, Vector2Int Rect, ref GameObject roadPrefab) {
        bool finish = false;
        if (!checkRoomRectCollide(pos,Rect)){
            fillRect(pos, Rect);
            genRoadPrefab(prefab, pos , Rect ,ref roadPrefab);
            finish = true;
        }else
            finish = false;
        return finish;
    }
    public void genRoadPrefab(GameObject prefab, Vector2Int pos, Vector2Int Rect,ref GameObject roadPrefab) {
        GameObject room = Instantiate(prefab);
        room.transform.parent = Grid.transform;
        room.transform.localPosition = new Vector3(pos.x + Rect.x / 2, pos.y + Rect.y / 2, 0);
        roadPrefab = room;
        room.GetComponent<RoomJunction>().roomPos = pos;
        
        roadList.Add(room);
    }
    //添加到 房间prefab列表
    public void add2GenList(Vector2Int pos,RoomPrefab prefab) {
        var roomNode = new RoomNode();
        roomNode.pos = pos;
        roomNode.roomPrefab = prefab;
        roomGenList.Add(roomNode);
    }
    //生成房间 prefabs /加入adaptor参数
    public void genRoomPrefab(RoomPrefab prefab,Vector2Int pos,Vector2Int worldPos) {
        GameObject room = Instantiate(prefab.roomPrefab);
        room.name = "Room:" + worldPos;
        room.transform.parent = Grid.transform;
        room.transform.localPosition = new Vector3(pos.x + prefab.Rect.x/2,pos.y + prefab.Rect.y/2, 0);
        room.GetComponent<RoomAdaptor>().roomPos = pos;
        room.GetComponent<RoomAdaptor>().Rect = prefab.Rect;
        room.GetComponent<RoomAdaptor>().index = currentRoomCount;
        Room.Add(room);
        WorldPrefab[worldPos.x, worldPos.y] = room;
    }
    //----
    //连接房间
    public void loadRoadNode4VectorRoom(Vector2Int pos) {
        List<GameObject> innerList = new List<GameObject>();
        if (World[pos.x + 1, pos.y] == 1) innerList.Add(WorldPrefab[pos.x + 1, pos.y]);
        if (World[pos.x - 1, pos.y] == 1) innerList.Add(WorldPrefab[pos.x - 1, pos.y]);
        if (World[pos.x, pos.y + 1] == 1) innerList.Add(WorldPrefab[pos.x, pos.y + 1]);
        if (World[pos.x, pos.y - 1] == 1) innerList.Add(WorldPrefab[pos.x, pos.y - 1]);
        RoadNodeCollection.Add(innerList);
    }
    // new ConnectRoad
    // 路网连接
    public void connectRoomRoad() {
        for(int i = 0; i < RoadNodeCollection.Count; i++) {
            List<Vector2Int> roadTileVectorList = new List<Vector2Int>();
            foreach (var val in RoadNodeCollection[i])
            {
                var junction = roadList[i].GetComponent<RoomJunction>();
                var room = val.GetComponent<RoomAdaptor>();
                var firstRoomMid = new Vector2Int(Mathf.RoundToInt(junction.transform.position.x), Mathf.RoundToInt(junction.transform.position.y));//roomGenList[i].roomPrefab.Rect / 2 + roomGenList[i].pos;
                var secondRoomMid = room.Rect / 2 + room.roomPos;//roomGenList[i + 1].roomPrefab.Rect / 2 + roomGenList[i + 1].pos;

                //Debug.Log(firstRoomMid+"/"+ secondRoomMid);

                var connectX = secondRoomMid.x - firstRoomMid.x;
                var connectY = secondRoomMid.y - firstRoomMid.y;

                int offsetX = 0;
                int offsetY = 0;

                if (connectX >= 0 && connectY >= 0)
                { //First Part
                    //Debug.Log(secondRoomMid.x+"/"+ (int)(roomGenList[i].pos.x+ roomGenList[i].roomPrefab.Rect.x));
                    //Debug.Log(firstRoomMid.y + "/" + roomGenList[i + 1].pos.y);
                    if (secondRoomMid.x == roomGenList[i].roomPrefab.Rect.x + roomGenList[i].pos.x - 1)
                    {
                        offsetX = 2;
                        Debug.Log("RightTopBlockY");
                    }
                    if (firstRoomMid.y == roomGenList[i + 1].pos.y)
                    {
                        offsetY = 2;
                        Debug.Log("RightTopBlockX");
                    }

                    for (int x = firstRoomMid.x; x < secondRoomMid.x; x++)
                    {
                        setRoad(new Vector2Int(x, firstRoomMid.y - offsetY), roadTileVectorList);
                        setRoad(new Vector2Int(x, firstRoomMid.y + 1 - offsetY), roadTileVectorList);
                        setRoad(new Vector2Int(x, firstRoomMid.y - 1 - offsetY), roadTileVectorList);
                    }
                    for (int y = firstRoomMid.y - 1 - offsetY; y < secondRoomMid.y; y++)
                    {
                        setRoad(new Vector2Int(secondRoomMid.x + offsetX, y), roadTileVectorList);
                        setRoad(new Vector2Int(secondRoomMid.x + 1 + offsetX, y), roadTileVectorList);
                        setRoad(new Vector2Int(secondRoomMid.x - 1 + offsetX, y), roadTileVectorList);
                    }
                }
                if (connectX < 0 && connectY >= 0)
                { //Second Part
                    if (secondRoomMid.x == roomGenList[i].pos.x)
                    {
                        offsetX = 2;
                        Debug.Log("LeftTopBlockY");
                    }
                    if (firstRoomMid.y == roomGenList[i + 1].pos.y)
                    {
                        offsetY = 2;
                        Debug.Log("LeftTopBlockX");
                    }

                    for (int x = firstRoomMid.x; x > secondRoomMid.x; x--)
                    {
                        setRoad(new Vector2Int(x, firstRoomMid.y - offsetY), roadTileVectorList);
                        setRoad(new Vector2Int(x, firstRoomMid.y + 1 - offsetY), roadTileVectorList);
                        setRoad(new Vector2Int(x, firstRoomMid.y - 1 - offsetY), roadTileVectorList);
                    }
                    for (int y = firstRoomMid.y - 1 - offsetY; y < secondRoomMid.y; y++)
                    {
                        setRoad(new Vector2Int(secondRoomMid.x - offsetX, y), roadTileVectorList);
                        setRoad(new Vector2Int(secondRoomMid.x + 1 - offsetX, y), roadTileVectorList);
                        setRoad(new Vector2Int(secondRoomMid.x - 1 - offsetX, y), roadTileVectorList);
                    }
                }
                if (connectX < 0 && connectY < 0)
                { //Third Part
                    if (secondRoomMid.x == roomGenList[i].pos.x)
                    {
                        offsetX = 2;
                        Debug.Log("LeftDownBlockY");
                    }
                    if (firstRoomMid.y == roomGenList[i + 1].pos.y)
                    {
                        offsetY = 2;
                        Debug.Log("LeftDownBlockX");
                    }

                    for (int x = firstRoomMid.x; x > secondRoomMid.x; x--)
                    {
                        setRoad(new Vector2Int(x, firstRoomMid.y + offsetY), roadTileVectorList);
                        setRoad(new Vector2Int(x, firstRoomMid.y + 1 + offsetY), roadTileVectorList);
                        setRoad(new Vector2Int(x, firstRoomMid.y - 1 + offsetY), roadTileVectorList);
                    }
                    for (int y = firstRoomMid.y + 1 + offsetY; y > secondRoomMid.y; y--)
                    {
                        setRoad(new Vector2Int(secondRoomMid.x - offsetX, y), roadTileVectorList);
                        setRoad(new Vector2Int(secondRoomMid.x + 1 - offsetX, y), roadTileVectorList);
                        setRoad(new Vector2Int(secondRoomMid.x - 1 - offsetX, y), roadTileVectorList);
                    }
                }
                if (connectX >= 0 && connectY < 0)
                { //Four Part
                    if (secondRoomMid.x == roomGenList[i].roomPrefab.Rect.x + roomGenList[i].pos.x - 1)
                    {
                        offsetX = 2;
                        Debug.Log("RightDownBlockY");
                    }
                    if (firstRoomMid.y == roomGenList[i + 1].pos.y)
                    {
                        offsetY = 2;
                        Debug.Log("RightDownBlockX");
                    }

                    for (int x = firstRoomMid.x; x < secondRoomMid.x; x++)
                    {
                        setRoad(new Vector2Int(x, firstRoomMid.y + offsetY), roadTileVectorList);
                        setRoad(new Vector2Int(x, firstRoomMid.y + 1 + offsetY), roadTileVectorList);
                        setRoad(new Vector2Int(x, firstRoomMid.y - 1 + offsetY), roadTileVectorList);
                    }
                    for (int y = firstRoomMid.y + 1 + offsetY; y > secondRoomMid.y; y--)
                    {
                        setRoad(new Vector2Int(secondRoomMid.x + offsetX, y), roadTileVectorList);
                        setRoad(new Vector2Int(secondRoomMid.x + 1 + offsetX, y), roadTileVectorList);
                        setRoad(new Vector2Int(secondRoomMid.x - 1 + offsetX, y), roadTileVectorList);
                    }
                }

                RoadCollection.Add(roadTileVectorList);
            }
        }
    }
    //旧道路连接
    /*public void connectRoomRoad() {
        for(int i = 0; i < roomGenList.Count; i++) {
            
            if (i != roomGenList.Count - 1){
                List<Vector2Int> roadList = new List<Vector2Int>();

                var firstRoomMid = roomGenList[i].roomPrefab.Rect / 2 + roomGenList[i].pos;
                var secondRoomMid = roomGenList[i + 1].roomPrefab.Rect / 2 + roomGenList[i + 1].pos;

                var connectX = secondRoomMid.x - firstRoomMid.x;
                var connectY = secondRoomMid.y - firstRoomMid.y;

                int offsetX = 0;
                int offsetY = 0;       

                if (connectX >= 0 && connectY >=0) { //First Part
                    //Debug.Log(secondRoomMid.x+"/"+ (int)(roomGenList[i].pos.x+ roomGenList[i].roomPrefab.Rect.x));
                    //Debug.Log(firstRoomMid.y + "/" + roomGenList[i + 1].pos.y);
                    if(secondRoomMid.x == roomGenList[i].roomPrefab.Rect.x + roomGenList[i].pos.x - 1) {
                        offsetX = 2;
                        Debug.Log("RightTopBlockY");
                    }
                    if(firstRoomMid.y == roomGenList[i + 1].pos.y){
                        offsetY = 2;
                        Debug.Log("RightTopBlockX");
                    }

                    for (int x = firstRoomMid.x; x < secondRoomMid.x; x++) { 
                        setRoad(new Vector2Int(x, firstRoomMid.y - offsetY), roadList);
                        setRoad(new Vector2Int(x, firstRoomMid.y + 1 - offsetY), roadList);
                        setRoad(new Vector2Int(x, firstRoomMid.y - 1 - offsetY), roadList);
                    }
                    for (int y = firstRoomMid.y - 1 - offsetY; y < secondRoomMid.y; y++) { 
                        setRoad(new Vector2Int(secondRoomMid.x + offsetX, y), roadList);
                        setRoad(new Vector2Int(secondRoomMid.x + 1 + offsetX, y), roadList);
                        setRoad(new Vector2Int(secondRoomMid.x - 1 + offsetX, y), roadList);
                    }
                }
                if (connectX < 0 && connectY >= 0){ //Second Part
                    if(secondRoomMid.x == roomGenList[i].pos.x) {
                        offsetX = 2;
                        Debug.Log("LeftTopBlockY");
                    }
                    if(firstRoomMid.y == roomGenList[i + 1].pos.y){
                        offsetY = 2;
                        Debug.Log("LeftTopBlockX");
                    }

                    for (int x = firstRoomMid.x; x > secondRoomMid.x; x--) { 
                        setRoad(new Vector2Int(x, firstRoomMid.y - offsetY), roadList);
                        setRoad(new Vector2Int(x, firstRoomMid.y + 1 - offsetY), roadList);
                        setRoad(new Vector2Int(x, firstRoomMid.y - 1 - offsetY), roadList);
                    }
                    for (int y = firstRoomMid.y - 1 - offsetY; y < secondRoomMid.y; y++) { 
                        setRoad(new Vector2Int(secondRoomMid.x - offsetX, y), roadList);
                        setRoad(new Vector2Int(secondRoomMid.x + 1 - offsetX, y), roadList);
                        setRoad(new Vector2Int(secondRoomMid.x - 1 - offsetX, y), roadList);
                    }
                }
                if (connectX < 0 && connectY < 0){ //Third Part
                    if(secondRoomMid.x == roomGenList[i].pos.x) {
                        offsetX = 2;
                        Debug.Log("LeftDownBlockY");
                    }
                    if(firstRoomMid.y == roomGenList[i + 1].pos.y){
                        offsetY = 2;
                        Debug.Log("LeftDownBlockX");
                    }

                    for (int x = firstRoomMid.x; x > secondRoomMid.x; x--) { 
                        setRoad(new Vector2Int(x, firstRoomMid.y + offsetY), roadList);
                        setRoad(new Vector2Int(x, firstRoomMid.y + 1 + offsetY), roadList);
                        setRoad(new Vector2Int(x, firstRoomMid.y - 1 + offsetY), roadList);
                    }
                    for (int y = firstRoomMid.y + 1 + offsetY; y > secondRoomMid.y; y--) { 
                        setRoad(new Vector2Int(secondRoomMid.x - offsetX, y), roadList);
                        setRoad(new Vector2Int(secondRoomMid.x + 1 - offsetX, y), roadList);
                        setRoad(new Vector2Int(secondRoomMid.x - 1 - offsetX, y), roadList);
                    }
                }
                if (connectX >= 0 && connectY < 0){ //Four Part
                    if(secondRoomMid.x == roomGenList[i].roomPrefab.Rect.x + roomGenList[i].pos.x - 1) {
                        offsetX = 2;
                        Debug.Log("RightDownBlockY");
                    }
                    if(firstRoomMid.y == roomGenList[i + 1].pos.y){
                        offsetY = 2;
                        Debug.Log("RightDownBlockX");
                    }

                    for (int x = firstRoomMid.x; x < secondRoomMid.x; x++) { 
                        setRoad(new Vector2Int(x, firstRoomMid.y + offsetY), roadList);
                        setRoad(new Vector2Int(x, firstRoomMid.y + 1 + offsetY), roadList);
                        setRoad(new Vector2Int(x, firstRoomMid.y - 1 + offsetY), roadList);
                    }
                    for (int y = firstRoomMid.y + 1 + offsetY; y > secondRoomMid.y; y--) { 
                        setRoad(new Vector2Int(secondRoomMid.x + offsetX, y), roadList);
                        setRoad(new Vector2Int(secondRoomMid.x + 1 + offsetX, y), roadList);
                        setRoad(new Vector2Int(secondRoomMid.x - 1 + offsetX, y), roadList);
                    }
                }

                RoadCollection.Add(roadList);
            }
        }
    }*/
    //检查道路的跟何方向连通
    public Vector2Int checkRoadConnect(Vector2Int pos) {
        Vector2Int nearby = new Vector2Int(pos.x,pos.y);
        if(isCollide(new Vector2Int(pos.x - 1, pos.y))) { //Left
            nearby = new Vector2Int(pos.x - 1, pos.y);
        }else
        if (isCollide(new Vector2Int(pos.x + 1, pos.y))){ //Right
            nearby = new Vector2Int(pos.x + 1, pos.y);
        }else
        if (isCollide(new Vector2Int(pos.x, pos.y + 1))){ // Top
            nearby = new Vector2Int(pos.x, pos.y + 1);
        }else
        if (isCollide(new Vector2Int(pos.x, pos.y - 1))){ //Down
            nearby = new Vector2Int(pos.x, pos.y - 1);
        }
        return nearby;
    }
    //替换 prefab中的 路与房间的连接处 （旧门口替换）
    public void setRoomDoor() {
        //路后端替换
        List<List<Vector2Int>> lastPoint = new List<List<Vector2Int>>();
        foreach (List<Vector2Int> innerList in RoadCollection)
        {
            List<Vector2Int> point = new List<Vector2Int>();
            Vector2Int endpoint1 = checkRoadConnect(innerList[innerList.Count - 1]);
            Vector2Int endpoint2 = checkRoadConnect(innerList[innerList.Count - 2]);
            Vector2Int endpoint3 = checkRoadConnect(innerList[innerList.Count - 3]);

            point.Add(endpoint1);
            point.Add(endpoint2);
            point.Add(endpoint3);

            lastPoint.Add(point);
        }
        for(int endRoad = 0; endRoad < Room.Count; endRoad++) {
            var room = Room[endRoad].GetComponent<RoomAdaptor>();
            foreach (var val in lastPoint[endRoad]) {
                room.setDoorTile(val);
            }
        }
        //路前端替换
        for (int i = 0; i < 4; i++) {
            var room = Room[i].GetComponent<RoomAdaptor>();

            var startPoint1 = checkRoadConnect(RoadCollection[i][0]);
            var startPoint2 = checkRoadConnect(RoadCollection[i][1]);
            var startPoint3 = checkRoadConnect(RoadCollection[i][2]);

            room.setDoorTile(startPoint1);
            room.setDoorTile(startPoint2);
            room.setDoorTile(startPoint3);           
        }
    }
    public void setRoad(Vector2Int pos,List<Vector2Int> list) {
        if (!isCollide(pos)){
            indexMap.SetTile(new Vector3Int(pos.x, pos.y, 0), roadTile);

            list.Add(pos);
        }
    }
    /// <summary>
    /// 获取随机位置
    /// </summary>
    /// <param name="dir">方向</param>
    /// <param name="range">范围</param>
    /// <returns></returns>
    public Vector2Int genRndPosition(Forward dir,Vector2Int range) {
        Vector2Int newGamePos = new Vector2Int();
        int distance = Random.Range(range.x, range.y);
        int offset = Random.Range(-shakeOffset, shakeOffset);
        if (dir == Forward.Left) newGamePos = currentPosition + new Vector2Int(-distance, offset);
        if (dir == Forward.Right) newGamePos = currentPosition + new Vector2Int(+distance, offset);
        if (dir == Forward.Top) newGamePos = currentPosition + new Vector2Int(offset, +distance);
        if (dir == Forward.Down) newGamePos = currentPosition + new Vector2Int(offset, -distance);
        return newGamePos;
    }
    public Vector2Int getWorldPos(Forward dir,Vector2Int currentPos) {
        Vector2Int newWorldPos= new Vector2Int();
        if (dir == Forward.Left) newWorldPos = new Vector2Int(currentPos.x - 1,currentPos.y);
        if (dir == Forward.Right) newWorldPos = new Vector2Int(currentPos.x + 1, currentPos.y);
        if (dir == Forward.Top) newWorldPos = new Vector2Int(currentPos.x, currentPos.y + 1);
        if (dir == Forward.Down) newWorldPos = new Vector2Int(currentPos.x, currentPos.y - 1);
        return newWorldPos;
    }
    //取随机方向 return Forward
    public Forward getRndForward() {
        var count = Random.Range(0,3);
        if (count == 0) return Forward.Right;
        else if (count == 1) return Forward.Top;
        else if (count == 2) return Forward.Left;
        else return Forward.Top;
    }
    public bool checkIndexMap(Vector2Int indexPos,Forward dir) {
        if (dir == Forward.Left && World[indexPos.x - 1, indexPos.y] == 1) return false;
        else if (dir == Forward.Right && World[indexPos.x + 1, indexPos.y] == 1) return false;
        else if (dir == Forward.Top && World[indexPos.x, indexPos.y + 1] == 1) return false;
        else if (dir == Forward.Down && World[indexPos.x, indexPos.y - 1] == 1) return false;
        else return true;
    }
    //检查矩形是否占用
    public bool checkRoomRectCollide(Vector2Int startPos, Vector2Int rect) {
        bool Collide = false;

        for(int x = startPos.x -3; x < startPos.x+rect.x+3; x++) {
            for (int y = startPos.y -3 ; y < startPos.y+rect.y+3; y++) {
                if(isCollide(new Vector2Int(x, y))) {
                    return true;
                }
                else { 
                    Collide = false;
                }
            }
        }
        return Collide;
    }
    /// <summary>
    /// 填满矩形
    /// </summary>
    /// <param name="startPos">起始地点（左下）</param>
    /// <param name="rect">矩形大小</param>
    public void fillRect(Vector2Int startPos , Vector2Int rect) { 
        for(int x =startPos.x; x< startPos.x + rect.x; x++) {
            for (int y = startPos.y; y < startPos.y + rect.y; y++) {
                indexMap.SetTile(new Vector3Int(x,y,0),indexTile);
            }
        }
    }
    //判断单个位置是否占用
    public bool isCollide(Vector2Int pos) => indexMap.GetTile(new Vector3Int(pos.x, pos.y, 0)) == indexTile;
}
