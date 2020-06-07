using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truc : MonoBehaviour
{
    Camera mainCamera;
    public Camera MainCamera => mainCamera = mainCamera ?? Camera.main;

    void Awake()
    {
        
    }

    void Start()
    {
    //    Debug.Log(MainCamera.name);
    }
}
