using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    Image image;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        time = Time.realtimeSinceStartup;
        //FadeIn();
    }

    void FadeIn()
    {
        while(image.color.a < 255)
        {
            float opacity = Mathf.Lerp(0, 255, (Time.realtimeSinceStartup - time) * 0.0015f);
            //Debug.Log(opacity);
            Vector4 vec = new Vector4(255,255,255,opacity);
            image.color = vec;
        }
        FadeOut();
    }

    public void End()
    {
        Destroy(transform.parent.gameObject);
    }

    void FadeOut()
    {
        /*if(image.color.a > 0)
        {
            float opacity = Mathf.Lerp(255, 0, Time.deltaTime * 0.001f);
            Debug.Log(opacity);
            Vector4 vec = new Vector4(255,255,255,opacity);
            image.color = vec;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        /*float opacity = Mathf.Lerp(0, 255, (Time.realtimeSinceStartup - time) * 0.0015f);
        Debug.Log(opacity);
        Vector4 vec = new Vector4(225,255,255,opacity);
        image.color = vec;*/
    }
}
