using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject owner;
    Animator ownerAnimator;
    public Vector2 size;
    // Start is called before the first frame update
    void Start()
    {
        //ownerAnimator = owner.GetComponent<Animator>();
    }

    void OnEnable()
    {
        ownerAnimator = owner.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y - 4.5f, owner.transform.position.z);
        GetComponent<Animator>().SetBool("Walk", owner.GetComponent<Animator>().GetBool("Walk"));
        GetComponent<Animator>().SetBool("Run", owner.GetComponent<Animator>().GetBool("Run"));
        transform.localScale = new Vector3(size.x * (owner.transform.localScale.x / 8), size.y, 1);
        GetComponent<SpriteRenderer>().flipX = owner.GetComponent<SpriteRenderer>().flipX;
    }
}
