using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public List<GameObject> objectsToDesactivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndDemo()
    {
        for(int i = 0 ; i < objectsToDesactivate.Count ; i++)
        {
            GameObject newObjectToDesactivate = objectsToDesactivate[i];
            newObjectToDesactivate.SetActive(false);
        }
        GameObject.FindWithTag("MainCamera").GetComponent<Camera_BoardMovements>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(3).localPosition = Vector3.zero;
    }

    public void MouseEnter(Transform button)
    {
        button.GetComponent<Text>().color = Color.grey;
    }
    public void MouseExit(Transform button)
    {
        button.GetComponent<Text>().color = Color.white;
    }

    public void ReturnTitle()
    {
        //Debug.Log("SupprGame");
        //LIGNE DE COMMANDE DE SUPPRIME GAME
        //GameObject.Find("Saver").GetComponent<Saver>().MakeASave();
        //Debug.Log("ReturnTitle");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        //Debug.Log("SupprGame");
        //LIGNE DE COMMANDE DE SUPPRIME GAME
        //GameObject.Find("Saver").GetComponent<Saver>().MakeASave();
        //Debug.Log("QuitGame");
        Application.Quit();
    }
}
