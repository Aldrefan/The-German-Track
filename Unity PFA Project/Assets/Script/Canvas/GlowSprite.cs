using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowSprite : MonoBehaviour
{
    bool glow;
    public Material material;
    float glowPosition = 0;
    float glowWidth = 0;
    bool grow;
    
    void Start()
    {
        //material = GetComponent<Image>().material;
    }

    void OnEnable()
    {
        StartCoroutine(DelayBetweenGlow());
        glowPosition = 0;
        glowWidth = 0;
    }

    void Update()
    {
        if(glow)
        {
            if(glowPosition < 1)
            {
                //glowPosition = Mathf.Lerp(glowPosition,1, Time.deltaTime);
                if(grow)
                {
                    if(glowWidth <= 0.25f)
                    {
                        glowWidth += 0.005f;
                    }
                    else grow = false;
                }
                else glowWidth -= 0.005f;

                glowPosition += 0.03f;
                material.SetFloat("_ShineLocation", glowPosition);
                material.SetFloat("_ShineWidth", glowWidth);
            }
            else 
            {
                glow = false;
                glowPosition = 0;
                glowWidth = 0;
                material.SetFloat("_ShineWidth", glowWidth);
            }
        }
    }

    void Glow()
    {
        glow = true;
        grow = true;
    }

    IEnumerator DelayBetweenGlow()
    {
        yield return new WaitForSeconds(3);
        Glow();
        StartCoroutine(DelayBetweenGlow());
    }
}
