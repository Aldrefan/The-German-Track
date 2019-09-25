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
    Vector3 plToBorder;

     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "BarrierLeft")
        {
            collision = true;
            barrier = "left";
        }

        if (col.transform.tag == "BarrierRight")
        {
            collision = true;
            barrier = "right";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x + 5, player.transform.position.y + YOffset, transform.position.z);
        barrier = "none";
        //StartCoroutine("StartTimer");
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        plToBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, -Camera.main.transform.position.z));
        Debug.Log(plToBorder);
        if (collision)
        {
            if (barrier=="left" && player.transform.position.x > transform.position.x)
            {
                collision = false;
                barrier = "none";
            }

            if (barrier=="right" && player.transform.position.x < transform.position.x)
            {
                collision = false;
                barrier = "none";
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(plToBorder, Vector3.one);

    }

    // LateUpdate is called after all Update functions have been called.
    void LateUpdate()
    {
        if (!collision && barrier=="none" && isFollowing)
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
