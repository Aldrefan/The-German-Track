using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterScript : MonoBehaviour
{
    public static ActiveCharacterScript ActiveCharacter;
    public GameObject actualCharacter;
    public Sprite KennethSprite;
    [System.Serializable]
    public class PlayableCharacter
    {
        public GameObject character;
        public bool isPlayable;
        public Sprite charactersSprite;

        public PlayableCharacter(GameObject newCharacter, bool playable)
        {
            character = newCharacter;
            isPlayable = playable;
        }
    }
    public List<PlayableCharacter> playableCharactersList;
    public GameObject startingCharacter;

    void Awake()
    {
        ActiveCharacter = this;
    }

    public Sprite GetCharacterSprite(string characterName)
    {
        foreach(PlayableCharacter character in playableCharactersList)
        {
            if(character.character.name == characterName)
            {
                return character.charactersSprite;
            }
        }
        return null;
    }

    public void ChangeActiveCharacter(string newCharacterName)
    {
        for(int i = 0; i < playableCharactersList.Count; i++)
        {
            if(playableCharactersList[i].character.name == newCharacterName && playableCharactersList[i].isPlayable)
            {
                actualCharacter = playableCharactersList[i].character;
            }
        }
    }
}
