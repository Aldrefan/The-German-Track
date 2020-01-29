using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sticker_Display : MonoBehaviour
{
    public Sticker sticker;
    public Text text;
    public Text tooltipText;
    public Image backgroundSticker;
    public Color backgroundColor;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    public void SetInformations()
    {
        text.text = LanguageManager.Instance.GetDialog(sticker.Text);
        tooltipText.text = LanguageManager.Instance.GetDialog(sticker.tooltipText);
        if(sticker.index >= GameObject.Find("Kenneth").GetComponent<PlayerMemory>().charactersRange.x && sticker.index <= GameObject.Find("Kenneth").GetComponent<PlayerMemory>().charactersRange.y)
        text.gameObject.SetActive(false);
        backgroundSticker.sprite = sticker.stickerBackground;
        backgroundColor = sticker.color;
        transform.GetChild(0).GetComponent<Image>().color = backgroundColor;
        backgroundSticker.rectTransform.sizeDelta = new Vector2(sticker.backgoundSize.x, sticker.backgoundSize.y);
        //text.rectTransform.sizeDelta = new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y/*  -20*/);
    }

    void OnEnable()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.001f);
        SetInformations();
    }
}