using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    Camera_BoardMovements cameraBoard;
    CameraFollow cameraFollow;
    GameObject responseButton;
    public GameObject boardCanvas;
    GameObject player;
    GameObject floatingCanvas;
    public List<GameObject> objectsToDisactive;

    // Start is called before the first frame update
    void Start()
    {
        cameraBoard = GetComponent<Camera_BoardMovements>();
        cameraFollow = GetComponent<CameraFollow>();
        //responseButton = GameObject.Find("FlottingCanvas").transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        //floatingCanvas = GameObject.Find("FlottingCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBoard()
    {
        for(int i = 0; i < objectsToDisactive.Count; i++)
        {
            if(objectsToDisactive[i].GetComponent<SpriteRenderer>())
            {
                objectsToDisactive[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            else if(objectsToDisactive[i].GetComponent<MeshRenderer>())
            {
                objectsToDisactive[i].GetComponent<MeshRenderer>().enabled = false;
            }
            else if(objectsToDisactive[i].GetComponent<ParticleSystem>())
            {
                objectsToDisactive[i].SetActive(false);
            }
        }
        //player.GetComponent<MovementsPlayer>().enabled = false;// Initial
        boardCanvas.SetActive(true);
        cameraBoard.enabled = true;
        cameraBoard.GetPosition();
        cameraFollow.enabled = false;
        //responseButton.SetActive(true);// Initial
    }

    public void NotOnBoard()
    {
        for(int i = 0; i < objectsToDisactive.Count; i++)
        {
            if(objectsToDisactive[i].GetComponent<SpriteRenderer>())
            {
                objectsToDisactive[i].GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(objectsToDisactive[i].GetComponent<MeshRenderer>())
            {
                objectsToDisactive[i].GetComponent<MeshRenderer>().enabled = true;
            }
            else if(objectsToDisactive[i].GetComponent<ParticleSystem>())
            {
                objectsToDisactive[i].SetActive(true);
            }
        }
        //player.GetComponent<MovementsPlayer>().enabled = true;// Initial
        boardCanvas.SetActive(false);
        cameraBoard.enabled = false;
        cameraFollow.enabled = true;
        //responseButton.SetActive(false);
    }

    public void OnCarnet()
    {
        //GetComponent<CameraFollow>().enabled = false;
        player.GetComponent<Interactions>().dialAndBookCanvas.transform.GetChild(5).gameObject.SetActive(true);
        //floatingCanvas.transform.GetChild(3).gameObject.SetActive(true);// Inital
        player.GetComponent<Interactions>().dialAndBookCanvas.transform.GetChild(2).GetComponent<Animator>().SetBool("ClickOn", true);
        //floatingCanvas.transform.GetChild(floatingCanvas.transform.childCount - 1).GetComponent<Animator>().SetBool("ClickOn", true);// Initial
    }

    public void NotOnCarnet()
    {
        //GetComponent<CameraFollow>().enabled = true;
        player.GetComponent<Interactions>().dialAndBookCanvas.transform.GetChild(5).gameObject.SetActive(false);
        //floatingCanvas.transform.GetChild(3).gameObject.SetActive(false);// Initial
        player.GetComponent<Interactions>().dialAndBookCanvas.transform.GetChild(2).GetComponent<Animator>().SetBool("ClickOn", false);
        //floatingCanvas.transform.GetChild(floatingCanvas.transform.childCount - 1).GetComponent<Animator>().SetBool("ClickOn", false);// Initial
    }
}