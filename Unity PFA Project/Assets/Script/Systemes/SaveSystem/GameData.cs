using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    void Save()
    {
        GameData saveObject = new GameData();
        string json = JsonUtility.ToJson(saveObject);

        GameSaveSystem.Save(json);
    }

    void Load()
    {
        string saveString = GameSaveSystem.Load();
        if (saveString != null)
        {
            GameData saveObject = JsonUtility.FromJson<GameData>(saveString);
            ReturnGameData(saveObject);
        }
    }

    public void ReturnGameData(GameData gameSave)
    {
        if (!gameSave.actualRoom.activeInHierarchy)
        {
            FindObjectOfType<CameraFollow>().actualRoom.SetActive(false);
            FindObjectOfType<CameraFollow>().actualRoom = gameSave.actualRoom;
            FindObjectOfType<CameraFollow>().actualRoom.SetActive(true);
        }

        if (FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Count != gameSave.playableCharacters.Count)
        {
            FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Clear();
            for (int i = 0; i < gameSave.playableCharacters.Count; i++)
            {
                GameObject[] playableCharacterInScene = GameObject.FindGameObjectsWithTag("Player");
                if (playableCharacterInScene != null)
                {
                    foreach(GameObject character in playableCharacterInScene)
                    {
                        if(gameSave.playableCharacters[i].character.name == character.name)
                        {
                            character.transform.position = gameSave.playableCharacters[i].position;
                        }
                    }
                    
                }
                FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Add(new ActiveCharacterScript.PlayableCharacter(gameSave.playableCharacters[i].character, true));
            }
        }


    }
}


public class GameData : MonoBehaviour
{
    
    //public int level = 0;
    public GameObject actualRoom;
    //public List<int> stickersIndexOnBoard;
    //public List<Vector3> stickersPositionOnBoard;
    //public List<int> memoryStickers;
    //public List<string> meetingList;
    //public string language = "french";
    //public List<string> eventList;
    public List<CharacterPosition> playableCharacters; 

    public GameData()
    {
        actualRoom = FindObjectOfType<CameraFollow>().actualRoom;

        for (int i = 0;i< FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Count; i++){
            playableCharacters.Add(new CharacterPosition(FindObjectOfType<ActiveCharacterScript>().playableCharactersList[i].character));

        }
    }

    

}

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
