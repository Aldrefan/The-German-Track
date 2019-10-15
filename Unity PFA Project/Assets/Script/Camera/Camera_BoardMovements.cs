using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_BoardMovements : MonoBehaviour
{
    Animator animator;
    public bool zoom;
    public Vector3 startPosition;
    public Vector2 Limits;
    Rigidbody2D rb2d;
    float speed = 100;
    public Transform boardCanvas;
    GameObject player;
    GameObject saver;

    enum State {Zoom, NotZoom}
    [SerializeField]
    State state;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        saver = GameObject.FindObjectOfType<Saver>().gameObject;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void GetPosition()
    {
        boardCanvas.transform.position = new Vector3(transform.position.x, boardCanvas.transform.position.y, boardCanvas.transform.position.z);
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        switch(state)
        {
            case State.Zoom:
            HandleUnzoomPossibility();
            Movements();
            break;

            case State.NotZoom:
            QuitBoardPossibility();
            HandleZoomPossibility();
            break;
        }
    }

    void HandleZoomPossibility()
    {
        if(Input.GetButtonDown("Interaction"))
        {
            ZoomExe();
        }
    }

    void QuitBoardPossibility()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            QuitBoardExe();
        }
    }

    void QuitBoardExe()
    {
        if(boardCanvas.transform.childCount > 1)
        {
            for(int i = 1; i < boardCanvas.transform.childCount; i++)
            {
                if(saver.GetComponent<Saver>().stickersIndexOnBoardFM.Contains(boardCanvas.transform.GetChild(i).GetComponent<Pin_System>().stickerIndex))
                {
                    for(int x = 0; x < saver.GetComponent<Saver>().stickersIndexOnBoardFM.Count; x++)
                    {
                        if(saver.GetComponent<Saver>().stickersIndexOnBoardFM[x] == boardCanvas.transform.GetChild(i).GetComponent<Pin_System>().stickerIndex)
                        {
                            saver.GetComponent<Saver>().stickersPositionOnBoardFM[x] = boardCanvas.transform.GetChild(i).localPosition;
                            break;
                        }
                    }
                }
                else 
                {
                    saver.GetComponent<Saver>().stickersIndexOnBoardFM.Add(boardCanvas.transform.GetChild(i).GetComponent<Pin_System>().stickerIndex);
                    saver.GetComponent<Saver>().stickersPositionOnBoardFM.Add(boardCanvas.transform.GetChild(i).localPosition);
                }
            }
        }
        GetComponent<CameraFollow>().actualRoom.SetActive(true);
        player.SetActive(true);
        player.GetComponent<Interactions>().CloseBoard();
        Camera.main.GetComponent<Camera_Manager>().NotOnBoard();
    }

    void ZoomExe()
    {
        animator.SetTrigger("Zoom");
        state = State.Zoom;
    }

    void HandleUnzoomPossibility()
    {
        if(Input.GetButtonDown("Cancel") || Input.GetButtonDown("Interaction"))
        {
            UnzoomExe();
            state = State.NotZoom;
        }
    }

    void UnzoomExe()
    {
        animator.SetTrigger("UnZoom");
        //zoom = false;
        rb2d.velocity = new Vector3(0, 0, 0);
        transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
    }

    void Movements()
    {
        float axisY = Input.GetAxis("Vertical");
        float axisX = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(axisX * speed, axisY * speed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, startPosition.x - Limits.x, startPosition.x + Limits.x), Mathf.Clamp(transform.position.y, startPosition.y - Limits.y, startPosition.y + Limits.y), startPosition.z);
    }
}