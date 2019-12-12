using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sticker", menuName = "Sticker")]
public class Sticker : ScriptableObject
{
    public string frenchText;
    public string englishText;
    public Color color;
    public int index;
    public Sprite stickerBackground;
    public Vector2 backgoundSize;
    public string frenchTooltip;
    public string englishTooltip;
}
