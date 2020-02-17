using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchPoint : MonoBehaviour
{
    public Text searchPointTimer;

    [SerializeField]
    float timeToPress = 3;

    float timeToActivate;
    float timeSaver;
    float pressTimer;
    bool playerInRange;
    bool playerPressButton;

    Transform playerPos;

    Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        //    searchPointTimer.color = new Vector4(1, 1, 1, 0);
        originalColor = this.GetComponent<SpriteRenderer>().color;
        timeToActivate = timeToPress;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInTrigger();
        SeachPointTimerVisibility();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerPos = collision.transform;
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().color = originalColor;
            playerInRange = false;
        }
    }

    void SeachPointTimerVisibility()
    {
        if (pressTimer > 0)
        {
            searchPointTimer.color = new Vector4(0, 0, 0, 1);
        }
        else
        {
            searchPointTimer.color = new Vector4(0, 0, 0, 0);
        }
    }

    void PlayerInTrigger()
    {
        if (playerInRange)
        {
            if (Input.GetButton("Interaction"))
            {
                if (!playerPressButton)
                {
                    playerPressButton = true;
                    timeSaver = Time.time;
                }
                float Timer = Time.time - timeSaver;
                pressTimer = timeToActivate - Timer;
                searchPointTimer.text = pressTimer.ToString("f1");
                searchPointTimer.transform.position = Camera.main.WorldToScreenPoint(playerPos.position);
                if(pressTimer <= 0)
                {
                    this.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
            else
            {
                playerPressButton = false;
                pressTimer = 0;
                timeToActivate = timeToPress;
            }
        }

    }
}
