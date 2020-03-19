using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            CheckPin();
        }
    }

    void OnMouseDrag()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(transform.parent.GetComponent<HorizontalLayoutGroup>())
            {
                transform.parent = CanvasManager.CManager.GetCanvas("Board_FIX").transform;
            }

            if(Vector3.Distance(screenPoint, Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.position)) < 110 && !CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.GetComponent<Animator>().GetBool("Unwrap"))
            {
                this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
            }
            else if(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.GetComponent<Animator>().GetBool("Unwrap") && screenPoint.y > Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.position).y - 50 && screenPoint.y < Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.position).y + 50)
            {
                this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
            }
            if(Vector3.Distance(screenPoint, Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.position)) < 110 && !CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.GetComponent<Animator>().GetBool("Unwrap"))
            {
                this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
            }
            else if(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.GetComponent<Animator>().GetBool("Unwrap") && screenPoint.y > Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.position).y - 50 && screenPoint.y < Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.position).y + 50)
            {
                this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
            }

            if(Vector3.Distance(screenPoint, Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.position)) < 110 && !CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.GetComponent<Animator>().GetBool("Unwrap"))
            {
                this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
            }
            else if(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.GetComponent<Animator>().GetBool("Unwrap") && screenPoint.y > Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.position).y - 50 && screenPoint.y < Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.position).y + 50)
            {
                this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
            }

            if(Vector3.Distance(screenPoint, Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.position)) < 110 && !CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.GetComponent<Animator>().GetBool("Unwrap"))
            {
                this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
            }
            else if(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.GetComponent<Animator>().GetBool("Unwrap") && screenPoint.y > Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.position).y - 50 && screenPoint.y < Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.position).y + 50)
            {
                this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
            }
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
        if(Time.realtimeSinceStartup - time < 0.15)
        {
            GameObject newPin = Instantiate(pin, transform);
            newPin.transform.localPosition = new Vector3(0, 25, 0);
            GameObject.FindObjectOfType<String_Manager>().AddPin(gameObject);
            click = false;
        }
        else
        {
            click = false;
            if(Vector3.Distance(screenPoint, Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.position)) < 110 && !CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.GetComponent<Animator>().GetBool("Unwrap") && GetComponent<Sticker_Display>().sticker.type == Sticker.Type.Profile)
            {
                transform.SetParent(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles);
                CheckPin();
            }
            else if(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.GetComponent<Animator>().GetBool("Unwrap") && screenPoint.y > Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.position).y - 50 && screenPoint.y < Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles.position).y + 50 && GetComponent<Sticker_Display>().sticker.type == Sticker.Type.Profile)
            {
                transform.SetParent(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileProfiles);
                CheckPin();
            }

            if(Vector3.Distance(screenPoint, Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.position)) < 110 && !CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.GetComponent<Animator>().GetBool("Unwrap") && GetComponent<Sticker_Display>().sticker.type == Sticker.Type.Clue)
            {
                transform.SetParent(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices);
                CheckPin();
            }
            else if(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.GetComponent<Animator>().GetBool("Unwrap") && screenPoint.y > Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.position).y - 50 && screenPoint.y < Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices.position).y + 50 && GetComponent<Sticker_Display>().sticker.type == Sticker.Type.Clue)
            {
                transform.SetParent(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileIndices);
                CheckPin();
            }

            if(Vector3.Distance(screenPoint, Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.position)) < 110 && !CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.GetComponent<Animator>().GetBool("Unwrap") && GetComponent<Sticker_Display>().sticker.type == Sticker.Type.Fact)
            {
                transform.SetParent(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits);
                CheckPin();
            }
            else if(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.GetComponent<Animator>().GetBool("Unwrap") && screenPoint.y > Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.position).y - 50 && screenPoint.y < Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits.position).y + 50 && GetComponent<Sticker_Display>().sticker.type == Sticker.Type.Fact)
            {
                transform.SetParent(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileFaits);
                CheckPin();
            }

            if(Vector3.Distance(screenPoint, Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.position)) < 110 && !CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.GetComponent<Animator>().GetBool("Unwrap")  && GetComponent<Sticker_Display>().sticker.type == Sticker.Type.Hypothesis)
            {
                transform.SetParent(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses);
                CheckPin();
            }
            else if(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.GetComponent<Animator>().GetBool("Unwrap") && screenPoint.y > Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.position).y - 50 && screenPoint.y < Camera.main.WorldToScreenPoint(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses.position).y + 50 && GetComponent<Sticker_Display>().sticker.type == Sticker.Type.Hypothesis)
            {
                transform.SetParent(CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHypothèses);
                CheckPin();
            }
            
        }
    }

    void CheckPin()
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
