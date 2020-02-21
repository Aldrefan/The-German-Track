using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sticker", menuName = "Sticker")]
public class Sticker : ScriptableObject
{
    public string Text;
    public Color color;
    public int index;
    public Sprite stickerBackground;
    public Vector2 backgoundSize;
    public string tooltipText;
    [SerializeField]
    public enum Type{Profile, Fact, Clue, Hypothesis}
    public Type type;
}
