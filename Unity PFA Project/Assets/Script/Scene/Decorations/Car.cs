using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float time;
    public float direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x - direction, transform.position.y, transform.position.z);
        //Mathf.Log(direction);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name == "CarDespawn")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D()
    {
        //direction = directionDefault;
    }
}