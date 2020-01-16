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
    public GameObject IS_PB;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("ShowText");
        //SceneManager.LoadScene(levelName);
        StartCoroutine("FirstWait");
        StartCoroutine("LoadYourAsyncScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (async != null && textFinished && Input.anyKey)
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
        else 
        {
            textFinished = true;
            IS_PB.SetActive(true);
            //GameObject.Find("ClignoText").GetComponent<Animator>().SetBool("CanLoad", true);
        }
    }

    IEnumerator FirstWait()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("ShowText");
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(0.5f);
        ChangeText();
    }

    IEnumerator ShowText()
    {
        string text = LanguageManager.Instance.GetDialog(stringList[textIndex]);
        for(int i = 0; i < text.Length + 1; i++)
        {
            int sound = Random.Range(0, clickSounds.Count - 1);
            GetComponent<AudioSource>().clip = clickSounds[sound];
            GetComponent<AudioSource>().Play();
            currentLine = text.Substring(0, i);
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
