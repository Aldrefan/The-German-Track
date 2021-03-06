﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewStickerDisplay : MonoBehaviour
{
    public List<Sticker> stickersToNotif = new List<Sticker>();
    public Sticker sticker;
    public Text text;
    public Text tooltipText;
    public Image backgroundSticker;
    public Color backgroundColor;

    public bool display_Finished;

    Vector3 startPosition;
    Vector3 endPosition;
    float timeToLerp;

    Vector2 defaultSize;

    Vector2 startSize;
    Vector2 endSize = new Vector2(0.2f, 0.2f);

    GameObject carnetMini;
    GameObject _carnetMini => carnetMini = carnetMini ?? this.transform.parent.Find("carnet_mini").gameObject;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.localPosition;
        endPosition = _carnetMini.transform.localPosition;
        endPosition = new Vector3(0, endPosition.y, endPosition.z);
        defaultSize = text.rectTransform.sizeDelta;
    }

    private void Update()
    {
        if (stickersToNotif.Count != 0)
        {
            if(sticker != stickersToNotif[0])
            {
                sticker = stickersToNotif[0];
                Corou();
            }

        }
        if (display_Finished)
        {
            if (Vector3.Distance(this.transform.localPosition, endPosition) > 0.5f)
            {
                _carnetMini.GetComponent<SpriteRenderer>().color = Color.white;
                timeToLerp += Time.deltaTime*4;
                timeToLerp = Mathf.Clamp01(timeToLerp);
                this.transform.localPosition = Vector3.Lerp(startPosition, endPosition, timeToLerp);
                this.transform.localScale = Vector2.Lerp(startSize, endSize, timeToLerp);
            }
            else
            {
                this.GetComponent<Animator>().SetTrigger("AnimOff");
                stickersToNotif.Remove(sticker);
                display_Finished = false;
                timeToLerp = 0;
                _carnetMini.GetComponent<SpriteRenderer>().color = new Vector4(0,0,0,0);
            }
        }
    }

    public void SetInformations()
    {
        if (sticker != null && stickersToNotif.Count != 0)
        {
            this.transform.localPosition = startPosition;
            this.transform.localScale = new Vector3(1,1,1);
            startSize = new Vector2(1,1);

            if (text != null)
            {
                text.text = LanguageManager.Instance.GetDialog(sticker.Text);
            }
            if (tooltipText != null)
            {
                tooltipText.text = LanguageManager.Instance.GetDialog(sticker.tooltipText);
            }
            backgroundSticker.rectTransform.sizeDelta = new Vector2(sticker.backgoundSize.x*1.1f, sticker.backgoundSize.y*1.1f);
            backgroundSticker.sprite = sticker.stickerBackground;
            backgroundColor = sticker.color;
            //GetComponent<SpriteRenderer>().color = backgroundColor;
            backgroundSticker.color = backgroundColor;
            if (sticker.type == Sticker.Type.Profile)
            {
                text.transform.localPosition = new Vector2(0, -28);
                text.rectTransform.sizeDelta = new Vector2(75, 28);

            }
            else
            {
                text.transform.localPosition = Vector2.zero;
                text.rectTransform.sizeDelta = defaultSize;

            }

            this.GetComponent<Animator>().SetTrigger("NewSticker");

        }


        //if (GameObject.Find("Kenneth").GetComponent<Interactions>().state == Interactions.State.OnCarnet)
        //    GetComponent<CarnetSticker>().SetBackgroundColor();
        //else transform.GetChild(0).GetComponent<Image>().color = backgroundColor;

        //transform.GetChild(0).GetComponent<Image>().color = backgroundColor;
        //text.rectTransform.sizeDelta = new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y/*  -20*/);
    }

    public void Corou()
    {
        StartCoroutine(Timer());
    }
    public void OnEnable()
    {
        Corou();
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.0001f);
        SetInformations();
    }

    public void HideCarnetMini()
    {
        _carnetMini.GetComponent<SpriteRenderer>().color = new Vector4(0,0,0,0);
    }
}
