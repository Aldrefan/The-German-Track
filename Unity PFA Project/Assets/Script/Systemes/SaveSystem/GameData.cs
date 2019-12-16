using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameData
{

    //public int level = 0;
    //public string language = "french";

    //Level
    public Scene currentLevel;
    //DayNightLight -> DirectionnalLight
    public DayNightLight dayNightCycle;
    //EventsCheck -> Player
    public List<string> eventList;
    //Interactions -> Player
    public List<string> NPCmet;
    //PlayerMemory -> Player
    public List<int> stickersIndexOnBoard = new List<int>();
    public List<int> stickersIndexInCarnet = new List<int>();
    public List<int> allStickers = new List<int>();
    public List<Vector3> stickersPositionOnBoard = new List<Vector3>();
    //CameraFollow -> MainCamera
    public GameObject actualRoom;
    //ActiveCharacterScript -> GameObject
    public List<CharacterPosition> playableCharacters = new List<CharacterPosition>();


    public GameData(CameraFollow camScript, ActiveCharacterScript currentCharacters, GameObject characterToPlay, DayNightLight directionalLight)
    {
        currentLevel = SceneManager.GetActiveScene();

        if (camScript != null && currentCharacters != null)
        {
            actualRoom = camScript.actualRoom;

            for (int i = 0; i < currentCharacters.playableCharactersList.Count; i++)
            {
                playableCharacters.Add(new CharacterPosition(currentCharacters.playableCharactersList[i].character));

            }
        }

        if (characterToPlay != null)
        {
            stickersIndexOnBoard = characterToPlay.GetComponent<PlayerMemory>().stickerIndexBoardList;
            stickersIndexInCarnet = characterToPlay.GetComponent<PlayerMemory>().stickerIndexCarnetList;
            allStickers = characterToPlay.GetComponent<PlayerMemory>().allStickers;
            stickersPositionOnBoard = characterToPlay.GetComponent<PlayerMemory>().stickersPositionBoard;
            NPCmet = characterToPlay.GetComponent<Interactions>().PnjMet;
            eventList = characterToPlay.GetComponent<EventsCheck>().eventsList;
        }

        if (directionalLight != null)
        {
            dayNightCycle = directionalLight;
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