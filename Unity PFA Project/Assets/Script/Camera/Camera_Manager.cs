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
    //SpriteRenderer[] spriteRenderers;

    // Start is called before the first frame update
    void Start()
    {
        boardCanvas = CanvasManager.CManager.GetCanvas("Board_FIX");
        cameraBoard = GetComponent<Camera_BoardMovements>();
        cameraFollow = GetComponent<CameraFollow>();
        player = GameObject.FindGameObjectWithTag("Player");
        //responseButton = GameObject.Find("FlottingCanvas").transform.GetChild(0).gameObject;
        //floatingCanvas = GameObject.Find("FlottingCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBoard()
    {
        /*spriteRenderers = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            
        }*/
        /*for(int i = 0; i < objectsToDisactive.Count; i++)
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
        }*/
        //player.GetComponent<MovementsPlayer>().enabled = false;// Initial
        GameObject.Find("Kenneth").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Kenneth").GetComponent<BoxCollider2D>().enabled = false;
        boardCanvas.SetActive(true);
        cameraBoard.enabled = true;
        cameraBoard.GetPosition();
        cameraFollow.enabled = false;
        //responseButton.SetActive(true);// Initial
    }

    public void NotOnBoard()
    {
        /*for(int i = 0; i < objectsToDisactive.Count; i++)
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
        }*/
        //player.GetComponent<MovementsPlayer>().enabled = true;// Initial
        cameraFollow.actualRoom.SetActive(true);
        GameObject.Find("Kenneth").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Kenneth").GetComponent<BoxCollider2D>().enabled = true;
        boardCanvas.SetActive(false);
        cameraBoard.enabled = false;
        cameraFollow.enabled = true;
        //responseButton.SetActive(false);
    }

    public void EndLevel()
    {
        cameraFollow.actualRoom.SetActive(true);
        player.SetActive(true);
        boardCanvas.SetActive(false);
        cameraBoard.enabled = false;
        cameraFollow.enabled = true;
    }

    /*IEnumerator EndTimer()
    {
        yield return new WaitForSecondsRealtime(1);

    }*/

    public void OnCarnet()
    {
        //GetComponent<CameraFollow>().enabled = false;
        CanvasManager.CManager.GetCanvas("Dialogue").transform.GetChild(5).gameObject.SetActive(true);
        //floatingCanvas.transform.GetChild(3).gameObject.SetActive(true);// Inital
        CanvasManager.CManager.GetCanvas("Dialogue").transform.GetChild(2).GetComponent<Animator>().SetBool("ClickOn", true);
        //floatingCanvas.transform.GetChild(floatingCanvas.transform.childCount - 1).GetComponent<Animator>().SetBool("ClickOn", true);// Initial
    }

    public void NotOnCarnet()
    {
        //GetComponent<CameraFollow>().enabled = true;
        CanvasManager.CManager.GetCanvas("Dialogue").transform.GetChild(5).gameObject.SetActive(false);
        //floatingCanvas.transform.GetChild(3).gameObject.SetActive(false);// Initial
        CanvasManager.CManager.GetCanvas("Dialogue").transform.GetChild(2).GetComponent<Animator>().SetBool("ClickOn", false);
        //floatingCanvas.transform.GetChild(floatingCanvas.transform.childCount - 1).GetComponent<Animator>().SetBool("ClickOn", false);// Initial
    }
}