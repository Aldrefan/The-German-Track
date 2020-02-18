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
        if (actualRoom.GetComponent<SceneInformations>())
        {
            transform.position = new Vector3(player.transform.position.x+5, player.transform.position.y + YOffset, -actualRoom.GetComponent<SceneInformations>().distanceBetweenPlayerAndCamera);
        }
        else if (actualRoom.GetComponent<RoomInformations>())
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + YOffset, actualRoom.GetComponent<RoomInformations>().distBtwPlAndCam);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + YOffset, -8.5f);
        }
        barrier = "none";
        //StartCoroutine("StartTimer");
        InitRoomLimit();
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
    void Update()
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
}
