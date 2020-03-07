using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicLoad : MonoBehaviour
{
    public bool cinematicFinished;
    public string SceneNameToLoad;
    AsyncOperation nextInterScene;

    private void Start()
    {
        StartCoroutine("LoadYourAsyncScene");

    }

    void Update()
    {
        CheckCinematicState();

    }

    void CheckCinematicState()
    { 

        if (nextInterScene!=null && cinematicFinished && SceneNameToLoad != "")
        {
            nextInterScene.allowSceneActivation = true;
        }
        if (nextInterScene != null && nextInterScene.isDone)
        {
            SceneManager.LoadScene(SceneNameToLoad);
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        nextInterScene = SceneManager.LoadSceneAsync(SceneNameToLoad);
        nextInterScene.allowSceneActivation = false;
        yield return nextInterScene;
    }
}
