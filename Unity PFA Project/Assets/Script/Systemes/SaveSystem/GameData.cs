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
    public List<StickersOnBoard> stickersOnBoard = new List<StickersOnBoard>();
    //CameraFollow -> MainCamera
    public string actualRoomName;
    //CameraPos
    public Vector3 camPosition;
    //ActiveCharacterScript -> GameObject
    public List<CharacterPosition> playableCharacters = new List<CharacterPosition>();
    //GoalsList
    public List<GoalKeys> goalsInProgress = new List<GoalKeys>();
    public List<GoalKeys> goalsComplete = new List<GoalKeys>();
    //StrickersList
    public List<StickersGivenToPNJ.PNJMemory> pnjStickerManager = new List<StickersGivenToPNJ.PNJMemory>();


    public GameData(GameObject mainCamera, ActiveCharacterScript currentCharacters, GameObject characterToPlay, DayNightLight directionalLight, CarnetGoal goalsObject, StickersGivenToPNJ newStrickerManager, Transform BoardTransform)
    {
        currentLevel = SceneManager.GetActiveScene().name;

        if (mainCamera != null && currentCharacters != null)
        {
            actualRoomName = mainCamera.GetComponent<CameraFollow>().actualRoom.name;
            camPosition = mainCamera.transform.position;

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
            NPCmet = characterToPlay.GetComponent<Interactions>().PnjMet;
            eventList = characterToPlay.GetComponent<EventsCheck>().eventsList;
        }

        if (BoardTransform != null)
        {
            foreach(Transform childTransform in BoardTransform)
            {
                if (childTransform.GetComponent<Sticker_Display>())
                {
                    stickersOnBoard.Add(new StickersOnBoard(childTransform.GetComponent<Sticker_Display>().sticker.index,
                                                            childTransform.GetComponent<Sticker_Display>().sticker.type,
                                                            childTransform.localPosition));
                }
            }
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

        if (newStrickerManager != null)
        {
            pnjStickerManager = newStrickerManager.PnjsInGame;
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

[System.Serializable]
public class StickersOnBoard
{
    public int stickerIndex;
    public int stickerType;
    public Vector3 stickerPosition;

    public StickersOnBoard(int newIndex, Sticker.Type newType, Vector3 newPosition)
    {
        stickerIndex = newIndex;

        stickerPosition = newPosition;
        
        if(newType == Sticker.Type.Profile)
        {
            stickerType = 0;
        }else
        if (newType == Sticker.Type.Clue)
        {
            stickerType = 1;
        }
        else
        if (newType == Sticker.Type.Fact)
        {
            stickerType = 2;
        }
        else
        if (newType == Sticker.Type.Hypothesis)
        {
            stickerType = 3;
        }
    }

}