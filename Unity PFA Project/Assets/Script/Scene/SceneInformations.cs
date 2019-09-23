using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInformations : MonoBehaviour
{
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
    GameObject player;
    public AudioClip theme;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        directionalLight = GameObject.Find("Directional Light");
    }

    // Start is called before the first frame update
    void Start()
    {
        /*if(fixedCamera)
        {
            Camera.main.transform.position = CameraSpot.position;
            Camera.main.GetComponent<CameraFollow>().isFollowing = false;
        }   
        Camera.main.GetComponent<CameraFollow>().YOffset = YOffset; */
        //player.GetComponent<MovementsPlayer>().canRun = canRun;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
