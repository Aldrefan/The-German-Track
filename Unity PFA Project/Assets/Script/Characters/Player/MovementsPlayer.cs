using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementsPlayer : MonoBehaviour {

    public float walk_speed;
    public float run_speed;
    bool facingRight = true;
    public bool sprint = false;
    float speed;
    public bool canRun;
    Rigidbody2D rb2d;
    Animator animator;
    bool canMove = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if(this.name == "Kenneth")
        {
            StartCoroutine(StartTimerBeforeCheckActivation());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Interactions>().state == Interactions.State.Normal && canMove)
        {
            if (Input.GetAxis("Sprint") > 0 && canRun)
            {
                sprint = true;
                animator.SetBool("Run", true);
            }
            else
            {
                sprint = false;
                animator.SetBool("Run", false);
            }

            if (sprint)
            {
                speed = run_speed;
            }
            else
            {
                speed = walk_speed;
            }

            float x = Input.GetAxis("Horizontal") * speed;
            float y = rb2d.velocity.y;
            rb2d.velocity = new Vector2(x, y);

            if (rb2d.velocity.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (rb2d.velocity.x < 0 && facingRight)
            {
                Flip();
            }

            if(rb2d.velocity.x > 0.01f || rb2d.velocity.x < -0.01f)
            {
                animator.SetBool("Walk", true);
            }
            else animator.SetBool("Walk", false);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
    }

    public void CheckSensAndFlip(float direction)
    {
        Debug.Log("Flip");
        Vector3 theScale = transform.localScale;
        if(direction < 0)
        {facingRight = false;}
        else facingRight = true;
        if(theScale.x < 0 && facingRight)
        {
            facingRight = false;
            theScale.x *= -1;
        }
        else if(theScale.x > 0 && !facingRight)
        {
            facingRight = true;
            theScale.x *= -1;
        }
    }

	void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        //GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator StartTimerBeforeCheckActivation()
    {
        if(GetComponent<Interactions>().PnjMet.Contains("Clara"))
        {
            canMove = true;
        }
        else 
        {
            yield return new WaitForSeconds(1);
            canMove = true;
        }
    }
}
