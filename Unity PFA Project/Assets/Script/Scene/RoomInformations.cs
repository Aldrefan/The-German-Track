﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomInformations : MonoBehaviour
{
    public string roomName;
    public int roomIndex;

    public float YOffset;
    public float distBtwPlAndCam = 8.5f;
    public bool staticCamera = false;

    public AudioClip roomTheme;

    Transform CameraSpot;
    GameObject player;
    GameObject fadePanel;

    public bool canMove;
    public bool canRun;

    float distanceOfTheCamera;
    bool inRespawn;

    public List<DoorShortcut> DoorList = new List<DoorShortcut>();

    [System.Serializable]
    public class DoorShortcut
    {
        public GameObject AccessDoor;
        public string destinationName;
        public GameObject FinalDest;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadePanel = GameObject.Find("Necessary_Floating_Canvas").transform.GetChild(0).gameObject;
        fadePanel.GetComponent<Animator>().SetTrigger("StartFade");

        player = GameObject.FindObjectOfType<EventsCheck>().gameObject;
        CameraSpot = this.transform.Find("Structure").Find("CameraSpot");

        PlaceCamera();
        GetDoorFinalDest();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanRun();
    }

    public void Teleport(string DoorName)
    {
        RoomInformations nextRoom = ReturnRoomDest(DoorName);
        distanceOfTheCamera = CalculateDistBtwCamToDoor(DoorName);

        if (!inRespawn)
        {

        StartCoroutine(Respawn(ReturnDoorDest(DoorName).transform, nextRoom));
        }
        //
    }

    void CheckCanRun()
    {

        if (player != null && GameObject.FindObjectOfType<CameraFollow>().actualRoom.name == this.gameObject.name)
        {
            if (player.GetComponent<MovementsPlayer>().canRun != canRun)
            {
                player.GetComponent<MovementsPlayer>().canRun = canRun;
            }
        }
    }

    public void PlaceCamera()
    {
        if (staticCamera)
        {
            Camera.main.transform.position = CameraSpot.position;
            Camera.main.GetComponent<CameraFollow>().isFollowing = false;
        }
        else
        {
            Camera.main.GetComponent<CameraFollow>().isFollowing = true;
            Camera.main.transform.position = new Vector3( player.transform.position.x, 0, player.transform.position.z - distBtwPlAndCam);
        }
    }

    public void ShowZoneName()
    {
        GameObject.Find("ZoneNameHolder").GetComponent<Animator>().Play("New State", 0);
        GameObject.Find("ZoneNameHolder").GetComponent<Animator>().SetTrigger("Fade");
        GameObject.Find("ZoneNameHolder").GetComponent<Text>().text = LanguageManager.Instance.GetDialog(roomName);
    }

    void GetDoorFinalDest()
    {
        foreach (DoorShortcut door in DoorList)
        {
            if (door.FinalDest == null)
            {
                if (door.AccessDoor != null && door.destinationName != null)
                {
                    GameObject newDest = GameObject.Find(door.destinationName);
                    if (newDest.tag == "Shortcut")
                    {
                        door.FinalDest = newDest;
                    }
                }
            }
        }
    }


    IEnumerator Respawn(Transform FinalDestTrans, RoomInformations FinalDestRoom)
    {
        if (!inRespawn)
        {
            inRespawn = true;
            fadePanel.GetComponent<Animator>().SetBool("FadeIn",true);
            Camera.main.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(0.3f);
            fadePanel.GetComponent<Animator>().SetBool("FadeIn", false);
            if (FinalDestRoom.roomTheme != null && FinalDestRoom.roomTheme != GameObject.Find("AudioManager").GetComponent<AudioSource>().clip)
            {
                GameObject.Find("AudioManager").GetComponent<AudioSource>().clip = FinalDestRoom.roomTheme;
                GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
            }
            FinalDestRoom.gameObject.SetActive(true);
            player.transform.position = CalculateDestPos(FinalDestTrans);
            player.GetComponent<MovementsPlayer>().canRun = FinalDestRoom.canRun;

            //if (!internTeleport)
            //{
            //    transform.parent.parent.gameObject.SetActive(false);
            //    linkedWith.transform.parent.parent.GetComponent<SceneInformations>().ShowZoneName();
            //}
            //FinalDestRoom.PlaceCamera();
            if (!FinalDestRoom.staticCamera)
            {
                Camera.main.GetComponent<CameraFollow>().actualRoom = FinalDestRoom.gameObject;
                Camera.main.GetComponent<CameraFollow>().InitRoomLimit();
                Camera.main.GetComponent<BoxCollider2D>().enabled = true;
                Camera.main.transform.position = new Vector3(FinalDestRoom.transform.position.x + distanceOfTheCamera, player.transform.position.y + FinalDestRoom.YOffset, player.transform.position.z - FinalDestRoom.distBtwPlAndCam);
                Camera.main.GetComponent<CameraFollow>().YOffset = FinalDestRoom.YOffset;
                Camera.main.GetComponent<CameraFollow>().barrier = "none";
                Camera.main.GetComponent<CameraFollow>().collision = false;
                Camera.main.GetComponent<CameraFollow>().isFollowing = true;
            }
            else
            {
                Camera.main.GetComponent<CameraFollow>().actualRoom = FinalDestRoom.gameObject;
                Camera.main.GetComponent<CameraFollow>().InitRoomLimit();
                Camera.main.transform.position = FinalDestRoom.CameraSpot.position;
                Camera.main.GetComponent<CameraFollow>().isFollowing = false;
            }
            inRespawn = false;
        }
    }

    float CalculateDistBtwCamToDoor(string doorName)
    {

        Vector3 doorPos = ReturnAccessDoor(doorName).transform.position;
        Vector3 camPos = new Vector3(Camera.main.transform.position.x, doorPos.y, doorPos.z);
        float dist = Vector3.Distance(doorPos, camPos);

        if (camPos.x > doorPos.x)
        {
            dist = -dist;
        }

        return dist;
            
    }

    RoomInformations ReturnRoomDest(string doorRoomName)
    {
        foreach (DoorShortcut door in DoorList)
        {
            if (doorRoomName == door.AccessDoor.name && door.FinalDest != null)
            {
                return door.FinalDest.transform.parent.parent.GetComponent<RoomInformations>();
            }
            return null;
        }
        return null;
    }

    GameObject ReturnDoorDest(string doorRoomName)
    {
        foreach (DoorShortcut door in DoorList)
        {
            if (doorRoomName == door.AccessDoor.name && door.FinalDest != null)
            {
                return door.FinalDest;
            }
            return null;
        }
        return null;
    }

    GameObject ReturnAccessDoor(string doorRoomName)
    {
        foreach (DoorShortcut door in DoorList)
        {
            if (doorRoomName == door.AccessDoor.name && door.FinalDest != null)
            {
                return door.AccessDoor;
            }
            return null;
        }
        return null;
    }

    Vector3 CalculateDestPos(Transform finalDestTrans)
    {
        float distBtwDoorAndPl = Vector3.Distance(finalDestTrans.position,new Vector3(finalDestTrans.position.x, player.transform.position.y, player.transform.position.z));
        Vector3 finalPos = new Vector3(finalDestTrans.position.x, finalDestTrans.position.y - distBtwDoorAndPl, finalDestTrans.position.z);
        return finalPos;
    }

}


 
