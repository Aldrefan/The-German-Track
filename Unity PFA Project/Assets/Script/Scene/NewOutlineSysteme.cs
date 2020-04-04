using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewOutlineSysteme : MonoBehaviour
{
    float gap = 0.01f;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //ShowOutline();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && this.transform.Find("Sprite").GetComponent<SpriteRenderer>() && col.GetComponent<Interactions>().state == Interactions.State.Normal)
        {
            player = col.gameObject;
            ShowOutline();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" && this.transform.Find("Sprite").GetComponent<SpriteRenderer>())
        {
            HideOutline();
        }
    }

    public void ShowOutline()
    {
        if (player.GetComponent<Interactions>().PNJContact != null)
        {
            if (transform.childCount > 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            if (player.GetComponent<Interactions>().PNJContact.GetComponent<OutlineSystem>())
            { player.GetComponent<Interactions>().PNJContact.GetComponent<OutlineSystem>().HideOutline(); }
        }
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        SpriteRenderer originalSprite = this.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        for (int i = 0; i < 4; i++)
        {
            GameObject sprite = new GameObject("OutlineComponent");
            sprite.transform.parent = transform.Find("Sprite");
            sprite.transform.localScale = new Vector3(1, 1, 1);
            sprite.AddComponent<SpriteRenderer>();
            sprite.GetComponent<SpriteRenderer>().sprite = originalSprite.sprite;
            sprite.GetComponent<SpriteRenderer>().material.shader = Shader.Find("GUI/Text Shader");
            sprite.GetComponent<SpriteRenderer>().flipX = originalSprite.flipX;
            sprite.GetComponent<SpriteRenderer>().sortingOrder = originalSprite.sortingOrder - 1;
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
    }

    public void HideOutline()
    {
        if (transform.childCount > 0)
        {
            transform.Find("GoTo").gameObject.SetActive(false);
        }
        foreach (Transform child in transform.Find("Sprite"))
        {
            if (child.name == "OutlineComponent")
            {
                Destroy(child.gameObject);
            }
        }
    }
}
