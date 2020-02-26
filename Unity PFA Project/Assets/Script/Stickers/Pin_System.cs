using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_System : MonoBehaviour
{
    public bool mouseOn = false;
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

        //Clamp
        //Vector3 pos = this.transform.position;
        //pos.x =  Mathf.Clamp(transform.position.x, -350f, 350f);
        //pos.y =  Mathf.Clamp(transform.position.y, -180f, 180f);
        //this.transform.position = pos;
        float xPos = Mathf.Clamp(transform.localPosition.x, -350f, 350f);
        float yPos = Mathf.Clamp(transform.localPosition.y, -180f, 180f);
        transform.localPosition = new Vector3(xPos, yPos, 0.0f);
    }

    void OnMouseEnter()
    {
        mouseOn = true;
    }
    void OnMouseExit()
    {
        mouseOn = false;
    }

    void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && click == false)
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
                        //Debug.Log("Click On");
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

    void OnMouseDrag()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            screenPoint = Input.mousePosition;
            screenPoint.z = transform.parent.position.z + 9;
            Camera camera = Camera.main;
            transform.position = camera.ScreenToWorldPoint(screenPoint);
            //transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            //transform.position = Input.mousePosition;
        }
    }

    public void RemoveFromList()
    {
        Destroy(transform.GetChild(2));
    }

    public void PointerEnter()
    {
        mouseOn = true;
    }
    public void PointerExit()
    {
        mouseOn = false;
    }

    /*public void PointerDown()
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
    }*/

    /*public void PointerUp()
    {
        if(click)
        {
            click = false;
            GameObject newPin = Instantiate(pin, transform);
            newPin.transform.localPosition = new Vector3(0, 25, 0);
            GameObject.FindObjectOfType<String_Manager>().AddPin(gameObject);
        }
    }*/

    public void OnMouseUp()
    {
        if(Time.realtimeSinceStartup - time < 0.1)
        {
            GameObject newPin = Instantiate(pin, transform);
            newPin.transform.localPosition = new Vector3(0, 25, 0);
            GameObject.FindObjectOfType<String_Manager>().AddPin(gameObject);
            click = false;
        }
        else click = false;
    }

    public void Drag()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            screenPoint = Input.mousePosition;
            screenPoint.z = transform.parent.position.z;
            Camera camera = Camera.FindObjectOfType<Camera>();
            transform.position = camera.ScreenToWorldPoint(screenPoint);
            //transform.position = Camera.main.ScreenToWorldPoint(screenPoint);//
            //transform.position = Input.mousePosition;
        }
    }
}
