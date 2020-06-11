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
        /*if(player.GetComponent<Interactions>().PNJContact != null)
        {
            if(player.GetComponent<Interactions>().PNJContact.tag == "PNJinteractable" && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().stickerAlreadyGivenList.Contains(GetComponent<Sticker_Display>().sticker.index))
            {
                transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                Debug.Log(GetComponent<Sticker_Display>().name + "has already been used");
                //GetComponent<Image>().color = Color.gray;
            }
            else 
            {
                transform.GetChild(0).GetComponent<Image>().color = GetComponent<Sticker_Display>().backgroundColor;
                Debug.Log(GetComponent<Sticker_Display>().name + "has never been used");
                //GetComponent<Image>().color = Color.white;
            }
        }*/
        SetBackgroundColor();
        DetectIfCanBeCalled();
    }

    public void SetBackgroundColor()
    {
        if(player.GetComponent<Interactions>().PNJContact!=null && player.GetComponent<Interactions>().PNJContact.tag == "PNJinteractable" && StickersGivenToPNJ.SGTP.CheckStickerInList(player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().PNJName, GetComponent<Sticker_Display>().sticker.index)/*player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().stickerAlreadyGivenList.Contains(GetComponent<Sticker_Display>().sticker.index*/)
        {
            transform.GetChild(0).GetComponent<Image>().color = Color.gray;
            //Debug.Log(GetComponent<Sticker_Display>().name + "has already been used");
            //GetComponent<Image>().color = Color.gray;
        }
        else 
        {
            transform.GetChild(0).GetComponent<Image>().color = GetComponent<Sticker_Display>().sticker.color;
            //Debug.Log(GetComponent<Sticker_Display>().name + "has never been used");
            //GetComponent<Image>().color = Color.white;
        }
    }

    void OnMouseDown()
    {
        if(player.GetComponent<Interactions>().PNJContact != null && (player.GetComponent<Interactions>().PNJContact.tag == "PNJinteractable"
                || (player.GetComponent<Interactions>().PNJContact.tag == "Interaction" && player.GetComponent<Interactions>().PNJContact.name == "Lamp")) && (GameObject.FindObjectOfType<DialogInterface>()))
        {
            player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().Response(GetComponent<Sticker_Display>().sticker.index);
            player.GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
            transform.parent.parent.parent.parent.gameObject.SetActive(false);

        }
        if (player.GetComponent<Interactions>().PNJContact != null && player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>())
        {
            player.GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
            player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>().GetInTouch(GetComponent<Sticker_Display>().sticker);
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

    void DetectIfCanBeCalled()
    {
        //Phone phone = GameObject.FindObjectOfType<Phone>();
        if (player.GetComponent<Interactions>().PNJContact != null && player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>())
        {
            if ( player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>().contactList != null && player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>().contactList.Contains(GetComponent<Sticker_Display>().sticker.index))
            {
                int index = default;

                for (int i = 0; i < player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>().contactList.Count; i++)
                {
                    if (player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>().contactList[i] == GetComponent<Sticker_Display>().sticker.index)
                    {
                        index = i;
                    }

                }
                if ( player.GetComponent<Interactions>().PnjMet.Contains(player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>().transform.GetChild(index).name) || player.GetComponent<Interactions>().PNJContact.GetComponent<Phone>().justStickerNeededList[index])
                {
                    this.transform.Find("PhoneIcon").gameObject.SetActive(true);
                }
            }
        }
        else
        {
            this.transform.Find("PhoneIcon").gameObject.SetActive(false);

        }
    }


}