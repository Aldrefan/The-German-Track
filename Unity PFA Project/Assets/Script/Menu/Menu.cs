using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Slider musicSlider;
    public AudioMixer musicMixer;
    public Slider fxSlider;
    public AudioMixer fxMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public Texture2D cursor;

    public GameObject englishArrow;
    public GameObject frenchArrow;
    public GameObject canvasTitle;
    public GameObject canvasButtons;
    public GameObject canvasPlay;
    public GameObject canvasOptions;
    public GameObject canvasQuit;

    bool isFrench;
    bool titleActivate;
    float musicValue;
    float fxValue;

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

    void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    void Start()
    {
        titleActivate = true;

        musicMixer.GetFloat("musicVolume", out musicValue);
        fxMixer.GetFloat("fxVolume", out fxValue);

        musicSlider.value = musicValue;
        fxSlider.value = fxValue;

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
        
        canvasTitle.SetActive(true);
        canvasButtons.SetActive(false);
        canvasPlay.SetActive(false);
        canvasOptions.SetActive(false);
        canvasQuit.SetActive(false);
    }

    void ChangeTexts()
    {

    }

    public void PlayTypeSound()
    {
        GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
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

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }

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
        isFrench = false;
        englishArrow.SetActive(true);
        frenchArrow.SetActive(false);
        save.language = "english";
        SaveGameManager.Save();
        FrenchSelection();
        Debug.Log(isFrench);
    }

    public void FrenchSelection()
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        isFrench = true;
        frenchArrow.SetActive(true);
        englishArrow.SetActive(false);
        save.language = "french";
        SaveGameManager.Save();
        Debug.Log(isFrench);
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
