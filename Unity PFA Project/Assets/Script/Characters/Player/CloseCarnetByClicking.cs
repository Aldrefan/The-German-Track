using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCarnetByClicking : MonoBehaviour
{
    void OnMouseDown()
    {
        ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<Interactions>().CloseBookExe();
    }
}
