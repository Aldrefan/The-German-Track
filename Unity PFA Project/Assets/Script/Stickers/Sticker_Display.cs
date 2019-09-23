using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sticker_Display : MonoBehaviour
{
    public Sticker sticker;
    public Text text;
    public Image backgroundSticker;
    public Color backgroundColor;
    // Start is called before the first frame update
    void Start()
    {
        SetInformations();
    }

    public void SetInformations()
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        if(save.language == "french")
        {
            text.text = sticker.frenchText;
        }
        else text.text = sticker.englishText;
        backgroundSticker.sprite = sticker.stickerBackground;
        backgroundSticker.color = sticker.color;
        backgroundSticker.rectTransform.sizeDelta = new Vector2(sticker.backgoundSize.x, sticker.backgoundSize.y);
    }
}
