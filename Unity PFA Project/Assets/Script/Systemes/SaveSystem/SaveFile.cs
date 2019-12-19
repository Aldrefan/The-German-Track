using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    public bool loadAtStart;
    List<GameObject> roomList = new List<GameObject>();


    private void Start()
    {
        GameSaveSystem.Init();
        InitGameData();
        BuildRoomList();

        Debug.Log(GameSaveSystem.gameToLoad);
        LoadAtStart();

    }

    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.K))
    //    {
    //        Debug.Log("Saved");
    //        Save();
    //    }
    //    if (Input.GetKey(KeyCode.L))
    //    {
    //        Load();
    //    }
    //}

    void Save()
    {
        GameSaveSystem.Save();
    }

    void Load()
    {
        ReturnGameData(GameSaveSystem.Load());
    }

    void LoadAtStart()
    {
        if (GameSaveSystem.gameToLoad || loadAtStart)
        {
            Load();
        }

    }

    public void ReturnGameData(GameData gameSave)
    {


        if (FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Count != gameSave.playableCharacters.Count)
        {


        }

        for (int i = 0; i < gameSave.playableCharacters.Count; i++)
        {
            GameObject[] playableCharacterInScene = GameObject.FindGameObjectsWithTag("Player");

            if (playableCharacterInScene != null)
            {
                foreach (GameObject character in playableCharacterInScene)
                {

                    if (gameSave.playableCharacters[i].characterName == character.name)
                    {
                        if (i == 0)
                        {
                            FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Clear();

                        }

                        character.transform.position = gameSave.playableCharacters[i].characterPosition;
                        FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Add(new ActiveCharacterScript.PlayableCharacter(character, true));
                    }
                }
            }
        }

        GameObject savedRoom = null;

        for (int i = 0; i< roomList.Count; i++)
        {

            if (roomList[i].name == gameSave.actualRoomName)
            {
                savedRoom =roomList[i];
            }
            
        }

        GameObject KPlayer = null;
        foreach(EventsCheck actualPlayer in FindObjectsOfType<EventsCheck>()){
                if(actualPlayer.gameObject.name == "Kenneth"){
                        KPlayer = actualPlayer.gameObject;
                }
        }

        if (!savedRoom.activeInHierarchy)
        {
            CameraFollow gameCam = FindObjectOfType<CameraFollow>();
            gameCam.actualRoom.SetActive(false);
            gameCam.actualRoom = savedRoom;
            gameCam.actualRoom.SetActive(true);
            //gameCam.transform.position += KPlayer.transform.position /*+= FindObjectOfType<EventsCheck>().transform.position*/;
            gameCam.InitRoomLimit();
        }

        GameObject levelLight = FindObjectOfType<DayNightLight>().gameObject;
        if (levelLight != null)
        {
            if (gameSave.dayNightCycle)
            {
                levelLight.GetComponent<DayNightLight>().time = DayNightLight.timeEnum.Day;
                levelLight.GetComponent<Light>().intensity = savedRoom.GetComponent<SceneInformations>().dayLightValue;
            }
            else
            {
                levelLight.GetComponent<DayNightLight>().time = DayNightLight.timeEnum.Night;
                levelLight.GetComponent<Light>().intensity = savedRoom.GetComponent<SceneInformations>().nightLightValue;
            }
        }

        GameObject player = null;
        foreach (Interactions Charac in FindObjectsOfType<Interactions>())
        {
            if (Charac.gameObject.name == "Kenneth")
            {
                player = Charac.gameObject;

            }
        }
        if (player != null)
        {
            CheckListInt(player.GetComponent<PlayerMemory>().stickerIndexBoardList, gameSave.stickersIndexOnBoard);
            CheckListInt(player.GetComponent<PlayerMemory>().stickerIndexCarnetList, gameSave.stickersIndexInCarnet);
            CheckListInt(player.GetComponent<PlayerMemory>().allStickers, gameSave.allStickers);
            CheckListVector3(player.GetComponent<PlayerMemory>().stickersPositionBoard, gameSave.stickersPositionOnBoard);
            CheckListString(player.GetComponent<Interactions>().PnjMet, gameSave.NPCmet);
            CheckListString(player.GetComponent<EventsCheck>().eventsList, gameSave.eventList);
            //player.GetComponent<PlayerMemory>().stickerIndexBoardList = gameSave.stickersIndexOnBoard;
            //player.GetComponent<PlayerMemory>().stickerIndexCarnetList = gameSave.stickersIndexInCarnet;
            //player.GetComponent<PlayerMemory>().allStickers = gameSave.allStickers;
            //player.GetComponent<PlayerMemory>().stickersPositionBoard = gameSave.stickersPositionOnBoard;
            //player.GetComponent<Interactions>().PnjMet = gameSave.NPCmet;
            //player.GetComponent<EventsCheck>().eventsList = gameSave.eventList;

        }

    }

    void BuildRoomList()
    {
        foreach (SceneInformations room in FindObjectsOfType<SceneInformations>())
        {

            roomList.Add(room.gameObject);

            if (room.gameObject.name != FindObjectOfType<CameraFollow>().actualRoom.name)
            {
                room.gameObject.SetActive(false);
            }
        }
    }

    void InitGameData()
    {
        GameObject player = null;
        foreach (Interactions Charac in FindObjectsOfType<Interactions>())
        {
            if (Charac.gameObject.name == "Kenneth")
            {
                player = Charac.gameObject;

            }
        }

        GameSaveSystem.GameDataInput(
            Camera.main.GetComponent<CameraFollow>(),
            FindObjectOfType<ActiveCharacterScript>(),
            player,
            FindObjectOfType<DayNightLight>());
    }

    void CheckListInt(List<int> intList,List<int> savedIntList)
    {
        foreach(int index in savedIntList)
        {
            if (!intList.Contains(index))
            {
                intList.Add(index);
            }
        }
    }
    void CheckListVector3(List<Vector3> intList, List<Vector3> savedIntList)
    {
        foreach (Vector3 index in savedIntList)
        {
            if (!intList.Contains(index))
            {
                intList.Add(index);
            }
        }
    }
    void CheckListString(List<string> intList, List<string> savedIntList)
    {
        foreach (string index in savedIntList)
        {
            if (!intList.Contains(index))
            {
                intList.Add(index);
            }
        }
    }
}