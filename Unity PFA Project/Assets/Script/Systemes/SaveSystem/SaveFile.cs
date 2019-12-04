using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    private void Start()
    {
        GameSaveSystem.Init();
        InitGameData();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
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
                    foreach (GameObject character in playableCharacterInScene)
                    {
                        if (gameSave.playableCharacters[i].character.name == character.name)
                        {
                            character.transform.position = gameSave.playableCharacters[i].position;
                        }
                    }

                }
                FindObjectOfType<ActiveCharacterScript>().playableCharactersList.Add(new ActiveCharacterScript.PlayableCharacter(gameSave.playableCharacters[i].character, true));
            }
        }


    }

    void InitGameData()
    {
        GameSaveSystem.GameDataInput(
            Camera.main.GetComponent<CameraFollow>(),
            FindObjectOfType<ActiveCharacterScript>());
    }
}
