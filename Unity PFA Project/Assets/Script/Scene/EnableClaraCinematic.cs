using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableClaraCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnableCinematic(1));
    }

    void InstantiateClaraCinematic()
    {
        if (!GameObject.FindObjectOfType<EventsCheck>().GetComponent<Interactions>().PnjMet.Contains("Clara"))
        {
            Vector3 posClaraToInstantiate = new Vector3(13.59f, -1.826f, 0.816f);
            Vector3 posKD_IWToInstantiate = new Vector3(9.25f, -2.51f, 0);
            GameObject roomToInstantiate = null;
            GameObject prefabClaraToInstantiate = Resources.Load("GameObject/Clara") as GameObject;
            GameObject prefabKD_IWToInstantiate = Resources.Load("GameObject/KD_InvisibleWall") as GameObject;
            foreach (SceneInformations room in FindObjectsOfType<SceneInformations>())
            {
                if (room.gameObject.name == "KennethBureau")
                {
                    roomToInstantiate = room.gameObject;
                }
            }

            GameObject Clara = Instantiate(prefabClaraToInstantiate, roomToInstantiate.transform, false);
            Clara.transform.localPosition = posClaraToInstantiate;
            Clara.name = prefabClaraToInstantiate.name;
            GameObject KD_IW = Instantiate(prefabKD_IWToInstantiate, roomToInstantiate.transform, false);
            KD_IW.transform.localPosition = posKD_IWToInstantiate;
            KD_IW.name = prefabKD_IWToInstantiate.name;


            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[0].objectToMove = Clara.gameObject.transform.GetChild(0).gameObject;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[1].objectToMove = Clara;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[2].objectToMove = Clara;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[2].direction = 0.05f;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[2].time = 1;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[3].objectToMove = Clara;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[5].objectToMove = Clara;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[6].objectToMove = Clara;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[6].direction = 0.05f;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[6].time = 3.5f;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[7].objectToMove = Clara;
            KD_IW.GetComponent<Clara_Cinematic>().annexInformation[8].objectToMove = Clara;


        }
    }

    IEnumerator EnableCinematic(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        InstantiateClaraCinematic();
    }
}
