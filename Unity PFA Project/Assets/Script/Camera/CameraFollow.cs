using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    public float YOffset;
    public float XOffset;
    public bool collision = false;
    Collider2D col;
    public string barrier;
    public float smoothSpeed = 0.125f;
    public bool isFollowing;
    public GameObject actualRoom;
    Vector3 plToBorderLeft;
    Vector3 plToBorderRight;
    public Vector3 leftBorder;
    public Vector3 rightBorder;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InitRoomLimit();
        InitCam();

        barrier = "none";
        //StartCoroutine("StartTimer");

    }

    public void InitRoomLimit()
    {
        if (actualRoom != null)
        {
            leftBorder = actualRoom.transform.Find("Structure").Find("BarrierLeft").position;
            rightBorder = actualRoom.transform.Find("Structure").Find("BarrierRight").position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnableFollow();
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(plToBorderLeft, Vector3.one);
    }*/
    
    void EnableFollow()
    {
        plToBorderRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, -Camera.main.transform.position.z));
        plToBorderLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, -Camera.main.transform.position.z));

        
        if(Vector3.Distance(new Vector3(Camera.main.transform.position.x, player.transform.position.y + YOffset, Camera.main.transform.position.z), Camera.main.transform.position) > 0.2f)
        {
            if(actualRoom.GetComponent<RoomInformations>() && !actualRoom.GetComponent<RoomInformations>().staticCamera)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(Camera.main.transform.position.x, player.transform.position.y + YOffset, Camera.main.transform.position.z), smoothSpeed);
            }
            if (actualRoom.GetComponent<SceneInformations>() && !actualRoom.GetComponent<SceneInformations>().fixedCamera)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(Camera.main.transform.position.x, player.transform.position.y + YOffset, Camera.main.transform.position.z), smoothSpeed);
            }
        }
        if (plToBorderLeft.x < leftBorder.x)
        {
            if (player.transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -Camera.main.transform.position.z)).x)
            {
                FollowPlayer();
            }
        }
        else
        if (plToBorderRight.x > rightBorder.x)
        {
            if (player.transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -Camera.main.transform.position.z)).x)
            {
                FollowPlayer();
            }
        }
        else
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        if(isFollowing)
        {
            Vector3 temp = transform.position;
            temp.x = player.transform.position.x;
            temp.x += XOffset;
            temp.y = player.transform.position.y;
            temp.y += YOffset;

            Vector3 tempSmoothed = Vector3.Lerp(transform.position, temp, smoothSpeed);
            transform.position = tempSmoothed;
        }
    }

    void InitCam()
    {
        if (actualRoom.name == "KennethBureau")
        {

            if (actualRoom.GetComponent<SceneInformations>())
            {
                YOffset = actualRoom.GetComponent<SceneInformations>().YOffset;
                transform.position = new Vector3(player.transform.position.x + 5, player.transform.position.y + YOffset, -actualRoom.GetComponent<SceneInformations>().distanceBetweenPlayerAndCamera);
            }
            else if (actualRoom.GetComponent<RoomInformations>())
            {
                YOffset = actualRoom.GetComponent<RoomInformations>().YOffset;
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y + YOffset, -actualRoom.GetComponent<RoomInformations>().distBtwPlAndCam);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x + 5, player.transform.position.y + YOffset, -8.5f);
            }
        }
        else
        {
            if (actualRoom.GetComponent<SceneInformations>())
            {
                //this.transform.position = actualRoom.GetComponent<SceneInformations>().CameraSpot.position;
                YOffset = actualRoom.GetComponent<SceneInformations>().YOffset;
                actualRoom.GetComponent<SceneInformations>().PlaceCamera();
                //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + YOffset, -actualRoom.GetComponent<SceneInformations>().distanceBetweenPlayerAndCamera);
            }
            else if (actualRoom.GetComponent<RoomInformations>())
            {
                //this.transform.position = actualRoom.GetComponent<RoomInformations>().CameraSpot.position;
                YOffset = actualRoom.GetComponent<RoomInformations>().YOffset;
                actualRoom.GetComponent<RoomInformations>().PlaceCamera();

                //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + YOffset, -actualRoom.GetComponent<RoomInformations>().distBtwPlAndCam);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y + YOffset, -8.5f);
            }
        }

    }
}
