using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurantMovement : MonoBehaviour
{
    public float maxSpeed;
    void OnEnable()
    {
        Debug.Log(- transform.right + "velocity :" + GetComponent<Rigidbody2D>().velocity.x);
        GetComponent<Rigidbody2D>().AddForce(transform.right, ForceMode2D.Impulse);
    }

    void Update()
    {
        Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.x, 0, maxSpeed);
    }

    void OnDisable()
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.right, ForceMode2D.Impulse);
    }

}
