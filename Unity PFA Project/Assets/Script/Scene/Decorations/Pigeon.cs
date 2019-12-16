using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spr_renderer;
    ParticleSystem part_syst;
    Rigidbody2D rb2D;
    public Vector2 speed;
    public enum State {Walk, Eat, Fly, Stay}
    public State state;

    // Start is called before the first frame update
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        spr_renderer = GetComponent<SpriteRenderer>();
        part_syst = GetComponent<ParticleSystem>();
        rb2D = GetComponent<Rigidbody2D>();
        //ChangeState(State.Stay);
        RollDice();
    }

    IEnumerator WaitForNextAction(State actualState)
    {
        state = actualState;
        StopForce();
        switch(actualState)
        {
            case State.Walk:
            animator.SetBool("Walk", true);
            animator.SetBool("Eat", false);
            AddForce();
            yield return new WaitForSeconds(2);
            RollDice();
            break;

            case State.Eat:
            animator.SetBool("Walk", false);
            animator.SetBool("Eat", true);
            yield return new WaitForSeconds(4);
            RollDice();
            break;
            
            case State.Stay:
            animator.SetBool("Walk", false);
            animator.SetBool("Eat", false);
            yield return new WaitForSeconds(2);
            RollDirection();
            RollDice();
            break;
        }
    }

    void RollDirection()
    {
        int changeDirectionProbability = Random.Range(1, 7);
        if(changeDirectionProbability == 1 || changeDirectionProbability == 2)
        {
            spr_renderer.flipX = !spr_renderer.flipX;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine("Wait");
            StartCoroutine("LifeTime");
            
            if(GetComponent<SpriteRenderer>().flipX)
            {
                var shape = GetComponent<ParticleSystem>().shape;
                shape.rotation = new Vector3(0,180,0);
                GetComponent<ParticleSystemRenderer>().flip = new Vector3(1,0,0);
            }
            else
            {
                Vector3 rotation = new Vector3(0,180,0);
                var shape = GetComponent<ParticleSystem>().shape;
            }
            part_syst.Play();
            //animator.SetTrigger("Fly");
        }
    }

    void RollDice()
    {
        int eatProbability = Random.Range(1, 6);
        if(eatProbability == 1 || eatProbability == 2)
        {
            //StopAllCoroutines();
            //StartCoroutine("WaitBetweenStates", 1);
            //ChangeState(State.Eat);
            StartCoroutine("WaitForNextAction", State.Eat);
        }
        else if(eatProbability == 3 || eatProbability == 4)
        {
            //StopAllCoroutines();
            //StartCoroutine("WaitBetweenStates", 1);
            //ChangeState(State.Walk);
            StartCoroutine("WaitForNextAction", State.Walk);
        }
        else if(eatProbability == 5)
        {
            //StopAllCoroutines();
            //StartCoroutine("WaitBetweenStates", 1);
            //ChangeState(State.Walk);
            StartCoroutine("WaitForNextAction", State.Stay);
        }
    }

    void StopForce()
    {
        rb2D.velocity = new Vector2(0,0);
    }

    void AddForce()
    {
        if(GetComponent<SpriteRenderer>().flipX)
        {
            rb2D.AddForce(-speed, ForceMode2D.Impulse);
        }
        else
        {
            rb2D.AddForce(speed, ForceMode2D.Impulse);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        spr_renderer.enabled = false;
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
