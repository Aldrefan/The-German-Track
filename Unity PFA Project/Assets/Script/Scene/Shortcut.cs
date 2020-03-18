using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Shortcut : MonoBehaviour
{
    GameObject player;
    public bool canTeleport;
    public GameObject linkedWith;
    public float distanceOfTheCamera;
    GameObject fadePanel;
    bool canMove;
    public bool internTeleport;
    GameObject directionalLight;
    [SerializeField]
    private AudioClip teleportSound;
    bool inRespawn;

    // Start is called before the first frame update
    void Start()
    {
        fadePanel =  GameObject.Find("Necessary_Floating_Canvas").transform.Find("FadePanel").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        directionalLight = GameObject.Find("Directional Light");
        StartFade();
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()// Initial
    {
        //if(canMove)
        //{
        //    if(Input.GetButtonDown("Interaction") && !player.GetComponent<Interactions>().isInDialog && !player.GetComponent<Interactions>().isInCinematic)
        //    {
        //        fadePanel.GetComponent<Animator>().SetTrigger("FadeIn");
        //        GameObject.FindObjectOfType<Saver>().lieuFM = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().zoneIndex;
        //        StartCoroutine("Respawn");
        //    }
        //}

    }

    void StartFade()
    {
        fadePanel.GetComponent<Animator>().SetTrigger("StartFade");
    }

    public void Teleport()
    {

        if (!GameObject.FindObjectOfType<MovementsPlayer>().inRespawn)
        {//GameObject.FindObjectOfType<Saver>().lieuFM = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().zoneIndex;
            if(teleportSound != null)
            {
                /*AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = teleportSound;
                audioSource.gameObject.AddComponent<LifeTimer>();
                audioSource.GetComponent<LifeTimer>().time = audioSource.clip.length;
                audioSource.transform.position = Camera.main.transform.position;
                audioSource.Play();*/
                AudioManager.Instance.PlaySound(teleportSound);
                //AudioSource.PlayClipAtPoint(teleportSound, Camera.main.transform.position /*new Vector3(Camera.main.transform.position.x, )Camera.main.transform.position.y, Camera.main.transform.position.z + 10)*/);
                //GameObject.Find("One shot audio").GetComponent<AudioSource>().outputAudioMixerGroup = Resources.Load("Assets/Resources/SoundMixer/FXMixer.mixer") as AudioMixerGroup;
                //Debug.Break();
            }
            StartCoroutine("Respawn");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            canMove = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        canMove = false;
        canTeleport = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator Respawn()
    {
        if (!GameObject.FindObjectOfType<MovementsPlayer>().inRespawn)
        {
            GameObject.FindObjectOfType<MovementsPlayer>().inRespawn = true;
            Camera.main.GetComponent<CameraFollow>().isFollowing = false;
            Camera.main.GetComponent<BoxCollider2D>().enabled = false;

            if (!fadePanel.GetComponent<Animator>().GetBool("FadeIn"))
            {
                fadePanel.GetComponent<Animator>().SetBool("FadeIn", true);
            }
            if (linkedWith.transform.parent.parent.GetComponent<SceneInformations>().theme != null && linkedWith.transform.parent.parent.GetComponent<SceneInformations>().theme != GameObject.Find("AudioManager").GetComponent<AudioSource>().clip)
            {
                GameObject.Find("AudioManager").GetComponent<AudioSource>().clip = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().theme;
                GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(0.5f);


            linkedWith.transform.parent.parent.gameObject.SetActive(true);
            player.transform.position = CalculateDestPos(linkedWith.transform);
            player.GetComponent<MovementsPlayer>().canRun = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().canRun;

            if (directionalLight.GetComponent<DayNightLight>().time == DayNightLight.timeEnum.Day)
            {
                directionalLight.GetComponent<Light>().intensity = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().dayLightValue;
            }
            else directionalLight.GetComponent<Light>().intensity = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().nightLightValue;

            if (!internTeleport)
            {
                transform.parent.parent.gameObject.SetActive(false);
                linkedWith.transform.parent.parent.GetComponent<SceneInformations>().ShowZoneName();
            }
            //else 
            //{
            //Saver saver = GameObject.FindObjectOfType<Saver>();
            //saver.lieuFM = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().zoneIndex;
            /*JsonSave save = SaveGameManager.GetCurrentSave();
            save.lieu = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().zoneIndex;
            SaveGameManager.Save();*/
            //}





            if (!linkedWith.transform.parent.parent.GetComponent<SceneInformations>().fixedCamera)
            {
                Camera.main.GetComponent<CameraFollow>().actualRoom = linkedWith.transform.parent.parent.gameObject;
                Camera.main.GetComponent<CameraFollow>().InitRoomLimit();
                Camera.main.GetComponent<BoxCollider2D>().enabled = true;
                //Camera.main.transform.position = linkedWith.transform.parent.parent.gameObject.transform.position;
                Camera.main.transform.position = new Vector3(linkedWith.transform.parent.parent.GetComponent<SceneInformations>().CameraSpot.position.x, linkedWith.transform.position.y + linkedWith.transform.parent.parent.GetComponent<SceneInformations>().YOffset, player.transform.position.z - linkedWith.transform.parent.parent.GetComponent<SceneInformations>().distanceBetweenPlayerAndCamera);
                Camera.main.GetComponent<CameraFollow>().YOffset = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().YOffset;
                Camera.main.GetComponent<CameraFollow>().barrier = "none";
                Camera.main.GetComponent<CameraFollow>().collision = false;
                Camera.main.GetComponent<CameraFollow>().isFollowing = true;
            }
            else
            {
                Camera.main.GetComponent<CameraFollow>().isFollowing = false;
                Camera.main.transform.position = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().CameraSpot.position;
                Camera.main.GetComponent<CameraFollow>().actualRoom = linkedWith.transform.parent.parent.gameObject;
                Camera.main.GetComponent<CameraFollow>().InitRoomLimit();
            }
            //linkedWith.transform.parent.parent.GetComponent<SceneInformations>().PlaceCamera();

            if (fadePanel.GetComponent<Animator>().GetBool("FadeIn"))
            {
                fadePanel.GetComponent<Animator>().SetBool("FadeIn", false);
            }
            GameObject.FindObjectOfType<MovementsPlayer>().inRespawn = false;

        }
    }

    float CalculateDistBtwCamToDoor()
    {

        Vector3 doorPos = this.transform.position;
        Vector3 camPos = new Vector3(Camera.main.transform.position.x, doorPos.y, doorPos.z);
        float dist = Vector3.Distance(doorPos, camPos);

        if (camPos.x > doorPos.x)
        {
            dist = -dist;
        }

        return dist;

    }

    Vector3 CalculateDestPos(Transform finalDestTrans)
    {
        float YDistBtwDoorAndDoor = Vector3.Distance(finalDestTrans.position, new Vector3(finalDestTrans.position.x, this.transform.position.y, player.transform.position.z));


        float distBtwDoorAndPl = default;
        if (this.transform.position.y < finalDestTrans.position.y)
        {
            distBtwDoorAndPl = Vector3.Distance(finalDestTrans.position, new Vector3(finalDestTrans.position.x, player.transform.position.y + YDistBtwDoorAndDoor, player.transform.position.z));

        }else
        {
            distBtwDoorAndPl = Vector3.Distance(finalDestTrans.position, new Vector3(finalDestTrans.position.x, player.transform.position.y - YDistBtwDoorAndDoor, player.transform.position.z));

        }
        Vector3 finalPos = new Vector3(finalDestTrans.position.x, finalDestTrans.position.y - distBtwDoorAndPl, finalDestTrans.position.z);

        return finalPos;
    }

    //void LinkedDestPos()
    //{
    //            Vector3 spawnPoint = Vector3.zero;        
    //    RaycastHit2D hit = default;
    //    RaycastHit2D[] hits = Physics2D.RaycastAll(linkedWith.transform.position, Vector2.down, 10);

    //    foreach (RaycastHit2D eachHit in hits)
    //    {
    //        Debug.Log(eachHit.collider.name);
    //        if(eachHit.collider.gameObject.layer == 16)
    //        {
    //            hit = eachHit;
    //            spawnPoint = new Vector3(hit.point.x, hit.point.y + 4, 0);
    //        }
    //    }
    //}
}