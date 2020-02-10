using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;

public class Menu : MonoBehaviour
{
    public string mainMenuName;
    public GameObject saveFeedback;
    public GameObject saver;
    public GameObject languageManager;
    public bool changeOptions;
    float musicValue;
    float fxValue;
    bool returnTitle;
    bool isFullscreen;
    int resolutionIndex;
    int speedIndex;
    float slowSpeed = 0.04f;
    float midSpeed = 0.025f;
    float fastSpeed = 0.01f;
    public List<GameObject> showOnStart;
    public List<GameObject> hideOnStart;

    [Header("Main")]
    public GameObject canvasPlay;
    public GameObject canvasTitle;
    public GameObject canvasButtons;
    public GameObject loadButton;

    [Header("Settings")]
    public GameObject canvasOptions;
    public AudioMixer musicMixer;
    public AudioMixer fxMixer;
    public Slider musicSlider;
    public Slider fxSlider;
    public Dropdown resolutionDropdown;
    public Dropdown speedDropdown;
    Resolution[] resolutions;
    public GameObject fullscreenCheckbox;
    public GameObject englishArrow;
    public GameObject frenchArrow;

    [Header("Quit")]
    public GameObject canvasQuit;
    public GameObject canvasConfirm;

    void Update()
    {
        //set les volumes des mixers selon les values des sliders
        musicMixer.SetFloat("musicVolume", musicSlider.value);
        fxMixer.SetFloat("fxVolume", fxSlider.value);

        //set le speed des textes selon la value du slider
        


        if(Input.anyKeyDown && canvasTitle!=null)
        {
            if(canvasTitle.activeSelf)
            {
                canvasTitle.SetActive(false);
                canvasButtons.SetActive(true);
                canvasPlay.SetActive(true);
            }
        }

        CanvasOptionsOut();
    }

    void OnEnable()
    {
        //active le menu à sa forme initiale
        ReturnMenu();
    }

    void Start()
    {


        //active le menu à sa forme initiale
        ReturnMenu();

        //settings initialisation
        saver.GetComponent<SaveFile>().LoadSettings();


        //resolution dropdown
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        //speed dropdown
        speedDropdown.ClearOptions();

        List<string> speed = new List<string>();
        /*speed.Add(LanguageManager.Instance.GetDialog("Speed1"));
        speed.Add(LanguageManager.Instance.GetDialog("Speed2"));
        speed.Add(LanguageManager.Instance.GetDialog("Speed3"));*/
        speed.Add("x1");
        speed.Add("x2");
        speed.Add("x3");

        speedDropdown.AddOptions(speed);

        if(LanguageManager.Instance.dialogSpeed == 0)
        {
            speedDropdown.value = 0;
            LanguageManager.Instance.dialogSpeed = slowSpeed;
        }
        else if(LanguageManager.Instance.dialogSpeed == slowSpeed)
        {
            speedDropdown.value = 0;
        }
        else if(LanguageManager.Instance.dialogSpeed == midSpeed)
        {
            speedDropdown.value = 1;
        }
        else if(LanguageManager.Instance.dialogSpeed == fastSpeed)
        {
            speedDropdown.value = 2;
        }

        speedDropdown.RefreshShownValue();
        


        //resolution & fullscreen initialisation
        SetResolution(resolutionIndex);
        SetFullscreen(isFullscreen);
        

        //language initialisation
        //JsonSave save = SaveGameManager.GetCurrentSave();
        if (languageManager.GetComponent<LanguageManager>().language == "english") EnglishSelection();
        else FrenchSelection();


        //fullscreen checkbox initialisation
        if (Screen.fullScreen) fullscreenCheckbox.GetComponent<Toggle>().isOn = true;
        else fullscreenCheckbox.GetComponent<Toggle>().isOn = false;


        //music initialisation
        musicMixer.GetFloat("musicVolume", out musicValue);
        fxMixer.GetFloat("fxVolume", out fxValue);

        musicSlider.value = musicValue;
        fxSlider.value = fxValue;
        
        
        //settings initialisation
        saver.GetComponent<SaveFile>().LoadSettings();



        //check if save exist
        if(loadButton != null)
        {
            LoadButtonActive();
        }
    }





    void ReturnMenu()
    {
        foreach(GameObject objet in showOnStart) objet.SetActive(true);
        foreach(GameObject objet in hideOnStart) objet.SetActive(false);
    }
    public void CanvasPlay()
    {
        if (!canvasPlay.gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        canvasPlay.SetActive(true);
        canvasOptions.SetActive(false);
        canvasQuit.SetActive(false);
    }
    public void Continue()
    {
        gameObject.SetActive(false);
        GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.GetComponent<Interactions>().ChangeState(Interactions.State.Normal);
    }

    public void CanvasOptions()
    {
        if (!canvasOptions.gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        canvasOptions.SetActive(true);
        canvasPlay.SetActive(false);
        canvasQuit.SetActive(false);

    }
    void CanvasOptionsOut()
    {
        if(!changeOptions && canvasOptions.gameObject.activeSelf)
        {
            changeOptions = true;
        }
        if (changeOptions && !canvasOptions.gameObject.activeSelf)
        {
            FindObjectOfType<SaveFile>().SaveSettings();
            changeOptions = false;
        }
    }
    public void CanvasQuit()
    {
        if (!canvasQuit.gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        canvasQuit.SetActive(true);
        canvasPlay.SetActive(false);
        canvasOptions.SetActive(false);
    }
    public void MouseEnter(Transform button)
    {
        button.GetComponent<Text>().color = Color.grey;
    }
    public void MouseExit(Transform button)
    {
        button.GetComponent<Text>().color = Color.black;
    }
    void LoadButtonActive()
    {
        if (File.Exists(Application.dataPath + "/Saves/gameSave.tgt"))
        {
            loadButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            loadButton.GetComponent<Button>().interactable = false;
            GameSaveSystem.gameToLoad = false;
        }
    }





    //Play
    public void NewGame()
    {
        if (GameSaveSystem.gameToLoad)
        {
            GameSaveSystem.gameToLoad = false;
        }
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        GameSaveSystem.gameToLoad = true;
        string levelName = "InterScene" + GameSaveSystem.ReturnLevelName();
        Debug.Log(GameSaveSystem.ReturnLevelName());
        SceneManager.LoadScene(levelName);
    }





    //Options
    public void SetResolution(int resolutionIndexTemp)
    {
        resolutionIndex = resolutionIndexTemp;
        Resolution resolution = resolutions[resolutionIndexTemp];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    /*public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }*/
    public void SetSpeed(int speedIndexTemp)
    {
        speedIndex = speedIndexTemp;
        switch(speedIndex)
        {
            case 0:
                LanguageManager.Instance.dialogSpeed = slowSpeed;
                break;
            case 1:
                LanguageManager.Instance.dialogSpeed = midSpeed;
                break;
            case 2:
                LanguageManager.Instance.dialogSpeed = fastSpeed;
                break;
        }

    }
    public void SetFullscreen(bool isFullscreenTemp)
    {
        isFullscreen = isFullscreenTemp;
        Screen.fullScreen = isFullscreenTemp;
    }
    public void EnglishSelection()
    {
        languageManager.GetComponent<LanguageManager>().language = "english";
        englishArrow.SetActive(true);
        frenchArrow.SetActive(false);
        RefreshTexts();
    }
    public void FrenchSelection()
    {
        languageManager.GetComponent<LanguageManager>().language = "french";
        frenchArrow.SetActive(true);
        englishArrow.SetActive(false);
        RefreshTexts();
    }
    void RefreshTexts()
    {
        TextApparition[] textsToRefresh = GameObject.FindObjectsOfType<TextApparition>();
        foreach(TextApparition objet in textsToRefresh)
        {
            objet.gameObject.SetActive(false);
            objet.gameObject.SetActive(true);
        }
    }
    public void TypePlaySound()
    {
        GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
    }





    //Quit
    public void SaveGame()
    {
        saver.GetComponent<SaveFile>().SaveGame();
        saveFeedback.GetComponent<Animator>().SetTrigger("Save");
    }
    public void QuitGame()
    {
        returnTitle = false;
        canvasConfirm.SetActive(true);
    }
    public void ReturnToMenu()
    {
        returnTitle = true;
        canvasConfirm.SetActive(true);
    }
    public void ConfirmQuitGameWithSave(bool saveGame)
    {
        if(saveGame) saver.GetComponent<SaveFile>().SaveGame();
        //else don't save

        if(returnTitle) SceneManager.LoadScene(mainMenuName);
        else Application.Quit();
    }
    public void ReturnTitle()
    {
        canvasOptions.SetActive(false);
        canvasPlay.SetActive(true);
        canvasQuit.SetActive(false);
    }
    public void FullyQuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }
}