using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomData 
{
    public enum Forward { Left , Right , Top , Down }//����ָ�� /����
    public enum RoomType { Normal , Boss , Bonus , Shop , Start , End} //��������

    //����
    public struct Dir {
        public bool Left;
        public bool Right;
        public bool Top;
        public bool Down;
    }
}
