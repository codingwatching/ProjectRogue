using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomData 
{
    public enum Forward { Left , Right , Top , Down }//房间指向 /开口
    public enum RoomType { Normal , Boss , Bonus , Shop , Start , End} //房间类型

    //弃用
    public struct Dir {
        public bool Left;
        public bool Right;
        public bool Top;
        public bool Down;
    }
}
