using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameData
{

    //public int level = 0;
    //public string language = "french";

    //Level
    public string currentLevel;
    //DayNightLight -> DirectionnalLight
    public bool dayNightCycle;
    //EventsCheck -> Player
    public List<string> eventList;
    //Interactions -> Player
    public List<string> NPCmet;
    //PlayerMemory -> Player
    //public List<int> stickersIndexOnBoard = new List<int>();
    //public List<int> stickersIndexInCarnet = new List<int>();
    public List<int> allStickers = new List<int>();
    public List<Vector3> stickersPositionOnBoard = new List<Vector3>();
    //CameraFollow -> MainCamera
    public string actualRoomName;
    //ActiveCharacterScript -> GameObject
    public List<CharacterPosition> playableCharacters = new List<CharacterPosition>();
    //GoalsList
    public List<GoalKeys> goalsInProgress = new List<GoalKeys>();
    public List<GoalKeys> goalsComplete = new List<GoalKeys>();


    public GameData(CameraFollow camScript, ActiveCharacterScript currentCharacters, GameObject characterToPlay, DayNightLight directionalLight, CarnetGoal goalsObject)
    {
        currentLevel = SceneManager.GetActiveScene().name;

        if (camScript != null && currentCharacters != null)
        {
            actualRoomName = camScript.actualRoom.name;

            for (int i = 0; i < currentCharacters.playableCharactersList.Count; i++)
            {
                playableCharacters.Add(new CharacterPosition(currentCharacters.playableCharactersList[i].character));

            }
        }

        if (characterToPlay != null)
        {
            //stickersIndexOnBoard = characterToPlay.GetComponent<PlayerMemory>().stickerIndexBoardList;
            //stickersIndexInCarnet = characterToPlay.GetComponent<PlayerMemory>().stickerIndexCarnetList;
            allStickers = characterToPlay.GetComponent<PlayerMemory>().allStickers;
            stickersPositionOnBoard = characterToPlay.GetComponent<PlayerMemory>().stickersPositionBoard;
            NPCmet = characterToPlay.GetComponent<Interactions>().PnjMet;
            eventList = characterToPlay.GetComponent<EventsCheck>().eventsList;
        }

        if (directionalLight.time == DayNightLight.timeEnum.Day)
        {
            dayNightCycle = true;
        }
        else
        {
            dayNightCycle = false;

        }

        if (goalsObject != null)
        {
            goalsInProgress = goalsObject.goalList;
            goalsComplete = goalsObject.removeGoalList;
        }
    }


}

[System.Serializable]
public class CharacterPosition
{
    public string characterName;
    public Vector3 characterPosition;

    public CharacterPosition(GameObject newCharacter)
    {
        characterName = newCharacter.name;
        characterPosition = newCharacter.transform.position;
    }

}