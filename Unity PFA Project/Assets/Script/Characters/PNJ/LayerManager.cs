using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LayerManager : MonoBehaviour
{
    public static LayerManager layerManager;

    private int newLayer;

    void Awake()
    {
        layerManager = this;
    }

    public int SetNewLayer(Vector2 objectPosition)
    {
        if(objectPosition.y < ActiveCharacterScript.ActiveCharacter.actualCharacter.transform.position.y)
        {
            newLayer = ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<SpriteRenderer>().sortingOrder + 1;
            return newLayer;
        }
        else 
        {
            newLayer = ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<SpriteRenderer>().sortingOrder - 1;
            return newLayer;
        }
    }
}
