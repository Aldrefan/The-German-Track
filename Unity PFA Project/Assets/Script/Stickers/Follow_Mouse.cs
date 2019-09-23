using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Mouse : MonoBehaviour
{
    public GameObject pin;
    Vector3 screenPoint;
    bool mouseOn = false;
    String_Manager stringManager;

    // Start is called before the first frame update
    void Start()
    {
        stringManager = GameObject.FindObjectOfType<String_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(1) && mouseOn)
        {
            if(transform.childCount == 0)
            {
                if(stringManager.origin == null)
                {
                    GameObject connard = Instantiate(pin, transform);
                    stringManager.origin = gameObject;
                    connard.transform.localPosition = Vector3.zero;
                }
                else if(stringManager.destination == null)
                {
                    GameObject connard = Instantiate(pin, transform);
                    stringManager.destination = gameObject;
                    connard.transform.localPosition = Vector3.zero;
                    stringManager.destination.transform.GetChild(0).GetComponent<LineRenderer>().enabled = true;
                    stringManager.destination.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(0, new Vector3(stringManager.origin.transform.position.x, stringManager.origin.transform.position.y, stringManager.origin.transform.position.z + 1));
                    stringManager.destination.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, new Vector3(stringManager.destination.transform.position.x, stringManager.destination.transform.position.y, stringManager.destination.transform.position.z + 1));
                }
            }
            else 
            {
                Destroy(transform.GetChild(0).gameObject);
                if(stringManager.origin == gameObject)
                {
                    stringManager.origin = null;
                }
                else if(stringManager.destination == gameObject)
                {
                    stringManager.destination = null;
                }
            }
        } */
    }

    public void Drag()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetKey(KeyCode.Mouse0))
        {
            screenPoint = Input.mousePosition;
            screenPoint.z = 10.0f;
            transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }
        //transform.position = Input.mousePosition;
    }

    public void PointerEnter()
    {
        mouseOn = true;
    }
    public void PointerExit()
    {
        mouseOn = false;
    }
}
