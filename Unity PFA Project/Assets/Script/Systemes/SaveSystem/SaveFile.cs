using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("Saved");
            SaveGame();
        }
        if (Input.GetKey(KeyCode.L))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        GameSaveSystem.SaveGameData();
    }

    public void SaveSettings()
    {
        GameSaveSystem.SaveSettingsData();
        Debug.Log("Settings Saved !");
    }

    public void LoadGame()
    {
        ReturnGameData(GameSaveSystem.LoadGameData());
    }

    public void LoadSettings()
    {
        ReturnSettingsData(GameSaveSystem.LoadSettingsData());
        Debug.Log("Settings Loaded !");
    }

    void LoadAtStart()
    {
        if (GameSaveSystem.gameToLoad || loadAtStart)
        {
            LoadGame();
        }

    }

    public void ReturnGameData(GameData gameSave)
    {

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
            player.GetComponent<PlayerMemory>().stickerIndexBoardList = gameSave.stickersIndexOnBoard;
            player.GetComponent<PlayerMemory>().stickerIndexCarnetList = gameSave.stickersIndexInCarnet;
            player.GetComponent<PlayerMemory>().allStickers = gameSave.allStickers;
            player.GetComponent<PlayerMemory>().stickersPositionBoard = gameSave.stickersPositionOnBoard;
            player.GetComponent<Interactions>().PnjMet = gameSave.NPCmet;
            player.GetComponent<EventsCheck>().eventsList = gameSave.eventList;

        }

        if (FindObjectOfType<TutoKenneth>() != null)
        {
            FindObjectOfType<TutoKenneth>().skipTuto();
            FindObjectOfType<TutoKenneth>().canSave = true;
        }

        CarnetGoal gameGoals = GameObject.FindObjectOfType<Ken_Canvas_Infos>().transform.Find("Panel").Find("Carnet").Find("Goal").Find("GoalFrame").GetComponent<CarnetGoal>();
        if (gameGoals != null)
        {
            foreach(string goal in gameSave.goalsInProgress)
            {
                gameGoals.NewGoal(goal);
            }
            foreach (string goal in gameSave.goalsComplete)
            {
                gameGoals.RemoveGoal(goal);
            }
        }


    }

    public void ReturnSettingsData(SettingsData settingsSave)
    {
        if (settingsSave != null)
        {
            Debug.Log(settingsSave.musicVol);
            Debug.Log(settingsSave.effectVol);
            Debug.Log(settingsSave.gameLanguage);
            Debug.Log(settingsSave.fullscreenBool);
            Debug.Log(settingsSave.screenResolution);

            AudioMixer musicMixer = Resources.Load<AudioMixer>("SoundMixer/MusicMixer");
            AudioMixer effectMixer = Resources.Load<AudioMixer>("SoundMixer/FXMixer");

            musicMixer.SetFloat("musicVolume", settingsSave.musicVol);
            effectMixer.SetFloat("fxVolume", settingsSave.effectVol);

            if (settingsSave.gameLanguage != null)
            {
                Debug.Log(FindObjectOfType<LanguageManager>().language);
                Debug.Log(settingsSave.gameLanguage);
                FindObjectOfType<LanguageManager>().language = settingsSave.gameLanguage;
            }

            Screen.fullScreen = settingsSave.fullscreenBool;
            Screen.SetResolution((int)settingsSave.screenResolution.x, (int)settingsSave.screenResolution.y, Screen.fullScreen);
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
            FindObjectOfType<DayNightLight>(),
            GameObject.FindObjectOfType<Ken_Canvas_Infos>().transform.Find("Panel").Find("Carnet").Find("Goal").Find("GoalFrame").GetComponent<CarnetGoal>());

        GameSaveSystem.SettingsDataInput(
            Resources.Load<AudioMixer>("SoundMixer/MusicMixer"),
            Resources.Load<AudioMixer>("SoundMixer/FXMixer"),
            FindObjectOfType<LanguageManager>());
    }
}