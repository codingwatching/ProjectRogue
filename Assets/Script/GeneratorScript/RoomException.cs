using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomException : Exception
{
    public int ErrorCode { get; }

    public RoomException(string message) : base(message){
    }
    public RoomException(string message, Exception innerException) : base(message, innerException){
    }
    public RoomException(string message, int errorCode) : base(message){
        ErrorCode = errorCode;
    }
}
