using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject owner;
    Animator ownerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        ownerAnimator = owner.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y - 4.5f, owner.transform.position.z);
        GetComponent<Animator>().SetBool("Walk", owner.GetComponent<Animator>().GetBool("Walk"));
        GetComponent<Animator>().SetBool("Run", owner.GetComponent<Animator>().GetBool("Run"));
        transform.localScale = new Vector3(owner.transform.localScale.x / 1.5f, transform.localScale.y, transform.localScale.z);
    }
}
