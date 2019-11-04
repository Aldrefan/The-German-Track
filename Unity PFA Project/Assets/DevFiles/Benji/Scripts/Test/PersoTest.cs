using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersoTest : MonoBehaviour
{

    [SerializeField]
    float speed = 1;

    bool inDialog;

    GameObject DialogCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<LeonDialogueManager>() != null)
        {
            DialogCanvas = FindObjectOfType<LeonDialogueManager>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CamFollow();
        if (!inDialog)
        {
            Movement();

        }
    }

    void Movement()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * speed, this.GetComponent<Rigidbody2D>().velocity.y);
        }else if (this.GetComponent<Rigidbody2D>().velocity.magnitude!=0)
        {
            this.GetComponent<Rigidbody2D>().velocity -= new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 0);
        }
    }

    void CamFollow()
    {
        Vector3 targetPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 10);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, 0.5f);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PNJinteractable"))
        {
            if (collision.GetComponent<DialogueTrigger>() != null)
            {
                if (Input.GetButton("Interaction"))
                {

                    inDialog = true;
                    this.DialogCanvas.GetComponent<LeonDialogueManager>().npcName = collision.GetComponent<DialogueTrigger>().npcName;
                    this.DialogCanvas.GetComponent<LeonDialogueManager>().InitDialogWidget();
                }
            }
        }
    }
}
