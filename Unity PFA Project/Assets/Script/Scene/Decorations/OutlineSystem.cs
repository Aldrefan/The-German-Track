using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OutlineSystem : MonoBehaviour
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
        if(col.tag == "Player" && GetComponent<SpriteRenderer>())
        {
            player = col.gameObject;
            ShowOutline();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player" && GetComponent<SpriteRenderer>())
        {
            HideOutline();
        }
    }

    public void ShowOutline()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        if(player.GetComponent<Interactions>().PNJContact != null)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            player.GetComponent<Interactions>().PNJContact.GetComponent<OutlineSystem>().HideOutline();
        }
        for(int i = 0; i < 4; i++)
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
    }

    public void HideOutline()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        foreach(Transform child in transform)
        {
            if(child.name == "OutlineComponent")
            {
                Destroy(child.gameObject);
            }
        }
    }
}
