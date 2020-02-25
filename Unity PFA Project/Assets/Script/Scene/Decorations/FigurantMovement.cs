using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurantMovement : MonoBehaviour
{
    public float maxSpeed;
    public Vector2 speed;
    public bool hasRandomSpeed;
    public float actualSpeed;
    public bool canDespawn;
    public bool isJustMoving;
    public Vector2 limits;
    public enum State{Walk, Stand, Slowering}
    public State state;

    void OnEnable()
    {
        //Debug.Log(- transform.right + "velocity :" + GetComponent<Rigidbody2D>().velocity.x);
        if(hasRandomSpeed)
        {
            actualSpeed = Random.Range(speed.x, speed.y);
        }
        else actualSpeed = speed.x;
        if(GetComponent<Transform>().lossyScale.x < 0)
        {
            GoToTheLeft();
        }
        else GoToTheRight();
    }

    void FixedUpdate()
    {
        //Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.x, 0, maxSpeed);
        switch(state)
        {
            case State.Walk:
            Walk();
            break;

            case State.Stand:
            break;

            case State.Slowering:
            //Slowering();
            break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //Debug.Log(LayerManager.layerManager.SetNewLayer(transform.position));
            //GetComponent<SpriteRenderer>().sortingOrder = LayerManager.layerManager.SetNewLayer(transform.position);
        }
        if(col.GetComponent<FigurantSpawner>())
        {
            ChangeState(State.Stand);
            col.GetComponent<FigurantSpawner>().StartCoroutine("NewFigurantTimer", 4);
            Destroy(gameObject, 1);
        }
    }

    void Walk()
    {
        GetComponent<Animator>().SetFloat("actualSpeed", actualSpeed * 0.6f);
        if(isJustMoving)
        {
            if(transform.localPosition.x >= limits.y && GetComponent<Transform>().lossyScale.x > 0)
            {
                //GetComponent<SpriteRenderer>().flipX = true;
                //ChangeState(State.Slowering);
                ChangeDirection();
            }
            if(transform.localPosition.x <= limits.x && GetComponent<Transform>().lossyScale.x < 0)
            {
                //GetComponent<SpriteRenderer>().flipX = false;
                //ChangeState(State.Slowering);
                ChangeDirection();
            }
        }
        /*else if(canDespawn)
        {
            if(transform.localPosition.x >= limits.y || transform.localPosition.x <= limits.x)
            {
                Destroy(gameObject);
            }
        }*/
    }

    void ChangeState(State newState)
    {
        switch(newState)
        {
            case State.Walk:
            //state = newState;
            //this.enabled = true;
            break;

            case State.Stand:
            state = newState;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            GetComponent<Animator>().SetFloat("actualSpeed", 0);
            break;

            case State.Slowering:
            state = newState;
            StartCoroutine("Pause");
            Slowering();
            //this.enabled = false;
            break;
        }
    }

    void GoToTheRight()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * actualSpeed, ForceMode2D.Impulse);
    }

    void GoToTheLeft()
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.right * actualSpeed, ForceMode2D.Impulse);
    }

    void ChangeDirection()
    {
        /*if(actualSpeed > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(actualSpeed < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }*/
        //ChangeState(State.Stand);
        Vector3 theScale = transform.localScale;
        //GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        theScale.x *= -1;
        transform.localScale = theScale;
        //GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        if(GetComponent<Transform>().lossyScale.x < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(-transform.right * actualSpeed, ForceMode2D.Impulse);
            GoToTheLeft();
        }
        else 
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * actualSpeed, ForceMode2D.Impulse);
            GoToTheRight();
        }
        /*switch(GetComponent<SpriteRenderer>().flipX)
        {
            case true:
            GetComponent<Rigidbody2D>().AddForce(-transform.right * actualSpeed, ForceMode2D.Impulse);
            GoToTheLeft();
            break;

            case false:
            GetComponent<Rigidbody2D>().AddForce(transform.right * actualSpeed, ForceMode2D.Impulse);
            GoToTheRight();
            break;
        }*/
        //ChangeState(State.Walk);
    }

    void Stand()
    {
        //Slowering();
    }

    void OnDisable()
    {
        if(GetComponent<Transform>().lossyScale.x < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * actualSpeed, ForceMode2D.Impulse);
        }
        else GetComponent<Rigidbody2D>().AddForce(-transform.right * actualSpeed, ForceMode2D.Impulse);
    }

    void Slowering()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        ChangeDirection();
    }

    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(2);
        ChangeDirection();
    }
}