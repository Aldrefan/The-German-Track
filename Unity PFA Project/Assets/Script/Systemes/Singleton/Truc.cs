using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truc : MonoBehaviour
{
    Camera mainCamera;
    public Camera MainCamera => mainCamera = mainCamera ?? Camera.main;

}
