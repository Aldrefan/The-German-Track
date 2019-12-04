﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Button", menuName = "Button")]
public class Button : ScriptableObject
{
    public string frenchText;
    public string englishText;
    public int index;
    public string eventNeeded;
}