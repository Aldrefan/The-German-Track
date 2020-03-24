using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInformations : MonoBehaviour
{
    public string zoneName;
    public int zoneIndex;
    GameObject directionalLight;
    public float dayLightValue;
    public float nightLightValue;
    bool canMove;
    public float distanceBetweenPlayerAndCamera = 8.5f;
    public bool fixedCamera = false;
    public Transform CameraSpot;
    public bool canRun;
    public float YOffset;
    public GameObject player;
    public AudioClip theme;

    void Awake()
    {
        
        if (this.transform.Find("CameraSpot"))
        {
            CameraSpot = this.transform.Find("CameraSpot");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<EventsCheck>().gameObject;
        directionalLight = GameObject.Find("Directional Light");
        //Camera.main.GetComponent<CameraFollow>().YOffset = YOffset;
        //player.GetComponent<MovementsPlayer>().canRun = canRun;

        if (fixedCamera)
        {
            distanceBetweenPlayerAndCamera = -CameraSpot.transform.position.z;
        }

        if (Camera.main.GetComponent<CameraFollow>().actualRoom == this.gameObject)
        {
            PlaceCamera();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanRun();
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
        Camera.main.transform.position = new Vector3(CameraSpot.position.x,CameraSpot.position.y,-distanceBetweenPlayerAndCamera);
        if (fixedCamera)
        {
            Camera.main.GetComponent<CameraFollow>().isFollowing = false;
        }
        else
        {
            Camera.main.GetComponent<CameraFollow>().isFollowing = true;
        }
    }

    public void ShowZoneName()
    {
        CanvasManager.CManager.GetCanvas("ZoneNameParent").transform.GetChild(1).GetComponent<Text>().text = LanguageManager.Instance.GetDialog(zoneName);
        //CanvasManager.CManager.GetCanvas("ZoneNameParent").GetComponent<Animator>().SetTrigger("Show");
        CanvasManager.CManager.GetCanvas("ZoneNameParent").GetComponent<Animator>().Play("Show", 0);
        /*GameObject.Find("ZoneNameHolder").GetComponent<Animator>().Play("New State", 0);
        GameObject.Find("ZoneNameHolder").GetComponent<Animator>().SetTrigger("Fade");*/
    }
}
