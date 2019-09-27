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

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.transform.tag == "BarrierLeft")
    //    {
    //        collision = true;
    //        barrier = "left";
    //    }

    //    if (col.transform.tag == "BarrierRight")
    //    {
    //        collision = true;
    //        barrier = "right";
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x + 5, player.transform.position.y + YOffset, transform.position.z);
        barrier = "none";
        //StartCoroutine("StartTimer");
        InitRoomLimit();
    }

    public void InitRoomLimit()
    {
        Debug.Log(actualRoom);
        leftBorder = actualRoom.transform.Find("Structure").Find("BarrierLeft").position;
        rightBorder = actualRoom.transform.Find("Structure").Find("BarrierRight").position;
    }

    //IEnumerator StartTimer()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    GetComponent<BoxCollider2D>().enabled = true;
    //}

    // Update is called once per frame
    void Update()
    {
        plToBorderRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, -Camera.main.transform.position.z));
        plToBorderLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, -Camera.main.transform.position.z));

        if (plToBorderLeft.x < leftBorder.x )
        {
            Debug.Log("1");
            if (player.transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -Camera.main.transform.position.z)).x)
            {
                Debug.Log("2");

                FollowPlayer();

            }
        }
        else
        if (plToBorderRight.x > rightBorder.x)
        {
            Debug.Log("3");

            if (player.transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -Camera.main.transform.position.z)).x)
            {
                Debug.Log("4");

                FollowPlayer();

            }
        }
        else
        {
            Debug.Log("5");

            FollowPlayer();
        }
        //if (collision)
        //{
        //    if (barrier=="left" && player.transform.position.x > transform.position.x)
        //    {
        //        collision = false;
        //        barrier = "none";
        //    }

        //    if (barrier=="right" && player.transform.position.x < transform.position.x)
        //    {
        //        collision = false;
        //        barrier = "none";
        //    }
        //}
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(plToBorderLeft, Vector3.one);


    }

    void FollowPlayer()
    {
        Vector3 temp = transform.position;
        temp.x = player.transform.position.x;
        temp.x += XOffset;
        temp.y = player.transform.position.y;
        temp.y += YOffset;

        Vector3 tempSmoothed = Vector3.Lerp(transform.position, temp, smoothSpeed);
        transform.position = tempSmoothed;
    }

    // LateUpdate is called after all Update functions have been called.
    //void LateUpdate()
    //{
    //    if (!collision && barrier=="none" && isFollowing)
    //    {

    //    }
    //}
}
