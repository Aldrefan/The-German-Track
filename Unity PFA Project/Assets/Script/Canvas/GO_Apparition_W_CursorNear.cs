using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GO_Apparition_W_CursorNear : MonoBehaviour
{
    Vector2 mousePos;
    public List<GameObject> GameObjectsList;
    float maxMousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        mousePos = Camera.main.WorldToViewportPoint(Input.mousePosition);
        maxMousePos = Vector2.Distance(Input.mousePosition, Camera.main.WorldToScreenPoint(transform.position));
    }
    
    void OnMouseOver()
    {
        float opacity = 2 - Vector2.Distance(Camera.main.WorldToViewportPoint(Input.mousePosition), mousePos);
        float distance = Vector2.Distance(Input.mousePosition, Camera.main.WorldToScreenPoint(transform.position)) / maxMousePos;
        foreach(Transform child in transform)
        {
            if(child.GetComponent<Text>())
            {
                child.GetComponent<Text>().color = new Vector4(1, 1, 1, opacity);
            }
            if(child.GetComponent<SpriteRenderer>())
            {
                child.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, opacity);
            }
        }
        //Debug.Log(distance);
    }

    void OnMouseExit()
    {
        foreach(Transform child in transform)
        {
            if(child.GetComponent<Text>())
            {
                child.GetComponent<Text>().color = new Vector4(1, 1, 1, 1);
            }
            if(child.GetComponent<SpriteRenderer>())
            {
                child.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
            }
        }
    }
}
