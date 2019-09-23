using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogCanvas;

    // Start is called before the first frame update
    void Start()
    {
        DialogCanvas = GameObject.Find("DialogCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dialogue()
    {
        DialogCanvas.SetActive(true);
    }
}
