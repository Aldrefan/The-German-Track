using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager RM;
    public RoomInformations[] roomList;
    void Awake()
    {
        roomList = GameObject.FindObjectsOfType<RoomInformations>();
        RM = this;
    }
}
