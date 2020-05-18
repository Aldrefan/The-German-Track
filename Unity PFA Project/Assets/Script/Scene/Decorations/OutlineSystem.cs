using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OutlineSystem : MonoBehaviour
{
    float gap = 0.01f;
    GameObject player;


    BoxCollider2D collider;

    int childCount;


    // Start is called before the first frame update
    void Awake()
    {
        //ShowOutline();
        collider = this.GetComponent<BoxCollider2D>();
        childCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (collider.enabled && this.transform.childCount <= childCount && player!=null)
        {
            ShowOutline();
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && GetComponent<SpriteRenderer>() && col.GetComponent<Interactions>().state == Interactions.State.Normal)
        {
            player = col.gameObject;
            //ShowOutline();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player" && GetComponent<SpriteRenderer>())
        {
            HideOutline();
            player = null;

        }
    }

    public void ShowOutline()
    {
        if (player.GetComponent<Interactions>().PNJContact != null)
        {
            if (transform.childCount > 1)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            if (player.GetComponent<Interactions>().PNJContact.GetComponent<OutlineSystem>())
            {player.GetComponent<Interactions>().PNJContact.GetComponent<OutlineSystem>().HideOutline(); /*Debug.Log("STP " + player.GetComponent<Interactions>().PNJContact);*/ }
        }
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        for (int i = 0; i < 4; i++)
        {
            GameObject sprite = new GameObject("OutlineComponent");
            sprite.transform.parent = transform;
            sprite.transform.localScale = new Vector3(1,1,1);
            sprite.AddComponent<SpriteRenderer>();
            sprite.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            sprite.GetComponent<SpriteRenderer>().material.shader = Shader.Find("GUI/Text Shader");
            sprite.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
            sprite.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
            switch (i)
            {
                case 0:
                    sprite.transform.localPosition = new Vector3(0, gap, 0);
                break;
                case 1:
                    sprite.transform.localPosition = new Vector3(0, -gap, 0);
                break;
                case 2:
                    sprite.transform.localPosition = new Vector3(gap, 0, 0);
                break;
                case 3:
                    sprite.transform.localPosition = new Vector3(-gap, 0, 0);
                break;
            }
        }
        //Debug.Log("Outline Shown " + player.GetComponent<Interactions>().PNJContact);
    }

    public void HideOutline()
    {
        player = null;
        if(transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        foreach(Transform child in transform)
        {
            if(child.name == "OutlineComponent")
            {
                Destroy(child.gameObject);
            }
        }
        //Debug.Log("Disparu " + player.GetComponent<Interactions>().PNJContact);
    }
}
