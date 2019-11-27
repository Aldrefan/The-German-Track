using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextAnimation : MonoBehaviour
{
    public List<Text> textsList;
    string currentLine = "";
    float dialogDelay = 0.1f;
    public List<string> stringList;
    int textIndex = 0;
    public List<AudioClip> clickSounds;
    public string levelName;
    private AsyncOperation async;
    bool textFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShowText");
        //SceneManager.LoadScene(levelName);
        StartCoroutine("LoadYourAsyncScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (async != null && textFinished)
            async.allowSceneActivation = true;

        if (async != null && async.isDone)
        {
            SceneManager.LoadScene(levelName);
        }
    }

    void ChangeText()
    {
        if(textIndex < textsList.Count - 1)
        {
            textIndex++;
            StartCoroutine("ShowText");
        }
        else textFinished = true;
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(0.5f);
        ChangeText();
    }

    IEnumerator ShowText()
    {
        for(int i = 0; i < stringList[textIndex].Length + 1; i++)
        {
            int sound = Random.Range(0, clickSounds.Count - 1);
            GetComponent<AudioSource>().clip = clickSounds[sound];
            GetComponent<AudioSource>().Play();
            currentLine = stringList[textIndex].Substring(0, i);
            textsList[textIndex].GetComponent<Text>().text = currentLine;
            yield return new WaitForSeconds(dialogDelay);
        }
        StartCoroutine("WaitTimer");
    }

    IEnumerator LoadYourAsyncScene()
    {
        async = SceneManager.LoadSceneAsync(levelName);
        async.allowSceneActivation = false;
        yield return async;
    }
}
