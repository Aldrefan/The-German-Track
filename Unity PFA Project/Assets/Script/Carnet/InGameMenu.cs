using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{

    public Slider musicSlider;
    public AudioMixer musicMixer;
    public Slider fxSlider;
    public AudioMixer fxMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    
    public GameObject englishArrow;
    public GameObject frenchArrow;

    bool isFrench;
    float musicValue;
    float fxValue;
    bool canQuit = false;

    void Update()
    {
        musicMixer.SetFloat("musicVolume", musicSlider.value);
        fxMixer.SetFloat("fxVolume", fxSlider.value);
        if(Input.GetButtonDown("Cancel") && canQuit)
        {
            ResumeGame();
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        canQuit = true;
    }

    void OnEnable()
    {
        canQuit = false;
        StartCoroutine("Timer");
    }

    void Start()
    {
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
    }

    public void SaveGame()
    {
        Debug.Log("SaveGame");
        //LIGNE DE COMMANDE DE SAVE GAME
        GameObject.Find("Saver").GetComponent<Saver>().MakeASave();
        transform.GetChild(4).transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true); //feedbackSave
    }

    public void ReturnTitle()
    {
        transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false); //options
        transform.GetChild(4).transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false); //feedbackSave
        transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(true); //returntitle
        transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false); //quitgame
    }

    public void ConfirmReturnTitle()
    {
        Debug.Log("SaveGame");
        //LIGNE DE COMMANDE DE SAVE GAME
        GameObject.Find("Saver").GetComponent<Saver>().MakeASave();
        Debug.Log("ReturnTitle");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ReturnTitleWithoutSave()
    {
        Debug.Log("ReturnTitle");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false); //options
        transform.GetChild(4).transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false); //feedbackSave
        transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false); //returntitle
        transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(true); //quitgame
    }

        public void ConfirmQuitGame()
    {
        Debug.Log("SaveGame");
        //LIGNE DE COMMANDE DE SAVE GAME
        GameObject.Find("Saver").GetComponent<Saver>().MakeASave();
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void QuitGameWithoutSave()
    {
        Debug.Log("QuitGame");
        //GameObject.Find("Saver").GetComponent<Saver>().ClearSave();
        Application.Quit();
    }

    public void CancelSaveOptions()
    {
        transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true); //options
        transform.GetChild(4).transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false); //feedbackSave
        transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false); //returntitle
        transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false); //quitgame
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
        isFrench = false;
        englishArrow.SetActive(true);
        frenchArrow.SetActive(false);
        FrenchSelection();
    }

        public void FrenchSelection()
    {
        isFrench = true;
        frenchArrow.SetActive(true);
        englishArrow.SetActive(false);
    }

    public void MouseEnter(Transform button)
    {
        button.GetComponent<Text>().color = Color.grey;
    }

    public void MouseExit(Transform button)
    {
        button.GetComponent<Text>().color = Color.black;
    }

    public void ButtonChangeOver(Transform button)
    {
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(205f, 92.5f);
        button.GetChild(0).GetComponent<Text>().color = Color.black;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().ChangeState(Interactions.State.Normal);
    }

    /*IEnumerator TimerResume()
    {

    }*/
}
