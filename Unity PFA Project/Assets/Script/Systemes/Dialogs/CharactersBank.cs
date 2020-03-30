using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersBank : MonoBehaviour
{
    public static CharactersBank CharBank;

    [System.Serializable]
    public class Character
    {
        public string charName;
        public Sprite charSprite;
    }

    [SerializeField]
    private List<Character> charactersList;

    void Awake()
    {
        CharBank = this;
    }

    public Sprite GetSprite(string CharacterName)
    {
        foreach(Character perso in charactersList)
        {
            if(perso.charName == CharacterName)
            {
                return perso.charSprite;
            }
        }
        return null;
    }
}
