using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject saveFeedback;
    public List<GameObject> showOnStart;
    public List<GameObject> hideOnStart;
    public GameObject saver;
    public string mainMenuName;

    [Header("Main")]
    public GameObject canvasPlay;
    public GameObject canvasTitle;
    public GameObject canvasButtons;


    [Header("Settings")]
    public GameObject canvasOptions;
    public AudioMixer musicMixer;
    public AudioMixer fxMixer;
    public Slider musicSlider;
    public Slider fxSlider;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public GameObject englishArrow;
    public GameObject frenchArrow;
    public GameObject popupChangeLanguage;

    [Header("Quit")]
    public GameObject canvasQuit;
    public GameObject canvasConfirm;


    bool titleActivate;
    float musicValue;
    float fxValue;
    bool returnTitle;

    void Update()
    {
        musicMixer.SetFloat("musicVolume", musicSlider.value);
        fxMixer.SetFloat("fxVolume", fxSlider.value);

        if(Input.anyKeyDown && titleActivate)
        {
            canvasTitle.SetActive(false);
            canvasButtons.SetActive(true);
            canvasPlay.SetActive(true);
            titleActivate = false;
        }
    }

    void OnEnable()
    {
        ReturnMenu();
    }

    void Start()
    {
        titleActivate = true;

        //music initialisation
        musicMixer.GetFloat("musicVolume", out musicValue);
        fxMixer.GetFloat("fxVolume", out fxValue);

        musicSlider.value = musicValue;
        fxSlider.value = fxValue;


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

        //resolution & fullscreen initialisation
        

        //language initialisation
        JsonSave save = SaveGameManager.GetCurrentSave();
        if (save.language == "english") EnglishSelection();
        else FrenchSelection();
        
        canvasTitle.SetActive(true);
        canvasButtons.SetActive(false);
        canvasPlay.SetActive(false);
        canvasOptions.SetActive(false);
        canvasQuit.SetActive(false);
    }

    void ReturnMenu()
    {
        foreach(GameObject objet in showOnStart) objet.SetActive(true);
        foreach(GameObject objet in hideOnStart) objet.SetActive(false);
    }
    public void PlayTypeSound()
    {
        GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
    }

    public void OpenOptions()
    {
        canvasOptions.SetActive(true);
    }
    public void OpenQuit()
    {
        canvasQuit.SetActive(true);
    }

    //Options
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void EnglishSelection()
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        englishArrow.SetActive(true);
        frenchArrow.SetActive(false);
        save.language = "english";
        //message de changement de la langue
    }
    public void FrenchSelection()
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        frenchArrow.SetActive(true);
        englishArrow.SetActive(false);
        save.language = "french";
        //message de changement de la langue
    }
    /*IEnumerator feedbackLanguage(float Time)
    {
        yield return new WaitForSeconds(Time);
        popupChangeLanguage.SetActive(false);
    }*/


    //Quit
    public void SaveGame()
    {
        saver.GetComponent<Saver>().MakeASave();
        saveFeedback.transform.GetChild(0).GetComponent<Text>().text = LanguageManager.Instance.GetDialog("Menu_03");
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
        if(saveGame) saver.GetComponent<Saver>().MakeASave();
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






    public void StartMenu()
    {
        canvasTitle.SetActive(false);
        canvasButtons.SetActive(true);
        canvasPlay.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("NewGame");
    }

    public void LoadGame()
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        save = SaveGameManager.GetCurrentSave();
        SceneManager.LoadScene(save.level);
        Debug.Log("LoadGame");
    }

    /*public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }*/

    

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
}
