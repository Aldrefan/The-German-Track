using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomInformations : MonoBehaviour
{
    public string roomName;
    public int roomIndex;

    public float YOffset;
    public float distBtwPlAndCam = 8.5f;
    public bool staticCamera = false;

    public AudioClip roomTheme;

    Transform CameraSpot;
    GameObject player;
    GameObject fadePanel;

    public bool canMove;
    public bool canRun;
    

    public List<DoorShortcut> DoorList = new List<DoorShortcut>();

    [System.Serializable]
    public class DoorShortcut
    {
        public GameObject AccessDoor;
        public string destinationName;
        public GameObject FinalDest;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadePanel = GameObject.Find("Necessary_Floating_Canvas").transform.GetChild(0).gameObject;
        fadePanel.GetComponent<Animator>().SetTrigger("StartFade");

        player = GameObject.FindObjectOfType<EventsCheck>().gameObject;
        CameraSpot = this.transform.Find("Structure").Find("CameraSpot");

        PlaceCamera();
        GetDoorFinalDest();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanRun();
    }

    public void Teleport(string DoorName)
    {
        fadePanel.GetComponent<Animator>().SetTrigger("FadeIn");
        RoomInformations nextRoom = ReturnRoomDest(DoorName);


        StartCoroutine(Respawn(ReturnDoorDest(DoorName).transform, nextRoom));
        nextRoom.PlaceCamera();
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
        if (staticCamera)
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
        GameObject.Find("ZoneNameHolder").GetComponent<Text>().text = LanguageManager.Instance.GetDialog(roomName);
    }

    void GetDoorFinalDest()
    {
        foreach (DoorShortcut door in DoorList)
        {
            if (door.FinalDest == null)
            {
                if (door.AccessDoor != null && door.destinationName != null)
                {
                    GameObject newDest = GameObject.Find(door.destinationName);
                    if (newDest.tag == "Shortcut")
                    {
                        door.FinalDest = newDest;
                    }
                }
            }
        }
    }

    RoomInformations ReturnRoomDest(string doorRoomName)
    {
        foreach (DoorShortcut door in DoorList)
        {
            if (doorRoomName == door.AccessDoor.name && door.FinalDest != null)
            {
                return door.FinalDest.transform.parent.parent.GetComponent<RoomInformations>();
            }
            return null;
        }
        return null;
    }

    GameObject ReturnDoorDest(string doorRoomName)
    {
        foreach (DoorShortcut door in DoorList)
        {
            if (doorRoomName == door.AccessDoor.name && door.FinalDest != null)
            {
                return door.FinalDest;
            }
            return null;
        }
        return null;
    }

    IEnumerator Respawn(Transform FinalDestTrans, RoomInformations FinalDestRoom)
    {
        Camera.main.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.4f);
        if (FinalDestRoom.roomTheme != null && FinalDestRoom.roomTheme != GameObject.Find("AudioManager").GetComponent<AudioSource>().clip)
        {
            GameObject.Find("AudioManager").GetComponent<AudioSource>().clip = FinalDestRoom.roomTheme;
            GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
        }
        FinalDestRoom.gameObject.SetActive(true);
        player.transform.position = FinalDestTrans.position;
        player.GetComponent<MovementsPlayer>().canRun = FinalDestRoom.canRun;

        ////if (!internTeleport)
        ////{
        ////    transform.parent.parent.gameObject.SetActive(false);
        ////    linkedWith.transform.parent.parent.GetComponent<SceneInformations>().ShowZoneName();
        ////}
        if (!FinalDestRoom.staticCamera)
        {
            Camera.main.GetComponent<CameraFollow>().isFollowing = true;
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + FinalDestRoom.YOffset, player.transform.position.z - FinalDestRoom.distBtwPlAndCam);
            Camera.main.GetComponent<CameraFollow>().YOffset = FinalDestRoom.YOffset;
            Camera.main.GetComponent<CameraFollow>().barrier = "none";
            Camera.main.GetComponent<CameraFollow>().collision = false;
            Camera.main.GetComponent<CameraFollow>().actualRoom = FinalDestRoom.gameObject;
            Camera.main.GetComponent<CameraFollow>().InitRoomLimit();
            Camera.main.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            Camera.main.transform.position = FinalDestRoom.CameraSpot.position;
            Camera.main.GetComponent<CameraFollow>().actualRoom = FinalDestRoom.gameObject;
            Camera.main.GetComponent<CameraFollow>().InitRoomLimit();
            Camera.main.GetComponent<CameraFollow>().isFollowing = false;
        }
    }
}


 
