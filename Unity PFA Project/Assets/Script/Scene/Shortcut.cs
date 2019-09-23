using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        fadePanel =  GameObject.Find("FadePanel");
        player = GameObject.FindGameObjectWithTag("Player");
        directionalLight = GameObject.Find("Directional Light");
    }

    // Update is called once per frame
    /*void Update()// Initial
    {
        if(canMove)
        {
            if(Input.GetButtonDown("Interaction") && !player.GetComponent<Interactions>().isInDialog && !player.GetComponent<Interactions>().isInCinematic)
            {
                fadePanel.GetComponent<Animator>().SetTrigger("FadeIn");
                GameObject.FindObjectOfType<Saver>().lieuFM = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().zoneIndex;
                StartCoroutine("Respawn");
            }
        }
    }*/

    public void Teleport()
    {
        fadePanel.GetComponent<Animator>().SetTrigger("FadeIn");
        GameObject.FindObjectOfType<Saver>().lieuFM = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().zoneIndex;
        StartCoroutine("Respawn");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        canMove = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        canMove = false;
        canTeleport = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator Respawn()
    {
        Camera.main.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.4f);
        if(linkedWith.transform.parent.parent.GetComponent<SceneInformations>().theme != null && linkedWith.transform.parent.parent.GetComponent<SceneInformations>().theme != GameObject.Find("AudioManager").GetComponent<AudioSource>().clip)
        { 
            GameObject.Find("AudioManager").GetComponent<AudioSource>().clip = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().theme;
            GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
        }
        linkedWith.transform.parent.parent.gameObject.SetActive(true);
        player.transform.position = linkedWith.transform.position;
        player.GetComponent<MovementsPlayer>().canRun = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().canRun;
        if(directionalLight.GetComponent<DayNightLight>().time == DayNightLight.timeEnum.Day)
        {
            directionalLight.GetComponent<Light>().intensity = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().dayLightValue;
        }
        else directionalLight.GetComponent<Light>().intensity = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().nightLightValue;
        if(!internTeleport)
        {
            transform.parent.parent.gameObject.SetActive(false);
        }
        else 
        {
            Saver saver = GameObject.FindObjectOfType<Saver>();
            saver.lieuFM = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().zoneIndex;
            /*JsonSave save = SaveGameManager.GetCurrentSave();
            save.lieu = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().zoneIndex;
            SaveGameManager.Save(); */
        }
        if(!linkedWith.transform.parent.parent.GetComponent<SceneInformations>().fixedCamera)
        {
            Camera.main.GetComponent<CameraFollow>().isFollowing = true;
            Camera.main.transform.position = new Vector3(player.transform.position.x + distanceOfTheCamera, player.transform.position.y + linkedWith.transform.parent.parent.GetComponent<SceneInformations>().YOffset, player.transform.position.z - linkedWith.transform.parent.parent.GetComponent<SceneInformations>().distanceBetweenPlayerAndCamera);
            Camera.main.GetComponent<CameraFollow>().YOffset = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().YOffset;
            Camera.main.GetComponent<CameraFollow>().barrier = "none";
            Camera.main.GetComponent<CameraFollow>().collision = false;
            Camera.main.GetComponent<BoxCollider2D>().enabled = true;
        }
        else 
        {
            Camera.main.transform.position = linkedWith.transform.parent.parent.GetComponent<SceneInformations>().CameraSpot.position;
            Camera.main.GetComponent<CameraFollow>().isFollowing = false;
        }
    }
}