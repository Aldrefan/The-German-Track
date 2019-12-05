using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData
{

    //public int level = 0;
    public GameObject actualRoom;
    //public List<int> stickersIndexOnBoard = new List<int>();
    //public List<Vector3> stickersPositionOnBoard;
    //public List<int> memoryStickers;
    //public List<string> meetingList;
    //public string language = "french";
    //public List<string> eventList;
    public List<CharacterPosition> playableCharacters = new List<CharacterPosition>();


    public GameData(CameraFollow camScript, ActiveCharacterScript currentCharacters)
    {
        if (camScript != null && currentCharacters != null)
        {
            actualRoom = camScript.actualRoom;

            for (int i = 0; i < currentCharacters.playableCharactersList.Count; i++)
            {
                playableCharacters.Add(new CharacterPosition(currentCharacters.playableCharactersList[i].character));

            }
        }
    }


}

[System.Serializable]
public class CharacterPosition
{
    public GameObject character;
    public Vector3 position;

    public CharacterPosition(GameObject newCharacter)
    {
        character = newCharacter;
        position = newCharacter.transform.position;
    }

}


