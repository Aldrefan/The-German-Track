using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public List<int> contactList;
    public List<bool> justStickerNeededList;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Kenneth");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetInTouch(Sticker profil)
    {
        if(contactList.Contains(profil.index))
        {
            int i = 0;
            for(i = 0; i < contactList.Count; i++)
            {
                //Debug.Log("index : " + i);
                if(profil.index == contactList[i])
                {
                    //Debug.Log("index found");
                    if(justStickerNeededList[i])
                    {
                        //Debug.Log("Only Sticker Needed");
                        player.GetComponent<Interactions>().CloseBookExe();
                        GetComponent<BoxCollider2D>().enabled = false;
                        player.GetComponent<Interactions>().PNJContact = transform.GetChild(i).gameObject;
                        player.GetComponent<Interactions>().StartDialog();
                        break;
                    }
                    else if(player.GetComponent<Interactions>().PnjMet.Contains(transform.GetChild(i).name))
                    {
                        //Debug.Log("Meeting with " + transform.GetChild(i).name + " Needed");
                        player.GetComponent<Interactions>().CloseBookExe();
                        GetComponent<BoxCollider2D>().enabled = false;
                        player.GetComponent<Interactions>().PNJContact = transform.GetChild(i).gameObject;
                        player.GetComponent<Interactions>().StartDialog();
                        break;
                    }
                    else
                    {
                        player.GetComponent<Interactions>().CloseBookExe();
                        GetComponent<PNJ>().ChangeDialog(GetComponent<PNJ>().negativeQuote);
                    }
                }
                else
                {
                    player.GetComponent<Interactions>().CloseBookExe();
                    GetComponent<PNJ>().ChangeDialog(GetComponent<PNJ>().negativeQuote);
                }
            }
            if(i > contactList.Count)
            {
                player.GetComponent<Interactions>().CloseBookExe();
                GetComponent<PNJ>().ChangeDialog(GetComponent<PNJ>().negativeQuote);
            }
        }
        else if(profil.type != Sticker.Type.Profile)
        {
            player.GetComponent<Interactions>().CloseBookExe();
            GetComponent<PNJ>().ChangeDialog(3);
        }else if (profil.index == 1)
        {
            player.GetComponent<Interactions>().CloseBookExe();
            GetComponent<PNJ>().ChangeDialog(4);
        }
        else
        {
            player.GetComponent<Interactions>().CloseBookExe();
            GetComponent<PNJ>().ChangeDialog(GetComponent<PNJ>().negativeQuote);
        }
    }
}
