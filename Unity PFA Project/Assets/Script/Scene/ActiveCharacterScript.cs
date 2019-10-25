using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterScript : MonoBehaviour
{
    public GameObject actualCharacter;
    [System.Serializable]
    public class playableCharacter
    {
        public GameObject character;
        public bool isPlayable;
    }
    public List<playableCharacter> playableCharactersList;
    public GameObject startingCharacter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
