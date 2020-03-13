using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager CManager;

    [System.Serializable]
    public class CanvasInfos
    {
        public string CanvasName;
        public GameObject CanvasGO;
    }

    [System.Serializable]
    public class CharactersInterfaces
    {
        public string CharcterName;
        public List<CanvasInfos> CanvasList;
    }

    [SerializeField]
    public List<CharactersInterfaces> charactersInterfaces;

    void Awake()
    {
        CManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject GetCanvas(string canvasName)
    {
        foreach(CharactersInterfaces character in charactersInterfaces)
        {
            foreach(CanvasInfos canvas in character.CanvasList)
            {
                if(canvas.CanvasName == canvasName)
                {
                    return canvas.CanvasGO;
                }
            }
        }
        return null;
    }

    // Update is called once per frame
    /*void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(playerInteractionScript.onBoard == false)
            {
                transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }*/
}
