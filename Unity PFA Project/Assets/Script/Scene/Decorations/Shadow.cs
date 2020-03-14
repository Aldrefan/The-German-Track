using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject owner;
    RuntimeAnimatorController ownerAnimator;
    public Vector2 size;
    bool canBeDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        //ownerAnimator = owner.GetComponent<Animator>();
    }

    void OnEnable()
    {
        if(owner != null)
        {
            SetOwner();
        }
    }

    public void SetOwner()
    {
        ownerAnimator = owner.GetComponent<Animator>().runtimeAnimatorController;
        GetComponent<Animator>().runtimeAnimatorController = ownerAnimator;
        transform.rotation = Quaternion.Euler(180,0,0);
        StartCoroutine(TimerBeforeDestructionCheck());
    }

    void FixedUpdate()
    {
        if(owner == null && canBeDestroyed)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y - 4.5f, owner.transform.position.z);
        GetComponent<Animator>().SetBool("Walk", owner.GetComponent<Animator>().GetBool("Walk"));
        GetComponent<Animator>().SetBool("Run", owner.GetComponent<Animator>().GetBool("Run"));
        GetComponent<Animator>().SetFloat("actualSpeed", owner.GetComponent<Animator>().GetFloat("actualSpeed"));
        if(owner.tag != "Player")
        {
            if(owner.transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(-size.x, size.y);
            }
            else transform.localScale = new Vector2(size.x, size.y);
        }
        if(owner.tag == "Player")
        {
            transform.localScale = new Vector3(size.x * (owner.transform.localScale.x / 8), size.y, 1);
        }
        else transform.localScale = new Vector3(owner.transform.localScale.x, size.y, 1);
        
        GetComponent<SpriteRenderer>().flipX = owner.GetComponent<SpriteRenderer>().flipX;
    }

    IEnumerator TimerBeforeDestructionCheck()
    {
        yield return new WaitForSeconds(0.0001f);
        canBeDestroyed = true;
    }
}
