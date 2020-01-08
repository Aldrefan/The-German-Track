using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarnetSticker : MonoBehaviour
{
    public Vector2 colliderSize;
    GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Kenneth");
    }

    void Start()
    {

        //colliderSize = transform.parent.GetComponent<GridLayoutGroup>().cellSize;
        colliderSize = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        GetComponent<BoxCollider2D>().size = colliderSize;
    }

    // Update is called once per frame
    /*public void Update()
    {
        
    }*/

    void OnEnable()
    {
        if(player.GetComponent<Interactions>().PNJContact != null)
        {
            if(player.GetComponent<Interactions>().PNJContact.tag == "PNJInteractable" && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().stickerAlreadyGivenList.Contains(GetComponent<Sticker_Display>().sticker.index))
            {
                transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                //GetComponent<Image>().color = Color.gray;
            }
            else 
            {
                transform.GetChild(0).GetComponent<Image>().color = GetComponent<Sticker_Display>().backgroundColor;
                //GetComponent<Image>().color = Color.white;
            }
        }
    }
    void OnMouseDown()
    {
        if(player.GetComponent<Interactions>().PNJContact != null && player.GetComponent<Interactions>().PNJContact.tag == "PNJinteractable")
        {
            player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().Response(GetComponent<Sticker_Display>().sticker.index);
            player.GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
            transform.parent.parent.parent.parent.gameObject.SetActive(false);
        }
        if(player.GetComponent<Interactions>().PNJContact != null && player.GetComponent<Interactions>().PNJContact.name == "Phone")
        {
            player.GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
            player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>().GetInTouch(GetComponent<Sticker_Display>().sticker.index);
        }
    }

    /*void OnMouseOver()
    {
        Debug.Log("Touch");
        foreach(Transform child in transform)
        {
            if(child.name == "New(Clone)")
            {
                Destroy(child.gameObject);
            }
        }
    }*/
}
