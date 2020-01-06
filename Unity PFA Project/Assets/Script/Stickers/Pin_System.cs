using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_System : MonoBehaviour
{
    bool mouseOn = false;
    Vector3 screenPoint;
    public GameObject pin;
    public bool click;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 colliderSize;
        colliderSize = GetComponent<RectTransform>().sizeDelta;
        GetComponent<BoxCollider2D>().size = colliderSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && mouseOn && transform.childCount == 4)
        {
            foreach(RectTransform child in transform)
            {
                if(child.name == "Pin 1(Clone)")
                {
                    GameObject.FindObjectOfType<String_Manager>().DeletePin(gameObject);
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public void RemoveFromList()
    {
        Destroy(transform.GetChild(2));
    }

    public void OnMouseEnter()
    {
        mouseOn = true;
    }
    public void OnMouseExit()
    {
        mouseOn = false;
    }

    public void PlaySpawnAnimation()
    {
        
    }

    public void OnMouseDown()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            int i = 0;
            foreach(RectTransform child in transform)
            {
                i++;
                if(child.name == "Pin 1(Clone)")
                {
                    break;
                }
                else 
                {
                    if(i == transform.childCount || transform.childCount < 4)
                    {
                        click = true;
                        time = Time.realtimeSinceStartup;
                    }
                }
            }
        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            foreach(RectTransform child in transform)
            {
                if(child.name == "Pin 1(Clone)")
                {
                    GameObject.FindObjectOfType<String_Manager>().DeletePin(gameObject);
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public void OnMouseUp()
    {
        if(click)
        {
            click = false;
            GameObject newPin = Instantiate(pin, transform);
            newPin.transform.localPosition = new Vector3(0, 25, 0);
            GameObject.FindObjectOfType<String_Manager>().AddPin(gameObject);
        }
    }

    public void OnMouseDrag()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(Time.realtimeSinceStartup - time > 0.2)
            {
                click = false;
            }
            screenPoint = Input.mousePosition;
            screenPoint.z = transform.parent.position.z;
            Camera camera = Camera.FindObjectOfType<Camera>();
            transform.position = camera.ScreenToWorldPoint(screenPoint);
            //transform.position = Camera.main.ScreenToWorldPoint(screenPoint);//
            //transform.position = Input.mousePosition;
        }
    }
}
