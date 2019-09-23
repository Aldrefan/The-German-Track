using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_System : MonoBehaviour
{
    bool mouseOn = false;
    Vector3 screenPoint;
    public GameObject pin;
    public bool click;
    public int stickerIndex;
    Vector2 colliderSize;
    float PositionZ;

    // Start is called before the first frame update
    void Start()
    {
        colliderSize = GetComponent<RectTransform>().sizeDelta;
        GetComponent<BoxCollider2D>().size = colliderSize;
        PositionZ = transform.parent.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveFromList()
    {
        Destroy(transform.GetChild(0));
    }

    public void PointerEnter()
    {
        mouseOn = true;
    }
    public void PointerExit()
    {
        mouseOn = false;
    }

    public void PointerDown()
    {
        if(Input.GetKey(KeyCode.Mouse0) && transform.childCount == 0)
        {
            click = true;
        }
        else if(Input.GetKey(KeyCode.Mouse1) && transform.childCount > 0)
        {
            GameObject.FindObjectOfType<String_Manager>().DeletePin(gameObject);
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void PointerUp()
    {
        if(click)
        {
            click = !click;
            GameObject newPin = Instantiate(pin, transform);
            newPin.transform.localPosition = new Vector3(0, 25, 0);
            GameObject.FindObjectOfType<String_Manager>().AddPin(gameObject);
        }
    }

    public void Drag()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            click = false;
            screenPoint = Input.mousePosition;
            screenPoint.z = transform.parent.position.z;
            Camera camera = Camera.FindObjectOfType<Camera>();
            transform.position = camera.ScreenToWorldPoint(screenPoint);
            //transform.position = Camera.main.ScreenToWorldPoint(screenPoint);//
            //transform.position = Input.mousePosition;
        }
    }
}
