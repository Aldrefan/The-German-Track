using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    private void Start()
    {
        GameSaveSystem.Init();
        InitGameData();
        LoadAtStart();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("Saved");
            Save();
        }
        if (Input.GetKey(KeyCode.L))
        {
            Load();
        }
    }

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
        if (GameSaveSystem.gameToLoad)
        {
            ReturnGameData(GameSaveSystem.Load());
        }
    }

    public void ReturnGameData(GameData gameSave)
    {


        if (FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Count != gameSave.playableCharacters.Count)
        {
            FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Clear();

            foreach (CharacterPosition character in gameSave.playableCharacters)
            {
                FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Add(new ActiveCharacterScript.PlayableCharacter(character.character, true));
            }
        }

        for (int i = 0; i < gameSave.playableCharacters.Count; i++)
        {
            GameObject[] playableCharacterInScene = GameObject.FindGameObjectsWithTag("Player");
            if (playableCharacterInScene != null)
            {
                foreach (GameObject character in playableCharacterInScene)
                {
                    if (gameSave.playableCharacters[i].character.name == character.name)
                    {
                        character.transform.position = gameSave.playableCharacters[i].position;
                    }
                }
            }
        }

        if (!gameSave.actualRoom.activeInHierarchy)
        {
            FindObjectOfType<CameraFollow>().actualRoom.SetActive(false);
            FindObjectOfType<CameraFollow>().actualRoom = gameSave.actualRoom;
            FindObjectOfType<CameraFollow>().actualRoom.SetActive(true);
            FindObjectOfType<CameraFollow>().transform.position += FindObjectOfType<EventsCheck>().transform.position;
            FindObjectOfType<CameraFollow>().InitRoomLimit();

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
            player.GetComponent<PlayerMemory>().stickerIndexBoardList = gameSave.stickersIndexOnBoard;
            player.GetComponent<PlayerMemory>().stickerIndexCarnetList = gameSave.stickersIndexInCarnet;
            player.GetComponent<PlayerMemory>().allStickers = gameSave.allStickers;
            player.GetComponent<PlayerMemory>().stickersPositionBoard = gameSave.stickersPositionOnBoard;
            player.GetComponent<Interactions>().PnjMet = gameSave.NPCmet;
            player.GetComponent<EventsCheck>().eventsList = gameSave.eventList;

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
            player);
    }
}
