using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street_PigeonFly_Particles : MonoBehaviour
{
    public Vector3 decalage;
    void OnEnable()
    {
        StartCoroutine("Wait");
    }

    void PigeonPop()
    {
        Vector3 cameraPosition = new Vector3(Camera.main.transform.position.x + decalage.x, Camera.main.transform.position.y + decalage.y, Camera.main.transform.position.z + decalage.z);
        transform.position = cameraPosition;
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        PigeonPop();
    }
}
