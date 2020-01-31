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
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<EventsCheck>().gameObject;
        directionalLight = GameObject.Find("Directional Light");
        //Camera.main.GetComponent<CameraFollow>().YOffset = YOffset;
        //player.GetComponent<MovementsPlayer>().canRun = canRun;
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
        if(fixedCamera)
        {
            Camera.main.transform.position = CameraSpot.position;
            Camera.main.GetComponent<CameraFollow>().isFollowing = false;
        }
        else Camera.main.GetComponent<CameraFollow>().isFollowing = true;
    }

    public void ShowZoneName()
    {
        GameObject.Find("ZoneNameHolder").GetComponent<Animator>().Play("New State", 0);
        GameObject.Find("ZoneNameHolder").GetComponent<Animator>().SetTrigger("Fade");
        GameObject.Find("ZoneNameHolder").GetComponent<Text>().text = LanguageManager.Instance.GetDialog(zoneName);
    }
}
