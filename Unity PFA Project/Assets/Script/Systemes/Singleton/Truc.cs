using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truc : MonoBehaviour
{
    SingletonTest _singleton;
    Camera mainCamera;
    public Camera MainCamera => mainCamera = mainCamera ?? Camera.main;
    public Truc cameraFinder;

    void Awake()
    {
        cameraFinder = this;
    }

    void Start()
    {
        SingletonTest.Instance.DebugLog("Yes");
    }
}
